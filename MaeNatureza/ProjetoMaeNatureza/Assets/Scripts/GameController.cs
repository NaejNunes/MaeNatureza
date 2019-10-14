using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{  
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jogar()
    {
        SceneManager.LoadScene("GamePlay");    
    }

    public void Inicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Tutorial1()
    {
        SceneManager.LoadScene("Tutorial 1");
    }
}
