using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;
    private Color colorEmission;

    private void Start()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        colorEmission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    public void Pickup()
    {
//        InventoryManager.instance.AddItem(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", colorEmission);
    }
}