using UnityEngine;

namespace DungeonGeneration
{
    public class Door : MonoBehaviour
    {
        public enum DoorType
        {
            left, right, top, bottom
        }

        public DoorType doorType;
        private BoxCollider2D doorCollider;
        [SerializeField]private SpriteRenderer openDoorSpriteRenderer;
        [SerializeField]private SpriteRenderer closedDoorSpriteRenderer;

        private void Start()
        {
            doorCollider = GetComponent<BoxCollider2D>();
        }
    
        public void CloseDoor()
        {
            doorCollider.enabled = true;
            closedDoorSpriteRenderer.enabled = true;
        }

        public void OpenDoor()
        {
            doorCollider.enabled = false;
            openDoorSpriteRenderer.enabled = true;
            closedDoorSpriteRenderer.enabled = false;
        }
    }
}
