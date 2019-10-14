using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotaAgua : MonoBehaviour
{
    public bool AguaPodre;

    int milesimosAguaPodre, segundosAguaPodre;

    // Start is called before the first frame update
    void Start()
    {
        milesimosAguaPodre = 100;
        segundosAguaPodre = 4;
    }

    // Update is called once per frame
    void Update()
    {
         milesimosAguaPodre -= 1;

        if(milesimosAguaPodre == 0)
        {
            segundosAguaPodre -=1;
            milesimosAguaPodre = 100;
        }

        if(segundosAguaPodre == 0)
        {
            Destroy(gameObject);
            segundosAguaPodre = 4;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("TagInimigo"))
        {
           Destroy(gameObject);
        }
       
    }
}

    

