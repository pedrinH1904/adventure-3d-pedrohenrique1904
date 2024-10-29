using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; //Singleton - Armazena a instância do inventário

    public List<Item> Itens = new List<Item>(); //Lista de Scriptble Itens (Dados)

    public Transform itemContent; //Itens da UI
    public GameObject inventoryItem; //Modelo(Prefab) do Item na UI

    public InventoryItemController[] inventoryItens; //Array dos Itens na UI

    private bool ativo = false;

    private void Awake()
    {
        if (instance == null) //Singleton - Apenas um inventário
        {
            instance = this;
        }

    }

    public void AddItem(Item item) //Adiciona um item ao inventário
    {
        Itens.Add(item);
    }

    public void RemoveItem(Item item) //Remove um item do inventário
    {
        Itens.Remove(item);
    }

    public void RemoverItem(GameObject item) //Remove um item da UI
    {
        Destroy(item);
    }

    private void ListItens() //Lista os itens na UI do inventário
    {
        foreach (Transform item in itemContent) //Limpa a lista de itens na UI
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Itens) //Propriedades do Item para o Botão no inventário
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("NomeItem").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("Icone").GetComponent<Image>();
            var itemExcluir = obj.transform.Find("RemoverItem").GetComponent<Button>();

            obj.GetComponent<InventoryItemController>().itemPrefab = item.itemPrefab;

            itemName.text = item.nome;
            itemIcon.sprite = item.sprite;
            itemExcluir.onClick.AddListener(() => RemoverItem(obj));

        }

        DefinirItensInventario();
    }

    private void DefinirItensInventario() //Define os itens na UI do inventário
    {
        inventoryItens = itemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Itens.Count; i++)
        {
            inventoryItens[i].AddItem(Itens[i]);
        }
    }

    public void ListarItens()
    {
        if (ativo)
        {
            ativo = false;
            foreach (Transform item in itemContent) //Limpa a lista de itens na UI
            {
                Destroy(item.gameObject);
            }
        }
        else
        {
            ListItens();
            ativo = true;
        }
    }
}