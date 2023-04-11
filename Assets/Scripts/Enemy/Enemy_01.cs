using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : Enemy
{
    protected override void Start()
    {
        base.Start();
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
                    Bullet.GetComponent<Rigidbody>().AddForce((transform.forward * -1) * BulletSpeed, ForceMode.Impulse);
                    Bullet.GetComponent<EnemyBullet>().Damage = Damage;

                    yield return new WaitForSeconds(AttackDelay / 15);
                }
                yield return new WaitForSeconds(AttackDelay);
            }
        }

        yield break;
    }
}
