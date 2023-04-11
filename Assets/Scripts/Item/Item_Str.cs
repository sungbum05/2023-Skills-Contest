using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Str : Item
{
    protected override IEnumerator ItemEffect()
    {
        yield return null;

        if (GameMgr.Instance.StrUpLevel < GameMgr.Instance.MaxSTR)
            GameMgr.Instance.StrUpLevel++;

        else
            GameMgr.Instance.Score += 500;

        Destroy(this.gameObject);
    }
}
