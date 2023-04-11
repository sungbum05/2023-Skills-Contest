using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Item
{
    protected override IEnumerator ItemEffect()
    {
        yield return null;

        if (GameMgr.Instance.IsShield == true)
            GameMgr.Instance.CurShieldTime = GameMgr.Instance.MaxShieldTime;

        else
        {
            GameMgr.Instance.IsShield = true;
            GameMgr.Instance.CurShieldTime = GameMgr.Instance.MaxShieldTime;
        }

        Destroy(this.gameObject);
    }
}
