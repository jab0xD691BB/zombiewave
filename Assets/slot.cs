using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image bg;
    public Image weaponImg;

    public int slotNr;
    public bool isChoose = false;
    public weapon weapon;

    private RectTransform rectTransform;
    private Vector3 oldPos;

    [SerializeField]
    private Canvas canvas;
    private CanvasGroup cg;



    private Transform can;

    Transform startParent;
    Vector3 startPos;

    private bool isBought = false;

    public itemslot itemslot;

    public shopmanager shopmanager;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        cg = GetComponent<CanvasGroup>();
        if (weapon == null)
        {
            weaponImg.color = new Color(255f, 255f, 255f, 0f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!shopmanager.isShopActive && shopmanager.currentSlotNr != 99)
        {
            Debug.Log("curr slot " + shopmanager.currentSlotNr + " _ " + shopmanager.shopslots[shopmanager.currentSlotNr].weapon.name);

            
           attachWeaponInSlot(shopmanager.shopslots[shopmanager.currentSlotNr].weapon);
        }


        if (weapon != null)
        {
            itemslot.lastPick = slotNr;

            itemslot.currentPick = slotNr;
            isChoose = true;

        }

    }

    void attachWeaponInSlot(weapon w)
    {
        if (/*itemslot.slots[slotNr].weapon == null &&*/ !GameObject.Find("slotbg").GetComponent<itemslot>().isAlreadyInSlot(w) && w.isBought)
        {
            if(weapon != null)
            {
                if (weapon.name.Equals("knife"))
                {
                    return;
                }
            }
            weaponImg.color = new Color(255f, 255f, 255f, 1f);

            weapon = w;
            weaponImg.sprite = w.sprite;

            shopmanager.currentSlotNr = 99;
            itemslot.oldLastPick = -1;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (weapon != null)
        {
            itemslot.currentPick = slotNr;
            itemslot.lastPick = slotNr;
        }
        oldPos = rectTransform.position;

        startPos = transform.position;
        startParent = transform.parent;
        can = GameObject.Find("Slots").transform;
        transform.parent = can;
        cg.blocksRaycasts = false;
        //itemslot.currentDrag = slotNr;

        itemslot.weaponDrag = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("ondrag");
        Debug.Log("oldpos: " + oldPos);

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        cg.alpha = 0.6f;
        transform.localScale = new Vector3(4, 4, 0);


    }


    public void OnEndDrag(PointerEventData eventData)
    {
        cg.alpha = 1f;
        transform.localScale = new Vector3(1, 1, 0);

        cg.blocksRaycasts = true;
        Debug.Log("onenddrag");
        transform.position = oldPos;

        if (transform.parent == can)
        {
            transform.position = startPos;
            transform.SetParent(startParent);

        }

        if (!itemslot.swap)
        {
            Debug.Log("delete");
            itemslot.deleteWeaponInSlot(slotNr);
        }

        itemslot.swap = false;

        itemslot.weaponDrag = false;
        itemslot.oldLastPick = -1; //reload color

    }




}
