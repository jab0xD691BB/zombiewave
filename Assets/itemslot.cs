using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class itemslot : MonoBehaviour
{
    //public GameObject bg;
    public int i = 0;
    public List<Image> bg;
    public List<slot> slots;
    public int lastPick;

    public int currentDrag = 99;
    public int swapNr = 99;

    public int oldLastPick = -1;

    public int currentPick;
    public PlayerScript playerScript;

    public bool swap = false;

    shopmanager shopmanager;

    public bool weaponDrag = false;

    private void Start()
    {
        slots[0].isChoose = true;
        slots[0].weaponImg.sprite = slots[0].weapon.shopAndSlotSprite;
        lastPick = slots[0].weapon.weaponIndex;

        slots[3].weaponImg.sprite = slots[3].weapon.shopAndSlotSprite;

        shopmanager = slots[0].shopmanager;


    }

    private void Update()
    {

        if (weaponDrag)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].bg.color = new Color32(0, 255, 255, 34);
            }
        }


        if (!shopmanager.isShopActive && shopmanager.currentSlotNr != 99 && !weaponDrag)
        {
            for (int i = 0; i < slots.Count - 1; i++)
            {
                if (isAlreadyInSlot(shopmanager.shopslots[shopmanager.currentSlotNr].weapon))
                {
                    slots[i].bg.color = new Color32(255, 255, 255, 34);
                    oldLastPick = -1;
                    continue;
                }
                if (shopmanager.shopslots[shopmanager.currentSlotNr].weapon.isBought)
                {
                    slots[i].bg.color = new Color32(173, 255, 47, 34);

                }



            }
        }



        if (Input.GetKey(KeyCode.Alpha2))
        {
            //gameObject.transform.Find("weapon").GetComponent<SpriteRenderer>().sprite = weapon.sprite;
            //bg[0].color = Color.black;
        }

        if (lastPick != oldLastPick)
        {

            slots[lastPick].bg.color = new Color32(0, 255, 0, 34);
            if (slots[currentPick].weapon != null)
            {
                playerScript.currentWeaponIndex = slots[currentPick].weapon.weaponIndex;
            }
            oldLastPick = lastPick;
            for (int i = 0; i < slots.Count; i++)
            {
                if (i == lastPick)
                {
                    continue;

                }

                slots[i].bg.color = new Color32(255, 255, 255, 34);

            }
        }




    }

    public bool isAlreadyInSlot(weapon w)
    {
        bool isequip = false;
        for (int i = 0; i < slots.Count; i++)
        {

            if (slots[i].weapon == w)
            {
                isequip = true;
            }
        }
        //Debug.Log("isAlreadyInSlot : " + isequip);
        return isequip;
    }

    public int whichSlot(int lp)
    {

        return 1;
    }

    public void addInSlots(slot s, int i)
    {
        slots[i] = s;
    }


    public void swapWeaponInSlot(int currentDrag, int slot)
    {
        if (slots[slot].weapon != null)
        {
            weapon tmp = slots[slot].weapon;
            slots[slot].weapon = slots[currentDrag].weapon;
            slots[slot].weaponImg.sprite = slots[slot].weapon.shopAndSlotSprite;

            slots[currentDrag].weapon = tmp;
            slots[currentDrag].weaponImg.sprite = slots[currentDrag].weapon.shopAndSlotSprite;
        }
        else
        {

            slots[slot].weapon = slots[currentDrag].weapon;
            slots[slot].weaponImg.sprite = slots[slot].weapon.shopAndSlotSprite;
            slots[slot].weaponImg.color = new Color(1f, 1f, 1f, 1f);

            slots[currentDrag].weapon = null;
            slots[currentDrag].weaponImg.color = new Color(1f, 1f, 1f, 0f);
        }




    }

    public void deleteWeaponInSlot(int nr)
    {

        if (slots[nr].weapon.name == "knife")
        {
            Debug.Log("u cant delete this");
            return;
        }

        int weaponSize = 0;
        int lastIndexWeapon = 0;
        foreach (slot s in slots)
        {
            if (s.weapon != null)
            {
                weaponSize++;
                lastIndexWeapon = s.slotNr;
            }
        }

        if (weaponSize == 1)
        {
            return;
        }

        slots[nr].weapon = null;
        slots[nr].weaponImg.color = new Color(1f, 1f, 1f, 0f);

        lastPick = lastIndexWeapon;
        currentPick = lastIndexWeapon;

        Debug.Log("wsize: " + weaponSize);
    }

    public void refreshSlots()
    {
        foreach (slot s in slots)
        {
            s.weaponImg.sprite = s.weapon.shopAndSlotSprite;
        }
    }

    /*
    * 
    * melee slot x -178.9 / y -117.5
    * 
    * slot 1 x -100 / y -117.5
    * 
    * slot 2 x 18.5 / y -117.5
    * 
    * slot3 x 124.5 / y -117.5
   */
    
    //Gameobject als Position benutzen statt hardcode Vectoren?
    Vector2 slotMeleePos;
    Vector2 slotMeleeNewPos = new Vector2(-280f, -100f);

    Vector2 slot1Pos;
    Vector2 slot1NewPos = new Vector2(-180f, -100f);

    Vector2 slot2Pos;
    Vector2 slot2NewPos = new Vector2(-80f, -100f);

    Vector2 slot3Pos;
    Vector2 slot3NewPos = new Vector2(0f, -100f);

    public void moveSlots()
    {


        slotMeleePos = slots[3].transform.parent.transform.localPosition;
        slot1Pos = slots[0].transform.parent.transform.localPosition;
        slot2Pos = slots[1].transform.parent.transform.localPosition;
        slot3Pos = slots[2].transform.parent.transform.localPosition;

        slots[3].transform.parent.transform.localPosition = slotMeleeNewPos;
        slots[0].transform.parent.transform.localPosition = slot1NewPos;
        slots[1].transform.parent.transform.localPosition = slot2NewPos;
        slots[2].transform.parent.transform.localPosition = slot3NewPos;

        slotMeleeNewPos = slotMeleePos;
        slot1NewPos = slot1Pos;
        slot2NewPos = slot2Pos;
        slot3NewPos = slot3Pos;

    }


    public void setDefaultColor()
    {
        for (int i = 0; i < slots.Count - 1; i++)
        {
            if (slots[i].weapon != null && (slots[i].weapon.name != playerScript.currWeapon.name))
            {
                slots[i].bg.color = new Color32(255, 255, 255, 34);
            }
            else
            {
                slots[i].bg.color = new Color32(0, 255, 0, 34);
            }

        }
    }


}


