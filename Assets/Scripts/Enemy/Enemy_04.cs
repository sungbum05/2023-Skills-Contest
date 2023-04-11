using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        base.Attack();
    }
    protected override IEnumerator AttackPatton()
    {
        base.AttackPatton();

        while (IsDie == false)
        {
            yield return null;

            if (IsMove && GameMgr.Instance.Player != null)
            {
                for (int i = 0; i < AttackCount; i++)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

                    Bullet.GetComponent<EnemyBazierBullet>().Init(transform, GameMgr.Instance.Player.transform, BulletSpeed, 30, 30);
                }
                yield return new WaitForSeconds(AttackDelay);
            }
        }

        yield break;
    }
}
