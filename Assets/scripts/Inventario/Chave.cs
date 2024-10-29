using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chave", menuName = "Novo Item/ Chave")]
public class Chave : Item
{
    
    public int numeroChave;

    public override string Nome()
    {
        return nome;
    }
    public override string Descricao()
    {
        return descricao;
    }
    public override Sprite Sprite()
    {
        return sprite;
    }
    public int PegarNumeroChave()
    {
        return numeroChave;
    }
}