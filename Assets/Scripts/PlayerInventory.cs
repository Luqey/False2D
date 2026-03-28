using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> playerInv = new();

    public void GiveItem(InventoryItem collectedItem)
    {
        playerInv.Add(collectedItem);
    }

    public bool CheckForItem(InventoryItem itemToCompare)
    {
        if (playerInv.Contains(itemToCompare))
        {
            Debug.Log("Item Matched!");
            return true;
        }
        else
        {
            Debug.Log("No Item to Match!");
            return false;
        }
    }
}
