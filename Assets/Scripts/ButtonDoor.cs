using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    private BoxCollider2D pressedCollider;
    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    [SerializeField] private Sprite openedSprite;
    

    private void Start()
    {
        baseSprite = spriteRenderer.sprite;
        pressedCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open(bool isOpen)
    {
        if (isOpen)
        {
            pressedCollider.enabled = false;
            spriteRenderer.sprite = openedSprite;
            Debug.Log("I am Open");
        }
        else
        {
            pressedCollider.enabled = true;
            spriteRenderer.sprite = baseSprite;
            Debug.Log("I have closed");
        }
    }
}
