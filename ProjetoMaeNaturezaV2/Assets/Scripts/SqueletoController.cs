using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueletoController : MonoBehaviour
{
    public int velocidade, numAgua;

    public static float X, Y;

    public bool direcaoDL;

    public GameObject gotaDeAgua;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        X = transform.position.x;
        Y = transform.position.y;

        if (direcaoDL == true)
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
        }
        if (direcaoDL == false)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }
    }
  
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            SpownAgua();
        }

        if (collider.gameObject.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.CompareTag("TagFogo"))
        {
            Destroy(gameObject);
        }
    }  

    public void SpownAgua()
    {
        numAgua = Random.Range(0,2);

        switch(numAgua)
        {
            case 0: Instantiate(this.gotaDeAgua, new Vector2(SqueletoController.X, SqueletoController.Y), Quaternion.identity);
                    Destroy(gameObject);
                    break;         
        }
    }
}
