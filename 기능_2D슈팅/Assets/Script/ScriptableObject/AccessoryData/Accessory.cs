using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AccessoryData", menuName = "Scriptable/Accessory")]
public class Accessory : ScriptableObject
{
    public int Number;
    public string Name;
    public int Level;
    public Sprite Image;
    public StatType UpgreadStat;
    public int UpgreadValue;
}
