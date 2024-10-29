using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arma", menuName = "Novo Item/ Arma")]
public class Arma : Item
{
    
    public bool ehMagica;
    public int dano;
    public int danoAdicional;

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

    public bool EhMagico()
    {
        return ehMagica;
    }

    public int Dano()
    {
        return dano;
    }

    public int DanoAdicional()
    {
        return danoAdicional;
    }
}