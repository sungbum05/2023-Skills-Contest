using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable/Weapon")]
public class Weapon : ScriptableObject
{
    public int Number;
    public string Name;
    public GameObject Prefab;
    public Sprite Image;
    public int Level;
    public Coroutine WeaponPatton;
}
