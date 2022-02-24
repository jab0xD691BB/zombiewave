using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinne : EnemyAI
{
    public Animator anim;
    Vector3 lastPlayerPos;

    public GameObject spiderGuts;

    float cooldown = -1f;

    Vector3 dir;
    float angle;

    //float timeToReachTarget = 0.6f;
    float timeToReachTarget = 1.2f;

    float t = 0;

    bool isJumping = false;



    private void Awake()
    {
        base.Awake();
        anim.SetBool("isWalking", true);
        aipath.maxSpeed = 3f;
        moneyValue = 50;
        startHealth = 3;
        health = startHealth;
        bodyDamage = 10;
        exp = 30;

}


// Update is called once per frame
void Update()
    {
        
        float distance = Vector2.Distance(aipath.target.position, transform.position);
        if (2.4f < distance && distance < 2.5f)
        {

            if (cooldown < 0f)
            {
                cooldown = 10f;
                lastPlayerPos = aipath.target.position;
                StartCoroutine(chargeJump());

            }


        }
        cooldown -= Time.deltaTime;

        if (isJumping)
        {
            t += Time.deltaTime / timeToReachTarget;

            //springt in 1.4s zum lastPlayerPos
            transform.position = Vector3.Lerp(transform.position, lastPlayerPos, t);

        }

    }




    IEnumerator chargeJump()
    {
        //aipath.enabled = false;
        isJumping = true;

        dir = aipath.target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;

        anim.SetBool("isJumping", isJumping);


        yield return new WaitForSeconds(1.2f);
        // transform.position = aipath.desiredVelocity.normalized;
        //aipath.enabled = true;
        isJumping = false;
        anim.SetBool("isJumping", isJumping);
        t = 0;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping)
        {
            Physics2D.IgnoreLayerCollision(8, 10);
        }
    }

    

    public override void Die()
    {
        base.Die();
        anim.SetBool("isJumping", false);
        GameObject sguts = Instantiate(spiderGuts, transform.position, transform.rotation);
        Destroy(sguts, 5f);
    }
}
