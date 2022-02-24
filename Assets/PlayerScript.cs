using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{

    public int currentWeaponIndex = 0;
    int oldCurrentWeaponIndex = -1;

    
    //public weapon[] weapons = new weapon[2];

    public weapon[] weapons = new weapon[2];
    public weapon currWeapon;

    //STATS
    private string name;
    private int _health;
    public int money = 0;

    public float playerHealth = 100f;
    public float currentHealth = 100f;
    //STATS

    public Text moneytext;


    public Sprite onehandSprite;
    public Sprite twohandSprite;

    public GameObject oneHandPos;
    public GameObject twoHandPos;

    public GameObject oneHandWeaponPos;
    public GameObject twoHandWeaponPos;

    public GameObject fireanimpoint;

    public GameObject arm;
    public Transform oneharm;
    public Transform twoharm;

    public Transform weapon1hPos;
    public Transform weapon2hPos;

    Vector3 originalPos;

    public bool playerIsGettingDmg = false;

    GameObject oneh;
    GameObject weapon;

    shooting shoot;
    movement movement;

    audioManager audioManager;

    public GameObject laser;

    public Text youDeadTxt;

    public bool isDead = false;

    private void Awake()
    {
        //STATS
        name = "Alpha";
        _health = 5;
        money = 10000;
        //STATS


        currWeapon = weapons[currentWeaponIndex];
        moneytext.text = money.ToString() + "€";

        youDeadTxt.enabled = false;

        originalPos = GameObject.Find("arm").transform.position;
        GameObject.Find("playerhealth").GetComponent<Image>().fillAmount = currentHealth/playerHealth;

        oneh = GameObject.Find("arm");
        weapon = GameObject.Find("weapon");

        shoot = GetComponent<shooting>();
        movement = GetComponent<movement>();

        /*WEAPON MANAGEMENT eigene klasse?*/ 
        currWeapon = weapons[0]; //pistol am anfang
        weapons[0].currammo = 12;
        weapons[0].mags = 3;


        weapons[1].isBought = false;    //beta
        weapons[1].currammo = 25;
        weapons[1].mags = 3;

        weapons[2].isBought = false;    //ak
        weapons[2].currammo = 30;
        weapons[2].mags = 3;


        weapons[4].isBought = false;    //ars
        weapons[4].currammo = 30;
        weapons[4].mags = 3;

   

        audioManager = FindObjectOfType<audioManager>();


        playerStatus.playerExpCurrentGame = 0;

        playerStatus.playerLvlBeforeStartGame = playerStatus.playerLvl;
        playerStatus.playerExpBeforeStartGame = playerStatus.playerExp;

    }


    private void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;

        }

        if(currentWeaponIndex != oldCurrentWeaponIndex) //nur updaten wenn weaponIndex geändert wurde
        {
            /*
             PISTOL is unter hand FIXEN
             */
            if (weapons[currentWeaponIndex].handType == "1h")
            {
                twoHandPos.SetActive(false);
                oneHandPos.SetActive(true);

                oneHandPos.GetComponent<SpriteRenderer>().sprite = onehandSprite;
                if (weapons[currentWeaponIndex].weaponType == "melee")
                {
                    GameObject.Find("owp").transform.localRotation = Quaternion.Euler(0,0,60);

                    if(weapons[currentWeaponIndex].name == "knife")
                    {
                        GameObject.Find("owp").transform.localPosition = new Vector3(0.0f, 0.342f, 0);
                    }else if(weapons[currentWeaponIndex].name == "katana")
                    {
                        GameObject.Find("owp").transform.localPosition = new Vector3(-0.258f, 0.491f, 0);
                    }

                    oneHandWeaponPos.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    oneHandWeaponPos.GetComponent<SpriteRenderer>().flipX = true;

                }
                else
                {
                    GameObject.Find("owp").transform.localRotation = Quaternion.Euler(0, 0, 0);
                    GameObject.Find("owp").transform.localPosition = new Vector3(0.078f, 0.335f, 0);
                    oneHandWeaponPos.GetComponent<SpriteRenderer>().sortingOrder = 4;
                    oneHandWeaponPos.GetComponent<SpriteRenderer>().flipX = false;

                }

                oneHandWeaponPos.GetComponent<SpriteRenderer>().sprite = weapons[currentWeaponIndex].sprite;
                fireanimpoint.transform.localPosition = new Vector3(0.189f, 0.543f, 0);

            }
            if (weapons[currentWeaponIndex].handType == "2h")
            {

                oneHandPos.SetActive(false);
                twoHandPos.SetActive(true);

                twoHandPos.GetComponent<SpriteRenderer>().sprite = twohandSprite;
                
                twoHandWeaponPos.GetComponent<SpriteRenderer>().sprite = weapons[currentWeaponIndex].sprite;

                fireanimpoint.transform.localPosition = new Vector3(0.159f, 0.565f, 0);
            }

            if (weapons[currentWeaponIndex].name == "ars")
            {
                laser.SetActive(true);
            }
            else
            {
                laser.SetActive(false);
            }


            currWeapon = weapons[currentWeaponIndex];
            shoot.refreshGunHud();

            oldCurrentWeaponIndex = currentWeaponIndex; //nur updaten wenns geändert wurde

            shoot.reloadTimeProgress = currWeapon.reloadTime - playerStatus.schnellerNachladenPerk;
        }

    }

    public void updateMoney()
    {
        moneytext.text = money.ToString() + "€";
    }


    private void initWeapon()
    {



    }
    public int health
    {
        get { return health; }
        set { _health = value; }
    }

    public void setCurrentWeapon(int index)
    {
        Debug.Log("current set " + index);
        currentWeaponIndex = index;

    }



    public void playerGetDmg(float dmg)
    {
        audioManager.play("playerHit");
        shoot.playerAnim.SetTrigger("playerGotHit");
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            playerStatus.updatePlayerLvl();

            isDead = true;

            shoot.playerAnim.SetTrigger("isDead");
            shoot.joystickShoot.enabled = false;
            movement.joystick.enabled = false;
            movement.speed = 0f;

            audioManager.play("zombiedead");

            youDeadTxt.enabled = true;


            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(waitNextScene());
            
        }

        GameObject.Find("playerhealth").GetComponent<Image>().fillAmount = currentHealth / playerHealth;
        GameObject.Find("playerhealthInText").GetComponent<Text>().text = Mathf.Round(currentHealth).ToString();

    }

    public void refreshMoneyHud()
    {
        moneytext.text = money.ToString() + "€";
    }


    IEnumerator waitNextScene()
    {
        yield return new WaitForSeconds(2f);
        Statistik.save();
        SceneManager.LoadScene("showStats");
    }

}

