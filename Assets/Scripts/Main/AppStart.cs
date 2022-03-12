using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main
{
    public class AppStart : MonoBehaviour
    {
        private readonly Services services = new Services();
        private static bool s_initialized;
        private static AppStart s_instance;
    
        private const float TimeLimitForOneFrame = 0.025f;
        private const string ServicesPath = "Assets/Content/Services/";
    
        public static AppStart Instance => s_instance;
        public bool IsInitialized => s_initialized;
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitForDomainReload()
        {
            s_instance = null;
            s_initialized = false;
        }

        public void Awake()
        {
            if (!s_initialized)
            {
                DontDestroyOnLoad(gameObject);
                s_instance = this;
            }
            else
            {
                if (this != s_instance)
                {
                    Destroy(gameObject);
                }
            }
        }
    
        public IEnumerator Start()
        {
            if (s_initialized)
            {
                yield break;
            }

            Screen.orientation = ScreenOrientation.Portrait;

            Application.backgroundLoadingPriority = ThreadPriority.High;

            yield return StartCoroutine(ConfigureServices(services));
            yield return StartCoroutine(InitializeServices());
            SetupServices();

            Application.backgroundLoadingPriority = ThreadPriority.Normal;

            s_initialized = true;
        }
    
    
        private  IEnumerator ConfigureServices(Services services)
        {
        
            /*services.AddService(typeof(ConfigurationService), new ConfigurationService());
        yield return CreatePrefabService<AudioService, IAudioService>("AudioService", services);*/
            yield return null;
        }
    
        private IEnumerator InitializeServices()
        {
            var timeStamp = Time.realtimeSinceStartup;
            foreach (IGameService service in services.GetServices())
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                for (var e = service.Initialize(); e.MoveNext(); )
                {
                    if (Time.realtimeSinceStartup - timeStamp >= TimeLimitForOneFrame || e.Current != null)
                    {
                        timeStamp = Time.realtimeSinceStartup;
                        yield return e.Current;
                    }
                }

                sw.Stop();
                Debug.Log($"{service.GetType().Name} initialized ({sw.Elapsed})");
            }
        }
    
        private void SetupServices()
        {
            foreach (IGameService service in services.GetServices())
            {
                try
                {
                    service.Setup(services);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);

                }
            }
        }
    
        public IServiceProvider GetServiceProvider()
        {
            return services;
        }

        public T GetService<T>() where T : class
        {
            return services.GetService<T>();
        }
    
        public IEnumerator CreatePrefabService<TImpl, TInterface>(string serviceName, Services services, Action<TImpl> callback = null) where TImpl : UnityEngine.Component
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(ServicesPath + serviceName + ".prefab");
            yield return handle;
            var service = Instantiate(handle.Result).GetComponent<TImpl>();
            service.transform.SetParent(transform);
            Debug.Log("Adding service " + serviceName);
            services.AddService(typeof(TInterface), service);
            callback?.Invoke(service);
        }
    
        public void OnDestroy()
        {
            foreach (IGameService service in services.GetServices())
            {
                service.Shutdown();
            }

            if (this == s_instance)
            {
                s_instance = null;
                s_initialized = false;
            }
        }
    }
}
