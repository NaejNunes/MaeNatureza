using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int milesimos, segundos;
    public bool vitoria;
    // Start is called before the first frame update

    void Start()
    {
        milesimos = 1;
        segundos = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (vitoria == true)
        {
            milesimos -= 1;
        }

        if (milesimos == 0)
        {
            segundos -= 1;
            milesimos = 60;
        }

        if (segundos == 0)
        {
            SceneManager.LoadScene("Inicio");
        }

    }

    public void Jogar()
    {
        SceneManager.LoadScene("GamePlay");    
    }

    public void Inicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Historia()
    {
        SceneManager.LoadScene("Historia");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
