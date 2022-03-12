using Core.AppStart;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        private new Rigidbody2D rigidbody;
        private new UnityEngine.Camera camera;
        public Text collectedText;
        public static int collectedAmount = 0;

        public GameObject bulletPrefab;
        public float bulletSpeed;
        private float lastFire;
        public float fireDelay;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            camera = UnityEngine.Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateVelocity();
            HandleShooting();
            UpdateUI();
        }

        void UpdateVelocity()
        {
            speed = GameController.MoveSpeed;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        }

        void HandleShooting()
        {
            fireDelay = GameController.FireRate;
            var mouseClick = Input.GetAxis("Fire1");
            if (mouseClick != 0)
            {
                ShootAt(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                float shootHor = Input.GetAxis("ShootHorizontal");
                float shootVert = Input.GetAxis("ShootVertical");
                if (shootHor != 0 || shootVert != 0)
                {
                    Shoot(shootHor, shootVert);
                }
            }
        }

        bool CanShoot()
        {
            return Time.time > lastFire + GameController.FireRate;
        }

        void Shoot(float x, float y)
        {
            if (CanShoot())
            {
                SpawnBullet(new Vector2(
                    (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                    (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed
                ));
            }
        }

        void ShootAt(float x, float y)
        {
            if (CanShoot())
            {
                var playerPos = camera.WorldToScreenPoint(transform.position);
                Debug.Log($"Mouse position is ({Input.mousePosition.x}, {Input.mousePosition.y}");
                Debug.Log($"Player position is {playerPos}");
                SpawnBullet((new Vector3(x, y, 0) - playerPos).normalized * bulletSpeed);
            }
        }

        void SpawnBullet(Vector2 velocity)
        {
            Debug.Log($"Spawning bullet with velocity {velocity}");
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = velocity;
            lastFire = Time.time;
        }

        void UpdateUI()
        {
            collectedText.text = "Items Collected: " + collectedAmount;
        }
    }
}
