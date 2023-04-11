using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Hp : Item
{
    protected override IEnumerator ItemEffect()
    {
        yield return null;

        if (GameMgr.Instance.Hp + 20 < GameMgr.Instance.MaxHp)
            GameMgr.Instance.Hp += 20;

        else
            GameMgr.Instance.Hp = GameMgr.Instance.MaxHp;

        Destroy(this.gameObject);
    }
}
