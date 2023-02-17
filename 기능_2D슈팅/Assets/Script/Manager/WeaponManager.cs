using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    None, AttackPower, MoveSpped, AttackDelay, Hp
}

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> Weapons;
    public List<Accessory> Accessories;

    private void Start()
    {
        GameManager.Instance.ConnectWeaponManager(this);
    }


    IEnumerator Weapon01()
    {
        yield return null;


    }
}
