using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float speedY = 3.5f;
    [SerializeField] private float speedX = 3.5f;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        rend.material.mainTextureOffset = new Vector2(
                                        speedX * Time.timeSinceLevelLoad, 
                                        speedY * Time.timeSinceLevelLoad);
    }
}
