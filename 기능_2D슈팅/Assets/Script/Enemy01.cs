using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Unit
{
    public GameObject Player;
    public float CurDelay;

    private void Awake()
    {
        SetType(UnitType.Enemy);
    }

    public void Update()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) > 0.1f)
        {
            CurDelay = 0;

            var Dir = (Player.transform.position - this.transform.position).normalized;
            transform.Translate(Dir * MoveSpeed * Time.deltaTime);
        }

        else
        {
            if (CurDelay > AttackDelay)
            {
                Player.GetComponent<Player>().OnHit(AttackPower);
                CurDelay = 0.0f;
            }

            else
                CurDelay += Time.deltaTime;
        }
    }
}
