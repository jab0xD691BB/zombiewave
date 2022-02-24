using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    itemslot itemslot;
    slot slot;

    private void Awake()
    {
        itemslot = GetComponentInParent<itemslot>();
        slot = GetComponentInChildren<slot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            itemslot.swap = true;

            itemslot.lastPick = slot.slotNr;
            itemslot.currentPick = slot.slotNr;

            if (eventData.pointerDrag.gameObject.GetComponent<slot>() != null)
                itemslot.swapWeaponInSlot(eventData.pointerDrag.gameObject.GetComponent<slot>().slotNr, slot.slotNr);

        }
    }
}
