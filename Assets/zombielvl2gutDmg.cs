using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombielvl2gutDmg : MonoBehaviour
{
    float dmgSleep = 0.2f;
    float gutDmg = 0.5f;

    bool istDrin = false;

    private void Update()
    {
        if (istDrin)
        {
            dmgSleep -= Time.deltaTime;
            if (dmgSleep < 0)
            {
                GameObject.Find("player").GetComponent<PlayerScript>().playerGetDmg(gutDmg);

                dmgSleep = 0.2f;
            }
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bin drin");
        if ("player" == collision.gameObject.name)
        {
            istDrin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ("player" == collision.gameObject.name)
        {
            istDrin = false;

        }
    }

    

}
