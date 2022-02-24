using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    protected Transform target;   //player

    public AIPath aipath;

    
    protected float speed;      //child speed aendern es lag an protected virutal in start

    protected int exp = 0;

    public float nextWayPointDistance = 1f;

    //public Image healthbar;
    public float health = 3;
    public float startHealth = 3;
    public int moneyValue = 100;

    public Text dmgTxt;
    public Canvas dmgexpCanvas;
    public Animator dmgAnim;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    public string name;

    protected Seeker seeker;
    protected Rigidbody2D rb;

    public int bodyDamage = 1;

    float dmgSleep = -1f;

    float interval = -0.4f;

    float bodyDmgCd = -1f;

    spawner spawner;
    protected audioManager audioManager;

    PlayerScript playerScript;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        aipath.maxSpeed = 1f;
        target = GameObject.Find("player").transform;
        gameObject.GetComponent<AIDestinationSetter>().target = target;
        spawner = GameObject.Find("background").GetComponent<spawner>();
        audioManager = FindObjectOfType<audioManager>();
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();

        dmgTxt = GameObject.Find("dmgText").GetComponent<Text>();
        dmgexpCanvas = GameObject.Find("CanvasDmg").GetComponent<Canvas>();
        dmgAnim = GameObject.Find("dmgText").GetComponent<Animator>();

    }


    public void TakeDmg(int dmg, string type)
    {
        audioManager.play("zombiehit");


        float xx = Random.Range(-0.25f, 0.25f);
        float yy = Random.Range(-0.25f, 0.25f);

        dmgTxt.transform.position = new Vector2(gameObject.transform.position.x + xx, gameObject.transform.position.y + yy);
        dmgTxt.text = dmg.ToString();

        dmgAnim.SetTrigger("showDmg");


        health -= dmg;
        //healthbar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            if (type == "melee")
            {
                playerScript.money += (moneyValue * 2);

            }
            else if (type == "gun")
            {
                playerScript.money += moneyValue;

            }
            playerScript.updateMoney();
            Die();
        }

    }



    public virtual void Die()
    {
        audioManager.play("zombiedead");

        Statistik.monsterKills++;
        playerStatus.playerExpCurrentGame += exp;
        Debug.Log("playerExpCurrentGame: " + playerStatus.playerExpCurrentGame);

        //Destroy(healthbar.transform.parent.gameObject);

        aipath.maxSpeed = 0f;
        rb.velocity = Vector3.zero;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);

        spawner.enemies.Remove(gameObject);
        spawner.enemyMinusMinus();

    }

    public virtual IEnumerator spawnGuts()
    {

        yield return new WaitForSeconds(0.25f);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            if (bodyDmgCd < Time.time)
            {
                bodyDmgCd = Time.time + 1f;
                target.GetComponent<PlayerScript>().playerGetDmg(bodyDamage);

            }

        }



    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            if (bodyDmgCd < Time.time)
            {
                target.GetComponent<PlayerScript>().playerGetDmg(bodyDamage);


                bodyDmgCd = Time.time + 1f;

            }
        }
    }



}
