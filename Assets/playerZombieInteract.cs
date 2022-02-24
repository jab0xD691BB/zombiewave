using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerZombieInteract : MonoBehaviour
{
    /*PlayerScript player;
    movement playerMovement;

    float bodyDmgCd = -1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerScript>();
        playerMovement = player.GetComponent<movement>();
    }

    




    List<GameObject> touchingEnemies = new List<GameObject>();
    int allBodyDmg = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            touchingEnemies.Add(collision.gameObject);
        }
        foreach (GameObject obj in touchingEnemies)
        {

        }
        Debug.Log(touchingEnemies.Count);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "zombielv1")
        {
            collision.gameObject.GetComponent<zombielv1>().anim.SetBool("enemyWalk", false);
            collision.gameObject.GetComponent<zombielv1>().anim.SetBool("isGrabbing", true);
            collision.gameObject.GetComponent<zombielv1>().isGrabbing = true;
            playerMovement.speed = 0f;
            playerMovement.GetComponent<shooting>().currentRotationSpeed = 0.25f;
        }

        if (collision.gameObject.layer == 8)
        {

            if (bodyDmgCd < Time.time)
            {
                foreach (GameObject obj in touchingEnemies)
                {
                    EnemyAI enemy = obj.GetComponent<EnemyAI>();

                    allBodyDmg += enemy.bodyDamage;

                }

                player.playerGetDmg(allBodyDmg);

                allBodyDmg = 0;
                bodyDmgCd = Time.time + 1f;

            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "zombielv1")
        {
            collision.gameObject.GetComponent<zombielv1>().anim.SetBool("enemyWalk", true);
            collision.gameObject.GetComponent<zombielv1>().anim.SetBool("isGrabbing", false);
            playerMovement.GetComponent<shooting>().currentRotationSpeed = playerMovement.GetComponent<shooting>().rotationSpeedMax;
            playerMovement.speed = playerMovement.maxSpeed;
        }

        if (touchingEnemies.Contains(collision.gameObject))
        {
            touchingEnemies.Remove(collision.gameObject);
            bodyDmgCd = Time.time + 1f;

        }
        Debug.Log(touchingEnemies.Count);
        Debug.Log(allBodyDmg);

    }


    */

}
