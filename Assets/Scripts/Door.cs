using UnityEngine;

public class Door : MonoBehaviour
{
    private BoxCollider2D pressedCollider;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        pressedCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open(bool isOpen)
    {
        if (isOpen)
        {
            pressedCollider.enabled = false;
            spriteRenderer.enabled = false;
            Debug.Log("I am Open");
        }
        else
        {
            pressedCollider.enabled = true;
            spriteRenderer.enabled = true;
            Debug.Log("I have closed");
        }
    }
}
