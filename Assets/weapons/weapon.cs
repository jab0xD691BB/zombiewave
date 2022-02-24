using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "weapon")]
public class weapon : ScriptableObject
{
    public int damage;

    public float firerate;

    public string name;

    public Sprite sprite;
    public Sprite shopAndSlotSprite;

    public int weaponIndex;

    public int price;

    public string handType;

    public string weaponType;

    public bool isBought = false;

    public int currammo;
    public int maxAmmo;
    public int mags;

    public float reloadTime;

    public int ammoPrice;


    public weapon(int d, string n)
    {
        damage = d;
        name = n;
        
    }

    public int gsDamage
    {
        get { return damage; }
        set { damage = value; }
    }
    public string gsName
    {
        get { return name; }
        set { name = value; }
    }

    public Sprite gsSrite
    {
        get { return sprite; }
        set { sprite = value; }
    }
}
