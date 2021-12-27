using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;
    private BoxCollider2D doorCollider;
    [SerializeField]private SpriteRenderer doorSpriteRenderer;
    private GameObject player;
    private float widthOffset = 4f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doorCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            switch(doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    break;
                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    break;
            }
        }
    }

    public void SetHideDoor(bool hide)
    {
        doorCollider.enabled = !hide;
        doorSpriteRenderer.enabled = !hide;
    }
}
