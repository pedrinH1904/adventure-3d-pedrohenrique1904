using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Quarto da bruxa");
    }
 
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
 
    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
    public void Exit()
    {
        Application.Quit();
    }
}
