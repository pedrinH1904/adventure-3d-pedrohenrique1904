using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private Item item;
    public Button removeButton;
    public GameObject itemPrefab;

    public void RemoverItem()
    {
        InventoryManager.instance.RemoveItem(item);

        Instantiate(
                    itemPrefab,
                    GameObject.FindGameObjectWithTag("ItemDrop").transform.position,
                    Quaternion.identity);

        Destroy(gameObject);       
    }

    public void AddItem(Item novoItem)
    {
        item = novoItem;
    }
}