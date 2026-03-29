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
    [SerializeField] Dialogue[] blockedDialogueObjs;
    [SerializeField] Dialogue[] collectedDialogueObjs;

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
        if (collectedDialogueObjs != null)
        {
            StartCoroutine(CollectedTextInteraction());
        }
        alrObtained = true;
    }

    private IEnumerator BlockedTextInteraction()
    {
        yield return StartCoroutine(dialogueManager.ReadText(blockedDialogueObjs[line].dialogue, blockedDialogueObjs[line].faceSprite, blockedDialogueObjs[line].barkClip, blockedDialogueObjs[line].lowPitch, blockedDialogueObjs[line].highPitch, blockedDialogueObjs[line].typeSpeed, true));
        if (line < blockedDialogueObjs.Length - 1)
        {
            line++;
        }
    }

    private IEnumerator CollectedTextInteraction()
    {
        yield return StartCoroutine(dialogueManager.ReadText(collectedDialogueObjs[line].dialogue, collectedDialogueObjs[line].faceSprite, collectedDialogueObjs[line].barkClip, collectedDialogueObjs[line].lowPitch, collectedDialogueObjs[line].highPitch, collectedDialogueObjs[line].typeSpeed, true));
        if (line < collectedDialogueObjs.Length - 1)
        {
            line++;
        }
    }
}