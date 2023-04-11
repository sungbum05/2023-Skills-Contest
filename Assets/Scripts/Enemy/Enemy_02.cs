using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : Enemy
{
    [Header("개인 속성")]
    [SerializeField]
    private Transform[] SpawnPos = new Transform[2];

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
                for (int i = 0; i < SpawnPos.Length; i++)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, SpawnPos[i].transform.position, Quaternion.identity);
                    Bullet.transform.LookAt(GameMgr.Instance.Player.transform.position);

                    Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
                    Bullet.GetComponent<EnemyBullet>().Damage = Damage;
                }
                yield return new WaitForSeconds(AttackDelay);
            }
        }

        yield break;
    }
}
