using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Avisos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI avisos;

    public void MostrarAviso(string aviso)
    {
        avisos.text = aviso;
        Invoke("LimparAviso", 3);
    }

    public void LimparAviso()
    {
        avisos.text = "";
    }
}
