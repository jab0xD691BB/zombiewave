using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Damit sich der spieler nach einer kollision mit 
 * einem Zombie nicht weiter bewegt freezed man im Inspector die Z-Achse!
 * 
 */

public class movement : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1;
    [HideInInspector]
    public float maxSpeed = 2f;

    public Rigidbody2D playerRigidbody2d;
    Vector2 moveDir;
    public Animator anim;

    public Joystick joystick;

    audioManager audioManager;

    AnimatorClipInfo[] ac;

    shooting shooting;

    float walkDuration = -1f;
    public bool isWalking = false;

    public PlayerScript playerScript;

    private void Awake()
    {

        audioManager = FindObjectOfType<audioManager>();
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        shooting = GetComponent<shooting>();

        playerScript = GetComponent<PlayerScript>();

        maxSpeed = 2.5f;
        speed = maxSpeed;
    }


    private void FixedUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;
        anim.SetBool("isWalking", false);
        //playerRigidbody2d.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = +1f;
        if (Input.GetKey(KeyCode.W)) moveY = +1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;

        if (joystick.Horizontal <= -.1f)
        {
            moveX = joystick.Horizontal;
            anim.SetBool("isWalking", true);
        }
        if (joystick.Horizontal >= .1f)
        {
            moveX = joystick.Horizontal;
            anim.SetBool("isWalking", true);
        }
        if (joystick.Vertical >= .1f)
        {
            anim.SetBool("isWalking", true);
            moveY = joystick.Vertical;
        }
        if (joystick.Vertical <= -.1f)
        {
            anim.SetBool("isWalking", true);
            moveY = joystick.Vertical;
        }

        float mv = moveX + moveY;

        //moveDir = new Vector2(moveX, moveY).normalized;
        moveDir = new Vector2(moveX, moveY);

        //beweget man sich in der richtung wo man hinschaut +- 70 grad, so is der movement normal, das gegenteil reduziert das movement um die hälfte, so als wuerde man rueckwaerts laufen
        /*float angleMov = Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg;
        float startAngle = shooting.lookangle - 80;
        float endAngle = shooting.lookangle + 80;
        Debug.Log(joystick.Direction);
        if (startAngle < angleMov && angleMov < endAngle)
        {
            transform.position += (Vector3)moveDir * (speed + playerStatus.schnellerRennenPerk) * Time.deltaTime;

        }
        else
        {
            transform.position += (Vector3)moveDir * ((speed / 2) + playerStatus.schnellerRennenPerk) * Time.deltaTime;

        }*/
        if (playerScript.currWeapon.name == "knife" || playerScript.currWeapon.name == "katana")
        {
            speed = 3.0f;
        }
        else
        {
            speed = 2.5f;
        }

        transform.position += (Vector3)moveDir * (speed + playerStatus.schnellerRennenPerk) * Time.deltaTime;

    }

    public void Stepleft()
    {
        audioManager.play("stepleft");
    }

    public void Stepright()
    {
        audioManager.play("stepright");
    }


}
