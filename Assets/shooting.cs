using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public Animator animFire;
    public Animator playerAnim;
    public Animator animReload;

    public CanvasGroup reloadProgressBg;
    public Image reloadProgressInner;

    private float bulletForce = 1000f;

    int maxAmmo = 100;
    int currentAmmo = 0;

    public bool isReloading = false;


    private float shootrate = 0.5f;
    private float nextFire = 0.0f;


    public float reloadTime = 2;
    public float reloadTimeProgress = 2f;

    public Button reloadButton;

    public Bullet bullet;


    public Text ammoDisplay;
    public Text magsTxt;

    public Joystick joystickShoot;

    GameObject player;
    PlayerScript playerScript;

    audioManager audioManager;

    public bool isShooting = false;
    public bool isKnifing = false;

    public GameObject bloodSpillPrefab;

    public float currentRotationSpeed = 1f;
    public float rotationSpeedMax = 10f;

    public float lookangle = 0f;

    //für kreis
    public float attackRange = 0.28f;
    //für rechteck
    public Vector2 attackRangeBox = new Vector2(0, 0);

    public GameObject slots;

    // Start is called before the first frame update
    void Start()
    {
        currentRotationSpeed = rotationSpeedMax;
        lookangle = 90f;

        player = GameObject.Find("player");
        playerScript = player.GetComponent<PlayerScript>();
        audioManager = FindObjectOfType<audioManager>();

        playerScript.currWeapon.currammo = playerScript.currWeapon.maxAmmo;
        refreshGunHud();

        slots = GameObject.FindGameObjectWithTag("slots");

    }

    // Update is called once per frame
    void Update()
    {
        if (joystickShoot.Vertical >= .1f || joystickShoot.Horizontal >= .1f ||
            joystickShoot.Vertical <= -.1f || joystickShoot.Horizontal <= -.1f)
        {
            float angle = Mathf.Atan2(joystickShoot.Direction.y, joystickShoot.Direction.x) * Mathf.Rad2Deg - 90;
            lookangle = Mathf.Atan2(joystickShoot.Direction.y, joystickShoot.Direction.x) * Mathf.Rad2Deg;
            if(lookangle <= -90)
            {
                lookangle *= -1;
            }
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, q, Time.deltaTime * currentRotationSpeed);
        }

        if ((joystickShoot.Vertical >= .6f || joystickShoot.Horizontal >= .6f ||
            joystickShoot.Vertical <= -.6f || joystickShoot.Horizontal <= -.6f)
            && !isReloading) // left click
        {
            if (!playerScript.isDead)
            {
                shoot();
                slots.GetComponent<CanvasGroup>().blocksRaycasts = false;

            }
        }
        else
        {
            playerAnim.SetBool("isShooting", false);
            animFire.SetBool("isFiring", false);
            slots.GetComponent<CanvasGroup>().blocksRaycasts = true;

        }


        if (isReloading)
        {
            reloadTimeProgress -= Time.deltaTime;
            reloadProgressBg.alpha = 1f;
            reloadProgressInner.fillAmount = reloadTimeProgress / (playerScript.currWeapon.reloadTime - playerStatus.schnellerNachladenPerk);

            if (reloadTimeProgress < 0)
            {
                GameObject.Find("reloadProgressBg").GetComponent<CanvasGroup>().alpha = 0f;

            }
        }
    }


    public void reloadWeapon()
    {
        if (playerScript.currWeapon.weaponType.Equals("melee"))
        {
            return;
        }
        if (playerScript.currWeapon.mags > 0)
        {
            audioManager.play("reloadgun");
            StartCoroutine(reload());
        }

    }


    //wenn muni voll ist nicht nachladen
    IEnumerator reload()
    {
        isReloading = true;
        slots.GetComponent<CanvasGroup>().blocksRaycasts = false;


        yield return new WaitForSeconds(playerScript.currWeapon.reloadTime - playerStatus.schnellerNachladenPerk);

        playerScript.currWeapon.currammo = playerScript.currWeapon.maxAmmo;
        playerScript.currWeapon.mags--;
        refreshGunHud();

        isReloading = false;
        slots.GetComponent<CanvasGroup>().blocksRaycasts = true;

        animReload.SetBool("isReloading", false);

        reloadTimeProgress = playerScript.currWeapon.reloadTime - playerStatus.schnellerNachladenPerk;

    }



    void shoot()
    {
        if (playerScript.currWeapon.weaponType == "gun")
        {

            if (playerScript.currWeapon.currammo > 0 && Time.time > nextFire)
            {
                audioManager.play(playerScript.currWeapon.name + "Shot");

                animFire.SetBool("isFiring", true);
                playerAnim.SetBool("isShooting", true);

                CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, .1f, playerScript.currWeapon.firerate);

                nextFire = Time.time + playerScript.currWeapon.firerate;
                playerScript.currWeapon.currammo--;
                refreshGunHud();


                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                /*GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet");
                if(bullet != null)
                {
                    bullet.transform.position = firePoint.position;
                    bullet.transform.rotation = firePoint.rotation;
                    bullet.SetActive(true);
                }*/
                bullet.GetComponent<Bullet>().dmg = playerScript.currWeapon.damage;

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                rb.AddForce(firePoint.up * bulletForce, 0);


            }
            else
            {
                playerAnim.SetBool("isShooting", false);
                animFire.SetBool("isFiring", false);

            }

            if (playerScript.currWeapon.currammo <= 0 && Time.time > nextFire)
            {
                audioManager.play("gunEmpty");

                animReload.SetBool("isReloading", true);
                nextFire = Time.time + playerScript.currWeapon.firerate;

            }

        }
        else if (playerScript.currWeapon.weaponType.Equals("melee"))
        {
            

            if (Time.time > nextFire)
            {
                LayerMask enemyLayers = LayerMask.GetMask("Enemy");

                if (playerScript.currWeapon.name == "knife")
                {
                    attackRange = 0.28f;
                    attackRangeBox = new Vector2((float)0.2, (float)0.5);
                    playerAnim.SetBool("isKnifing", true);

                }
                else if (playerScript.currWeapon.name == "katana")
                {
                    Debug.Log("curr weapon katana");
                    attackRange = 0.56f;
                    attackRangeBox = new Vector2((float)0.35, 1.1f);
                    playerAnim.SetBool("isKnifingKatana", true);

                }

                audioManager.play("knifeswish");




                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerScript.oneHandWeaponPos.transform.position, attackRange, enemyLayers);
                //Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(playerScript.oneHandWeaponPos.transform.position, attackRangeBox , 180, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("hit with melee");
                    enemy.GetComponent<EnemyAI>().TakeDmg(playerScript.currWeapon.damage, playerScript.currWeapon.weaponType);
                    GameObject go = Instantiate(bloodSpillPrefab,
                        enemy.ClosestPoint(playerScript.oneHandWeaponPos.transform.position),
                        playerScript.oneHandPos.transform.rotation);
                    go.GetComponent<SpriteRenderer>().sortingOrder = 3;

                    Destroy(go, 0.8f);
                }
                nextFire = Time.time + playerScript.currWeapon.firerate;

            }
            else
            {

                playerAnim.SetBool("isKnifing", false);
                playerAnim.SetBool("isKnifingKatana", false);
            }

            /*
             * Bei Knife Anim -> Samples 3 = 1x schwingen und Firerate = 1/ bei Firerate 0.5 -> Samples 6
             */

            isKnifing = true;

        }

    }


    private void OnDrawGizmosSelected()
    {
        //fürs knife
        //Gizmos.DrawWireSphere(GameObject.Find("onehandWeaponPos").transform.position, 0.2f);

        Gizmos.DrawWireSphere(GameObject.Find("onehandWeaponPos").transform.position, attackRange);

        //fürs katana
        //Gizmos.matrix = Matrix4x4.TRS(GameObject.Find("onehandWeaponPos").transform.position, GameObject.Find("onehandWeaponPos").transform.rotation, GameObject.Find("onehandWeaponPos").transform.lossyScale);
        //Gizmos.DrawWireCube(GameObject.Find("onehandWeaponPos").transform.localPosition, new Vector3(attackRangeBox.x, attackRangeBox.y, 0));
    }

    public void refreshGunHud()
    {
        ammoDisplay.text = playerScript.currWeapon.currammo.ToString() + "/" + playerScript.currWeapon.maxAmmo.ToString();
        magsTxt.text = playerScript.currWeapon.mags.ToString();
    }



}
