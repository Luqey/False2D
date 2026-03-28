using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    [SerializeField] private Sprite collectedSprite;
    private bool alrObtained;

    [SerializeField] private bool playerNearby;
    private PlayerInventory playerInventory;

    [SerializeField] private InventoryItem containedItem;
    [SerializeField] private InventoryItem itemNeeded;
    [SerializeField] private bool ignoreItemCheck;

    #region Dialogue Stuff
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] dialogueObjs;

    private int line = 0;
    #endregion

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNearby = true;
            playerInventory = collision.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNearby = false;
            playerInventory = null;
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            if(alrObtained) return;
            if(!ignoreItemCheck && !playerInventory.CheckForItem(itemNeeded))
            {
                StartCoroutine(BlockedTextInteraction());
            }
            else
            {
                Collect();
            }
        }
    }

    private void Collect()
    {
        if(collectedSprite != null) { spriteRenderer.sprite = collectedSprite; }
        playerInventory.GiveItem(containedItem);
        containedItem = null;
        alrObtained = true;
    }

    private IEnumerator BlockedTextInteraction()
    {
        yield return StartCoroutine(dialogueManager.ReadText(dialogueObjs[line].dialogue, dialogueObjs[line].faceSprite, dialogueObjs[line].barkClip, dialogueObjs[line].lowPitch, dialogueObjs[line].highPitch, dialogueObjs[line].typeSpeed, true));
        if (line < dialogueObjs.Length - 1)
        {
            line++;
        }
    }
}