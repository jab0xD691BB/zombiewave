using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombielv1 : EnemyAI
{

    public Animator anim;
    public GameObject zombieGuts;
    public bool isGrabbing = false;
    

    //das problem mit dem erben war, dass das gameobject einmal den script zombielv hatte und zusätzlich noch das,
    //EnemyAI skript. Da zombielv schon vom EnemyAI erbt, war das EnemyAI scipt im GameObject zu viel.
    //So wurden die Daten überschrieben und die Objekte haben sich nicht so verhalten wie man es wollte.
    //shoutout an https://forum.unity.com/threads/inheritance-and-public-protected-var.131111/ lol nice fix

    void Awake()
    {
        base.Awake();
        aipath.maxSpeed = 2.5f; //2.5f default
        
        moneyValue = 35;
        startHealth = 40000;    //4 default
        health = startHealth;
        bodyDamage = 5;     //5 default
        exp = 25;
        anim.SetBool("enemyWalk", true);

        if (name == "zombielv1Boss" && name != null)
        {
            aipath.maxSpeed = 1f; //2.5f default
            transform.localScale = new Vector3(2f, 2f, 2f);

            moneyValue = 100;
            startHealth = 20;
            health = startHealth;
            bodyDamage = 15;     //5 default
            exp = 100;

        }



        InvokeRepeating("playZombieSound" , Random.Range(1.0f, 10.0f), Random.Range(10f, 20.0f));
    }

    public void setBossStats()
    {
        aipath.maxSpeed = 1f; //2.5f default
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        moneyValue = 100;
        startHealth = 20;
        health = startHealth;
        bodyDamage = 15;     //5 default
        exp = 100;
    }


    public override void Die()
    {
        base.Die();
        anim.SetBool("die", true);
        StartCoroutine(spawnGuts());    //nach 0.25s spawnt die innerei


    }

    public override IEnumerator spawnGuts()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject guts = Instantiate(zombieGuts, transform.position, Quaternion.identity);
        Destroy(guts, 8f);      //5sec wegen guts prefab animation, dauert auch nur 5sec
    }

    private void OnDestroy()
    {
        if (target != null)
        {
            target.GetComponent<movement>().speed = target.GetComponent<movement>().maxSpeed;
            target.GetComponent<shooting>().currentRotationSpeed = target.GetComponent<shooting>().rotationSpeedMax;

        }

    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        if (collision.gameObject.name == "player")
        {

            target.GetComponent<movement>().speed = 0f;
            target.GetComponent<shooting>().currentRotationSpeed = 0.25f;
            anim.SetTrigger("isGrabbing");

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            target.GetComponent<movement>().speed = target.GetComponent<movement>().maxSpeed;
            target.GetComponent<shooting>().currentRotationSpeed = target.GetComponent<shooting>().rotationSpeedMax;

        }

    }


    void playZombieSound()
    {
        audioManager.play("zombieSound");
    }
}
