using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    private bool ativo = false;
    public void AtivarInventario()
    {
        if(ativo)
        {
            inventario.SetActive(false);
            ativo = false;
        }
        else
        {
            inventario.SetActive(true);
            ativo = true;
        }

    }

    public void DesativarInventario()
    {
        inventario.SetActive(false);
        ativo = false;
    }
}