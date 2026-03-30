using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlipSwitch : MonoBehaviour
{
    private bool playerNearby = false;

    [SerializeField] Light2D connectedBulb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
            connectedBulb.enabled = !connectedBulb.enabled;
        }
    }
}
