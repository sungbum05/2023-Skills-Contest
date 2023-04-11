using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSkill : Skill
{
    [SerializeField]
    private GameObject Particle;

    protected override void UseSkillEffct()
    {
        base.UseSkillEffct();
    }

    protected override IEnumerator SkillEffect()
    {
        yield return null;
        GameMgr.Instance.SoundList[2].Play();

        Particle.SetActive(true);

        if (GameMgr.Instance.Hp + 20 < GameMgr.Instance.MaxHp)
            GameMgr.Instance.Hp += 20;

        else
            GameMgr.Instance.Hp = GameMgr.Instance.MaxHp;

        yield return new WaitForSeconds(1.0f);

        Particle.SetActive(false);
        yield break;
    }
}
