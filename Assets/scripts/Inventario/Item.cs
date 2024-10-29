using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Item : ScriptableObject
{
  public string nome;
  public string descricao;
  public Sprite sprite;
  public GameObject itemPrefab; 

  public virtual string Nome()
  {
    return nome;
  }

  public virtual string Descricao()
  {
    return descricao;
  }

  public virtual Sprite Sprite()
  {
    return sprite;
  }
  public virtual GameObject ItemPrefab()
  {
    return itemPrefab;
  }
  
}
