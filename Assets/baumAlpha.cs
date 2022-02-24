using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baumAlpha : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Color color = new Color(1f, 1f, 1f, 0.7f);

            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Color color = new Color(1f, 1f, 1f, 1f);

            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
         

    }
    

}
