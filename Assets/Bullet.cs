using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    public GameObject en;
    public int dmg = 0;

    //public GameObject hitEffect;
    public GameObject hitEffectBlood;

    public Animator anim;


    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)  //Enemy
        {
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.transform.GetComponent<EnemyAI>().TakeDmg(dmg, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currWeapon.weaponType);
            anim.SetBool("bulletHitEnemy", true);

            Destroy(gameObject, 0.5f);

        }

        if (collision.gameObject.layer == 9) //Walls
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rb.velocity = Vector2.zero;
            anim.SetBool("bulletHitObstacle", true);

            Destroy(gameObject, 0.5f);
        }
    }


    IEnumerator waitForAnim(Collider2D collision)
    {
        
            if (collision.gameObject.layer == 8)  //Enemy
            {
                collision.transform.GetComponent<EnemyAI>().TakeDmg(dmg, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currWeapon.weaponType);
                anim.SetBool("bulletHitEnemy", true);
                rb.velocity = Vector2.zero;

                yield return new WaitForSeconds(0.6f);
                anim.SetBool("bulletHitEnemy", false);
                //gameObject.SetActive(false);
            }

            if (collision.gameObject.layer == 9) //Walls
            {
                rb.velocity = Vector2.zero;
                anim.SetBool("bulletHitObstacle", true);

                yield return new WaitForSeconds(0.6f);
                anim.SetBool("bulletHitObstacle", false);
                //gameObject.SetActive(false);
            }
    }

}
