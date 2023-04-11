using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Stamina : Item
{
    protected override IEnumerator ItemEffect()
    {
        yield return null;

        if (GameMgr.Instance.Stamina + 15 < GameMgr.Instance.MaxStamina)
            GameMgr.Instance.Stamina += 15;

        else
            GameMgr.Instance.Stamina = GameMgr.Instance.MaxStamina;

        Destroy(this.gameObject);
    }
}
