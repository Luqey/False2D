using System.Collections;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    #region Dialogue Stuff
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] dialogueObjs;

    private int line = 0;
    #endregion

    private bool playerNearby;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(StartInteraction());
        }
    }

    private IEnumerator StartInteraction()
    {
        yield return StartCoroutine(dialogueManager.ReadText(dialogueObjs[line].dialogue, dialogueObjs[line].faceSprite, dialogueObjs[line].barkClip, dialogueObjs[line].lowPitch, dialogueObjs[line].highPitch, dialogueObjs[line].typeSpeed, true));
        if (line < dialogueObjs.Length - 1)
        {
            line++;
        }
    }
}
