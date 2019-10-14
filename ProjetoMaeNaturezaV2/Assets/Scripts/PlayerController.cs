﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static float positionX, positionY, positionZ;

    public float speed, forceJump;

    int gotaDeAgua;

    bool groundCheck, florColisorCheck;

    public Rigidbody2D body;

    Animator animacao;

    public GameObject painelInfoFlor, nivelAguaPlanta;

    public GameObject[] GotaAguaHUD;

    // Start is called before the first frame update
    void Start()
    {
        gotaDeAgua = 1;

        groundCheck = true;
        florColisorCheck = false;

        body = gameObject.GetComponent<Rigidbody2D>();

        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {      
        positionX = transform.position.x;
        positionY = transform.position.y;

        MovimentarPlayer();
        
        if (gotaDeAgua > 5)
        {
            gotaDeAgua = 5;
        }

        //CONDIÇÃO PARA O PLAYER FALHAR CASO A AGUA DELE ACABE.
        else if (gotaDeAgua <= 0)
        {
            SceneManager.LoadScene("Derrota");

            animacao.SetBool("Morte", true);
        }

        if (Input.GetKeyDown(KeyCode.E) && florColisorCheck == true)
        {
            gotaDeAgua -= 1;
        }  
        
        //MOSTRA A QUATIDADE DE AGUA NA HUD
        if(gotaDeAgua == 5)
        {
            GotaAguaHUD[4].SetActive(true);    
            GotaAguaHUD[3].SetActive(true);    
            GotaAguaHUD[2].SetActive(true);    
            GotaAguaHUD[1].SetActive(true);    
            GotaAguaHUD[0].SetActive(true);    
        }
        if(gotaDeAgua == 4)
        {
            GotaAguaHUD[3].SetActive(true);    
            GotaAguaHUD[2].SetActive(true);    
            GotaAguaHUD[1].SetActive(true);    
            GotaAguaHUD[0].SetActive(true);

            GotaAguaHUD[4].SetActive(false);
        }

        if(gotaDeAgua == 3)
        {
            GotaAguaHUD[2].SetActive(true);    
            GotaAguaHUD[1].SetActive(true);    
            GotaAguaHUD[0].SetActive(true);

            GotaAguaHUD[3].SetActive(false);
            GotaAguaHUD[4].SetActive(false);
        }

        if(gotaDeAgua == 2)
        {
            GotaAguaHUD[1].SetActive(true);    
            GotaAguaHUD[0].SetActive(true);

            GotaAguaHUD[2].SetActive(false);
            GotaAguaHUD[3].SetActive(false);
            GotaAguaHUD[4].SetActive(false);
        }

        if(gotaDeAgua == 1)
        {
            GotaAguaHUD[0].SetActive(true);

            GotaAguaHUD[1].SetActive(false);
            GotaAguaHUD[2].SetActive(false);
            GotaAguaHUD[3].SetActive(false);
            GotaAguaHUD[4].SetActive(false);
        }

         if(gotaDeAgua == 0)
        {
            GotaAguaHUD[0].SetActive(false);
            GotaAguaHUD[1].SetActive(false);
            GotaAguaHUD[2].SetActive(false);
            GotaAguaHUD[3].SetActive(false);
            GotaAguaHUD[4].SetActive(false);
        }
    }
     void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Plataforma"))
        {
            groundCheck = true;
        }

        if (collider.gameObject.CompareTag("TagFogo"))
        {
            SceneManager.LoadScene("Derrota");
        }

        if (collider.gameObject.CompareTag("TagMorcego"))
        {
            SceneManager.LoadScene("Derrota");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TagGotaAgua"))
        {
            gotaDeAgua += 1;
        }
     
        if (collision.gameObject.CompareTag("TagFlor"))
        {
            florColisorCheck = true;
        }

        if (collision.gameObject.CompareTag("TagEsqueleto"))
        {
            gotaDeAgua -= 1;
        }

        if (collision.gameObject.CompareTag("TagFlor"))
        {
            painelInfoFlor.SetActive(true);
            nivelAguaPlanta.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TagFlor"))
        {
            painelInfoFlor.SetActive(false);
            nivelAguaPlanta.SetActive(false);
        }

        if (collision.gameObject.CompareTag("TagFlor"))
        {
            florColisorCheck = false;
        }
    }

    void MovimentarPlayer()
    {   
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animacao.SetBool("Parado", false);
            animacao.SetBool("Jump", false);
            animacao.SetFloat("Andar", speed);
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animacao.SetBool("Parado", false);
            animacao.SetBool("Jump", false);
            animacao.SetFloat("Andar", speed);
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
        else
        {
            animacao.SetBool("Jump", false);
            animacao.SetBool("Parado", true);
        }

        if (Input.GetButtonDown("Jump") && groundCheck == true)
        {
            animacao.SetBool("Jump",true);
            groundCheck = false;
            body.AddForce(new Vector2(0, forceJump));
        }
    }
}