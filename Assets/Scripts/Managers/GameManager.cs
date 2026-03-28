using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStart = true;
    [SerializeField] Transform bedPos;
    [SerializeField] GameObject player;

    private void Start()
    {
        if (gameStart)
        {
            gameStart = false;
            StartCoroutine(GameStartSequence());
        }
    }

    private IEnumerator GameStartSequence()
    {
        player.GetComponent<Collider2D>().enabled = false;
        player.transform.position = bedPos.position;
        player.GetComponent<Animator>().Play("Sleeping");

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Return));

        player.GetComponent<Animator>().Play("OutOfBed");
    }
}
