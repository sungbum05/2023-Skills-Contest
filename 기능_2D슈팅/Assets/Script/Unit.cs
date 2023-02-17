using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None, Player, Enemy
}

public class Unit : MonoBehaviour
{
    [Header("À¯´Ö Á¤º¸")]
    [SerializeField]
    protected int hp;
    public int HP
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }
    [SerializeField]
    protected UnitType UnitType;
    [SerializeField]
    protected int MoveSpeed;
    [SerializeField]
    protected int AttackPower;
    [SerializeField]
    protected float AttackDelay;


    public void SetType(UnitType Type)
    {
        this.gameObject.tag = Type.ToString();
        this.UnitType = Type;
    }
    public void OnHit(int Damage)
    {
        this.HP -= Damage;
    }
    public void OnDie()
    {
        Destroy(this.gameObject);
    }
}
