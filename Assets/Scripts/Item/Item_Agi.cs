using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Agi : Item
{
    protected override IEnumerator ItemEffect()
    {
        yield return null;

        if (GameMgr.Instance.AgiUpLevel < GameMgr.Instance.MaxAGI)
            GameMgr.Instance.AgiUpLevel++;
        else
            GameMgr.Instance.Score += 500;

        Destroy(this.gameObject);
    }
}
