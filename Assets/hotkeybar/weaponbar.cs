using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newbar", menuName = "bar")]
public class weaponbar : ScriptableObject
{
    public Color bgColor;
    public Color innerColor;
    public weapon weapon;

    public GameObject go;
}
