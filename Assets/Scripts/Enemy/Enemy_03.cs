using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03 : Enemy
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
                for (float i = 0; i <= 360.0f; i += 360.0f / AttackCount)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                    Bullet.transform.eulerAngles = new Vector3(0, i, 0);

                    Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
                    Bullet.GetComponent<EnemyBullet>().Damage = Damage;
                }
                yield return new WaitForSeconds(AttackDelay);
            }
        }

        yield break;
    }
}
