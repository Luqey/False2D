using System.Collections;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    private Collider2D coll;

    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    [SerializeField] private Sprite openSprite;
    private bool isOpen;

    [SerializeField] InventoryItem itemNeeded;
    [SerializeField] private bool ignoreItemCheck;
    [SerializeField] private bool openOnStart = false;

    [SerializeField] private bool playerNearby;
    private PlayerInventory playerInventory;

    #region Dialogue Stuff
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] dialogueObjs;

    private int line = 0;
    #endregion

    #region Audio Stuff
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
    #endregion

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        baseSprite = spriteRenderer.sprite;

        if (openOnStart)
        {
            OpenDoor();
        }
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
            if(isOpen) return;
            if(!ignoreItemCheck && !playerInventory.CheckForItem(itemNeeded))
            {
                StartCoroutine(BlockedTextInteraction());
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        coll.enabled = false;
        spriteRenderer.sprite = openSprite;
        isOpen = true;
        if (!openOnStart)
        {
            audioSource.PlayOneShot(doorOpen);
        }
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
