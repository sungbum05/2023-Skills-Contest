using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : Skill
{
    public GameObject Bomb;

    protected override IEnumerator SkillEffect()
    {
        GameMgr.Instance.SoundList[1].Play();

        yield return null;
        Bomb.SetActive(true);

        while(Bomb.transform.localScale.x <= 60)
        {
            yield return null;
            Bomb.transform.localScale += Vector3.one * 150 * Time.deltaTime;
        }

        yield return new WaitForSeconds(2.0f);

        while (Bomb.transform.localScale.x >= 1)
        {
            yield return null;
            Bomb.transform.localScale -= Vector3.one * 150 * Time.deltaTime;
        }

        Bomb.transform.localScale = Vector3.zero;
        Bomb.SetActive(false);
        yield break;
    }
}
