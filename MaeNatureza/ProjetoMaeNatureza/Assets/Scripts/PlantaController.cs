using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlantaController : MonoBehaviour
{
        public static float X, Y;

    int nivelAtualAgua,// nivelPlanta,
        milesimosVitoria, milesimosEsqueleto, milesimosFogo, milesimosLevelDown, milesimosLevelUp, milesimosRegador, milesimosBat,
        segundosVitoria, segundosEsqueleto, segundosFogo, segundosBat,
        numEsqueleto, numEsqueleto2, numFogo, numBat;

    public int nivelPlanta;
    public Text txtnivelAguaPlanta, txtNivel, txtTempo;

    public GameObject painelFlor, nivelAguaHUD,  
                      levelUp, levelDown, regador, 
                      inimigoEsqueletoRight, inimigoEsqueletoLeft, inimigofogo, inimigoBatRight, inimigoBatLeft;

    bool florCheck;

    Animator animacao;

    SqueletoController EsqueletoCon;

    // Start is called before the first frame update
    void Start()
    {
        levelDown.SetActive(false);
        levelUp.SetActive(false);
        regador.SetActive(false);

        //nivelPlanta = 1;
        nivelAtualAgua = 0;

        animacao = GetComponent<Animator>();

        milesimosVitoria = 60;
        milesimosEsqueleto = 100;
        milesimosFogo = 100;
        milesimosBat = 100;

        segundosVitoria = 10;
        segundosEsqueleto = 3;
        segundosFogo = 2;
        segundosBat = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(segundosBat);

        //TEMPO RODANDO
        milesimosRegador -= 1;
        milesimosLevelDown -= 1;
        milesimosLevelUp -= 1;
        milesimosEsqueleto -= 1;
        milesimosFogo -= 1;
        milesimosBat -= 1;

        //PEGA A POSIÇÃO DO OBJETO NO CASO A PLANTA
        X = transform.position.x;
        Y = transform.position.y;
       
        //MOSTRA OS TEXTOS EM TELA 
        txtNivel.text = "Nv. " + nivelPlanta;     

        //TRATA O TEMPO PARA SPOWN DO ESQUELETOS
        if (milesimosEsqueleto == 0)
        {
            segundosEsqueleto -= 1;
            milesimosEsqueleto = 100;
        }

        //TRATA O TEMPO PARA O SPOWN DO FOGO
        if (milesimosFogo == 0)
        {
            segundosFogo -= 1;
            milesimosFogo = 200;
        }

        //TRATA O TEMPO PARA O SPOWN DO MORCEGO
        if (milesimosBat == 0)
        {
            segundosBat -= 1;
            milesimosBat = 200;
        }

        //COMANDO QUE REGA A PLANTA PARA ALMENTAR SEU NIVEL
        if (Input.GetKeyDown(KeyCode.E) && florCheck == true)
        {
            nivelAtualAgua += 1;
            regador.SetActive(true);
            milesimosRegador = 60;
        }

        //DA UM NIVEL A MAIS PARA A PLANTA FAZENDO ELA EVOLUIR
        if (nivelAtualAgua == 5)
        {
            levelUp.SetActive(true);
            milesimosLevelUp = 50;
            nivelPlanta += 1;
            nivelAtualAgua = 0;
        }

        //A PLANTA MORRE QUANDO SEU NIVEL É MENOS QUE NIVEL1
        if (nivelPlanta == 0)
        {
            SceneManager.LoadScene("Derrota");
        }

        //ATIVA A ANIMAÇÃO QUANDO A PLANTA SOBE DE NIVEL         
        if (nivelPlanta == 2)
        {
            animacao.SetInteger("Nivel", 1);
        }

        if (nivelPlanta == 3)
        {
            animacao.SetInteger("Nivel", 2);
        }

        if (nivelPlanta == 4)
        {
            animacao.SetInteger("Nivel", 3);
        }

        if (nivelPlanta == 5)
        {
            animacao.SetInteger("Nivel", 4);        
        }

        if (nivelPlanta == 6)
        {
            txtTempo.text = "" + segundosVitoria;
            animacao.SetInteger("Nivel", 5);
            TempoVitoria();
        }

        //PARA A ANIMAÇÃO DO REGADOR.
        if (milesimosRegador == 0)
        {
            regador.SetActive(false);
        }
        //PARA A ANIMAÇÃO DO LEVEL DOWN.
        if (milesimosLevelDown == 0)
        {
            levelDown.SetActive(false);
        }

        //PARA A ANIMAO DO LEVEL UP.
        if (milesimosLevelUp == 0)
        {
            levelUp.SetActive(false);
        }

        //SPOWNA OS INIMIGOS DO NIVEL 1
        if (nivelPlanta >= 1 && segundosEsqueleto == 0)
        {
            SpownEsqueleto();

            segundosEsqueleto = 3;
        }

        //SPOWNA OS INIMIGOS DO NIVEL 2
        if (nivelPlanta >= 2 && segundosFogo == 0)
        {
            SpownFogo();

            segundosFogo = 2;
        }
        else if (segundosFogo == 0)
        {
            segundosFogo = 2;
        }

        //SPOWNA OS INIMIGOS DO NIVEL 3
        if (nivelPlanta >= 3 && segundosBat == 0)
        {
            SpownMorcego(); 
            
            segundosBat = 4;
        }     
        else if(segundosBat == 0)
        {
            segundosBat = 4;
        }     
        
        var sliderNivelAgua = transform.GetChild(0).GetComponentInChildren<Slider>();
        sliderNivelAgua.value = nivelAtualAgua;  
    }

    //COMPARA OS COLISORES DOS OBJETOS E EXECUTA SUAS AÇÕES PRÉ DEFINIDAS.
    public void OnTriggerEnter2D(Collider2D collision)
    {

        //O QUE ACONTECE QUANDO O PLAYER COLIDE COM A PLANTA
        if (collision.gameObject.CompareTag("Player"))
        {
            painelFlor.SetActive(true);
            nivelAguaHUD.SetActive(true);
            florCheck = true;
        }

        //O QUE ACONTECE QUANDO O INIMIGO COLIDE COM A PLANTA
        if (collision.gameObject.CompareTag("TagInimigo"))
        {
            levelDown.SetActive(true);
            nivelPlanta -= 1;
            milesimosLevelDown = 50;
        }
    }

    public void OnTriggerExit2D(Collider2D collission)
    {
        florCheck = false;
    }

    //MÉTODO QUE INSTANCIA OS INIMIGOS ESQUELETOS NO CHAO
    public void SpownEsqueleto()
    {
        numEsqueleto = Random.Range(0, 2);

        switch (numEsqueleto)
        {
            case 0:
                Instantiate(this.inimigoEsqueletoRight, new Vector2(PlantaController.X + 10f, PlantaController.Y + 0.1f), Quaternion.identity);
                break;

            case 1:
                Instantiate(this.inimigoEsqueletoLeft, new Vector2(PlantaController.X - 10f, PlantaController.Y + 0.1f), Quaternion.identity);
                break;
        }
    }

    //MÉTODO QUE INSTANCIA OS INIMIGOS MORCEGO
    public void SpownMorcego()
    {
        numBat = Random.Range(0, 2);

        switch (numEsqueleto)
        {
            case 0:
                Instantiate(this.inimigoBatRight, new Vector2(PlantaController.X + 10f, PlantaController.Y + 3f), Quaternion.identity);
                break;

            case 1:
                Instantiate(this.inimigoBatLeft, new Vector2(PlantaController.X - 10f, PlantaController.Y + 3f), Quaternion.identity);
                break;
        }
    }

    //MÉTODO QUE INSTANCIA O FOGO QUE CAI DO CÉU
    public void SpownFogo()
    {
        numFogo = Random.Range(0, 2);

        switch (numFogo)
        {
            case 0:
                Instantiate(this.inimigofogo, new Vector2(PlantaController.X + 3f, PlantaController.Y + 10f), Quaternion.identity);
                break;

            case 1:
                Instantiate(this.inimigofogo, new Vector2(PlantaController.X - 3f, PlantaController.Y + 10f), Quaternion.identity);
                break;
          
        }
    }

    //MÉTODO DE TEMPO PARA CRONOMETRAR ALGUMAS AÇÕES QUE DEVEM OCORRER NO JOGO
    public void TempoVitoria()
    {
        milesimosVitoria -= 1;

        if (milesimosVitoria <= 0)
        {
            milesimosVitoria = 60;
            segundosVitoria -= 1;
        }

        if (segundosVitoria == 0)
        {
            SceneManager.LoadScene("Vitoria");
        

        }
    }
}
