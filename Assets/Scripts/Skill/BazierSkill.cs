using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierSkill : Skill
{
    [SerializeField]
    private GameObject BazierBullet;
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private float Stpow;
    [SerializeField]
    private float Edpow;

    protected override void Update()
    {
        if (CurCool > 0)
        {
            CurCool -= Time.deltaTime;
        }

        if (Input.GetKeyDown(UseSkillBtn) && CurCool <= 0 && SkillCount > 0 && EnemySpawn.CurSpawnEnemy.Count > 0)
        {
            UseSkillEffct();
        }
    }

    protected override void UseSkillEffct()
    {
        UseSkill = StartCoroutine(SkillEffect());
    }

    protected override IEnumerator SkillEffect()
    {
        yield return null;
        while (SkillCount > 0)
        {
            GameObject Bazier = Instantiate(BazierBullet);

            int TargetNum = Random.Range(0, EnemySpawn.CurSpawnEnemy.Count);
            Bazier.GetComponent<BazierBullet>().Init(transform, EnemySpawn.CurSpawnEnemy[TargetNum].transform, BulletSpeed, Stpow, Edpow);

            SkillCount--;
            yield return new WaitForSeconds(MaxCool);
        }
        yield break;
    }
}
