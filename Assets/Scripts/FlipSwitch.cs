using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
    private bool playerNearby = false;

    [SerializeField] LightSwitch connectedBulb;

    [SerializeField] private float desiredIntensity;

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
            connectedBulb.SetNewIntensity(desiredIntensity);
        }
    }
}
