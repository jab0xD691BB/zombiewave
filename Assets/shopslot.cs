using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopslot : MonoBehaviour, IPointerClickHandler
{

    public weapon weapon;
    public bool isChoose = false;
    public int slotNr = 99;
    public Image bg;
    public Image inner;
    public Image weaponImg;
    Text nameText;
    public Text priceText;
    Text dmgText;
    Text ammoPricetxt;

    PlayerScript playerScript;

    audioManager audioManager;


    private void OnEnable()
    {
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();
    }

    private void Awake()
    {
        GameObject parent = transform.parent.gameObject;
        List<GameObject> gos = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            gos.Add(child.gameObject);
        }

        audioManager = GameObject.FindObjectOfType<audioManager>();

        nameText = gos[3].GetComponent<Text>();
        priceText = gos[4].GetComponent<Text>();
        dmgText = gos[5].GetComponent<Text>();
        ammoPricetxt = gos[8].GetComponent<Text>();
        weaponImg = GetComponent<Image>();

        nameText.text = weapon.name;
        priceText.text = "Price: " + weapon.price.ToString() + "€";
        dmgText.text = "Damage: " + weapon.damage.ToString();
        ammoPricetxt.text = weapon.ammoPrice.ToString() + "€";

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("shopbg").GetComponent<shopmanager>().currentSlotNr = slotNr;
        Debug.Log(slotNr);
        audioManager.play("buttonClick");

        if (!weapon.isBought && weapon.price <= GameObject.Find("player").GetComponent<PlayerScript>().money)
        {
            Debug.Log("weapon bought: " + weapon.isBought);
            GameObject.Find("shopbg").GetComponent<shopmanager>().showPopUp();
        }

    }

    public void buyBullets()
    {
        if (playerScript.money >= weapon.ammoPrice && weapon.isBought)
        {
            audioManager.play("buttonClick");

            playerScript.weapons[weapon.weaponIndex].mags += 1;
            playerScript.money -= weapon.ammoPrice;
            playerScript.GetComponent<shooting>().refreshGunHud();
            playerScript.refreshMoneyHud();
        }
    }



}
