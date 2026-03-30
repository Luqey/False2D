using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] public bool myRoomOccupied;

    private LightSwitch[] lightsInRoom;

    [SerializeField] private float numberOfShifts;

    private void Start()
    {
        lightsInRoom = GetComponentsInChildren<LightSwitch>();
    }

    public IEnumerator LightChange(bool isPos)
    {
        for (int i = 0; i < numberOfShifts; i++)
        {
            if (isPos)
            {
                foreach(LightSwitch light in lightsInRoom)
                {
                    light.RoomShiftFunc(false, numberOfShifts);
                }
                yield return new WaitForSeconds(.15f);
            }
            else
            {
                foreach (LightSwitch light in lightsInRoom)
                {
                    light.RoomShiftFunc(true, numberOfShifts);
                }
                yield return new WaitForSeconds(.15f);
            }
        }
        myRoomOccupied = !myRoomOccupied;
    }
}
