using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public float velocidade;

    public bool direcaoDL;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (direcaoDL == true)
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (direcaoDL == false)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
