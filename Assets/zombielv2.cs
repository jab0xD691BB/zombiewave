using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombielv2 : EnemyAI
{
    // Start is called before the first frame update
    //das problem mit dem erben war, dass das gameobject einmal den script zombielv hatte und zusätzlich noch das,
    //EnemyAI skript. Da zombielv schon vom EnemyAI erbt, war das EnemyAI scipt im GameObject zu viel.
    //So wurden die Daten überschrieben und die Objekte haben sich nicht so verhalten wie man es wollte.
    //shoutout an https://forum.unity.com/threads/inheritance-and-public-protected-var.131111/ lol nice fix
    public Animator anim;
    public GameObject zguts;


    void Awake()
    {
        base.Awake();
    }

     void Start()
    {
        aipath.maxSpeed = 3.5f;     //grünen drücken zu gut, langsamer machen und mehr leben
        moneyValue = 75;
        startHealth = 6;
        health = startHealth;
        bodyDamage = 2;
        exp = 35;

}



public override void Die()
    {
        base.Die();
        anim.SetBool("die", true);
        aipath.maxSpeed = 0f;
        StartCoroutine(spawnGuts());    //spawnt die innerei
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 0.5f);
    }

    public override IEnumerator spawnGuts()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject guts = Instantiate(zguts, transform.position, Quaternion.identity);
        Destroy(guts, 10f);      //5sec wegen guts prefab animation, dauert auch nur 5sec
    }
}
