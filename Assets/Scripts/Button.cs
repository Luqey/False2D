
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{

    [Header("Connections")]
    [SerializeField] private Door connectedDoor;

    [SerializeField] private BoxCollider2D hitbox;


    [SerializeField] private LayerMask layerMask;
    private bool isPressed;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, hitbox.size, 0f, layerMask);

        bool shouldBePressed = hit != null;

        if(shouldBePressed != isPressed)
        {
            isPressed = shouldBePressed;
            connectedDoor.Open(isPressed);
        }
    }
}
