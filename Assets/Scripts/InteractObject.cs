using System.Collections;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private bool playerNearby = false;

    #region Dialogue Stuff
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] objDialogues;

    private int line = 0;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(InteractWithObject());
        }
    }

    private IEnumerator InteractWithObject()
    {
        yield return StartCoroutine(dialogueManager.ReadText(objDialogues[line].dialogue, objDialogues[line].faceSprite, objDialogues[line].barkClip, objDialogues[line].lowPitch, objDialogues[line].highPitch, objDialogues[line].typeSpeed, true));
        if (line < objDialogues.Length - 1)
        {
            line++;
        }
    }
}
