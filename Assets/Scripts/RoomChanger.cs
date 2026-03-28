using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] private GameObject mySpace;
    [SerializeField] private LightSwitch myRoomLight;

    private bool inside = false;
    private bool beingMovedTo;
    [SerializeField] int numberOfColorShifts;

    [SerializeField] private Transform connectedChanger;
    [SerializeField] private LightSwitch connectedLight;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !beingMovedTo && !inside)
        {
            connectedChanger.GetComponent<RoomChanger>().beingMovedTo = true;
            StartCoroutine(ShiftToNewSpace(collision.gameObject));
            inside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        beingMovedTo = false;
        inside = false;
    }

    private IEnumerator ShiftToNewSpace(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;
        yield return StartCoroutine(myRoomLight.LightChange(false));

        player.transform.position = connectedChanger.position;
        yield return new WaitForSeconds(.1f);

        yield return StartCoroutine(connectedLight.LightChange(true));
        player.GetComponent<PlayerController>().canMove = true;
    }

}
