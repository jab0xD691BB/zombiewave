using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopmanager : MonoBehaviour
{
    public List<shopslot> shopslots;
    public int currentSlotNr = 99;
    public GameObject panel;
    public bool isShopActive;
    public GameObject popupbuy;
    bool buyActive;

    public itemslot itemslot;

    int oldPick = -1;

    public Button exitButton;

    private void Awake()
    {

        //im scriptable waffe initen beim playerscript code und unity, shopslot unity ui und hier 

        shopslots[0].GetComponent<Image>().sprite = shopslots[0].weapon.shopAndSlotSprite;
        shopslots[0].bg.color = new Color(150, 150, 150);
        shopslots[0].inner.color = new Color(0, 255, 188);

        shopslots[1].GetComponent<Image>().sprite = shopslots[1].weapon.shopAndSlotSprite;
        shopslots[1].bg.color = new Color(150, 150, 150);

        shopslots[2].GetComponent<Image>().sprite = shopslots[2].weapon.shopAndSlotSprite;
        shopslots[2].bg.color = new Color(150, 150, 150);

        shopslots[3].GetComponent<Image>().sprite = shopslots[3].weapon.shopAndSlotSprite;
        shopslots[3].bg.color = new Color(150, 150, 150);

        shopslots[4].GetComponent<Image>().sprite = shopslots[4].weapon.shopAndSlotSprite;
        shopslots[4].bg.color = new Color(150, 150, 150);

        panel.SetActive(false);

        GameObject.Find("shopPanel");

    }

    private void Update()
    {
        if (panel != null || oldPick != currentSlotNr)
        {
            //Debug.Log("currentslot in shop: " + currentSlotNr);

            for (int i = 0; i < shopslots.Count; i++)
            {
                //if (currentSlotNr != 99)
                //{
                if (currentSlotNr == i)
                {
                    shopslots[i].bg.color = Color.green;

                }
                else
                {
                    shopslots[i].bg.color = Color.grey;
                }

                if (shopslots[i].weapon.isBought)
                {
                    shopslots[i].inner.color = new Color32(80, 118, 117,255);
                    shopslots[i].weaponImg.color = Color.white;

                }
                else {
                    
                    shopslots[i].inner.color = new Color32(45, 65, 65, 255);
                    shopslots[i].weaponImg.color = new Color(0.1f,0.1f,0.1f);
                }

                if (shopslots[i].weapon.price <= GameObject.Find("player").GetComponent<PlayerScript>().money &&
                    !shopslots[i].weapon.isBought)
                {
                    shopslots[i].priceText.color = Color.green;

                }
                else
                {
                    shopslots[i].priceText.color = Color.white;
                }


                //}
            }

            oldPick = currentSlotNr;
        }


    }


    public void openShop()
    {

        if (panel != null)
        {
            isShopActive = panel.activeSelf;
            panel.SetActive(!isShopActive);
            itemslot.moveSlots();

        }
        
        currentSlotNr = 99;
        itemslot.oldLastPick = -1;

    }




    public void showPopUp()
    {
        if (popupbuy != null)
        {
            buyActive = popupbuy.activeSelf;
            popupbuy.SetActive(!buyActive);
            GameObject.Find("scrollBg").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }


    }
    public void buyYes()
    {

        shopslots[currentSlotNr].weapon.isBought = true;
        GameObject.Find("player").GetComponent<PlayerScript>().money -= shopslots[currentSlotNr].weapon.price;
        shopslots[currentSlotNr].inner.color = new Color(0, 255, 188);
        buyActive = popupbuy.activeSelf;
        popupbuy.SetActive(!buyActive);
        GameObject.Find("scrollBg").GetComponent<CanvasGroup>().blocksRaycasts = true;
        oldPick = -1;

    }
    public void buyNo()
    {
        shopslots[currentSlotNr].weapon.isBought = false;

        buyActive = popupbuy.activeSelf;
        popupbuy.SetActive(!buyActive);
        GameObject.Find("scrollBg").GetComponent<CanvasGroup>().blocksRaycasts = true;


    }

    public void closeShop()
    {
        Debug.Log("button click test");
        if (panel != null)
        {
            isShopActive = panel.activeSelf;
            panel.SetActive(!isShopActive);
            itemslot.moveSlots();

        }

        itemslot.setDefaultColor();

    }

   
    
}
