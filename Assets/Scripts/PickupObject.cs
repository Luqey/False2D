using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] public BoxCollider2D hitbox;

    [SerializeField] private bool playerNearby = false;

    [SerializeField] private bool pickupAble = true;
    [SerializeField] private bool pickedUp = false;
}
