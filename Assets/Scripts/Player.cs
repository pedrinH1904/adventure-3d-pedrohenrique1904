using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool estaVivo = true;
    [SerializeField] private int ouro;
    [SerializeField] private int vida;
    [SerializeField] private int forcaPulo;
    [SerializeField] private float velocidade;
    [SerializeField] private bool temChave;
    [SerializeField] private bool pegando;
    [SerializeField] private GameObject TelaDaMorte;
    [SerializeField] private bool podePegar;
    [SerializeField] private InventoryManager inventario;
    private Rigidbody rb;
    private bool estaPulando;
    private Vector3 angleRotation;
    private Avisos avisos;
    private TextMeshProUGUI textoOuro;
    private List<string> listaAvisos = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        ouro = 0;
        temChave = false;
        pegando = false;
        podePegar = false;
        angleRotation = new Vector3(0, 90, 0);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        avisos = GetComponent<Avisos>();
        TelaDaMorte.SetActive(false);
        //textoOuro = GameObject.FindGameObjectWithTag("Ouro").GetComponent<TextMeshProUGUI>();
        inventario = GameObject.FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //textoOuro.text = ouro.ToString();

        TurnAround();

        if (Input.GetKeyDown(KeyCode.E) && podePegar)
        {
            animator.SetTrigger("Pegando");
            pegando = true;
        }

        //Andar
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Andar", true);
            animator.SetBool("AndarPraTras", false);
            Walk();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("AndarPraTras", true);
            animator.SetBool("Andar", false);
            Walk();
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Andar", true);
        }
        else
        {
            animator.SetBool("Andar", false);
            animator.SetBool("AndarPraTras", false);
        }

        //Evitar o bug da movimentação
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Andar", false);
            animator.SetBool("AndarPraTras", false);
        }

        //Pulo
        if (Input.GetKeyDown(KeyCode.Space) && !estaPulando)
        {
            animator.SetTrigger("Pular");
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Ataque");
        }

        //Correr
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Correndo", true);
            Walk(8);
        }
        else
        {
            animator.SetBool("Correndo", false);
        }

        if (!estaVivo)
        {
            animator.SetTrigger("EstaVivo");
            estaVivo = true;
        }
    }

    private void Walk(float velo = 1)
    {
        if ((velo == 1))
        {
            velo = velocidade;
        }
        float fowardInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * fowardInput;
        Vector3 moveForward = rb.position + moveDirection * velo * Time.deltaTime;
        rb.MovePosition(moveForward);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        estaPulando = true;
        animator.SetBool("EstaNoChao", false);
    }

    private void TurnAround()
    {
        float sideInput = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(angleRotation * sideInput * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Chao"))
        {
            estaPulando = false;
            animator.SetBool("EstaNoChao", true);
        }

        if(collision.gameObject.CompareTag("Escada"))
        {
            SceneManager.LoadScene("Corredor");
        }
        if(collision.gameObject.CompareTag("Lava"))
        {
            TelaDaMorte.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        podePegar = true;
        avisos.MostrarAviso("Pressione E");
        if(other.gameObject.CompareTag("Lava"))
        {
            TelaDaMorte.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);
      
        if(other.gameObject.CompareTag("Chave") && pegando)
        {
            other.gameObject.GetComponent<ItemPickup>().Pickup();
            podePegar = false;
            pegando = false;
        }
        
        if(other.gameObject.CompareTag("Porta") && pegando && temChave)
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Abrir");
        }

        if (other.gameObject.CompareTag("Bau") && pegando)
        {
            if(VerificaChave(other.gameObject.GetComponent<Bau>().PegarNumeroFechadura()))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("Abrir");
                PegarConteudoBau(other.gameObject);
                podePegar = false;
                pegando = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pegando = false;
        podePegar = false;
    }

    private bool VerificaChave(int chave)
    {
        foreach (Item item in inventario.Itens)
        {
            if (item is Chave chaveNumero)
            {
                if (chaveNumero.PegarNumeroChave() == chave)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void PegarConteudoBau(GameObject bau)
    {
        Bau bauTesouro = bau.GetComponent<Bau>();

        avisos.MostrarAviso($"+{ouro += bauTesouro.PegarOuro()} de ouro");

        bauTesouro.AcessarConteudoBau();
        
        avisos.MostrarAviso("Novos itens!");
        
        foreach (Item item in inventario.Itens)
        {
            if (item is Chave chave) //Cast
            {
                if (chave.PegarNumeroChave() 
                    == bauTesouro.PegarNumeroFechadura())
                {
                    inventario.RemoveItem(item);
                    inventario.Itens.Remove(item);
                    avisos.MostrarAviso("Chave Utilizada!");
                }
            }
        }
    }


    
}



