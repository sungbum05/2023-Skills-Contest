using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    [Header("자식 변수"), SerializeField]
    Transform BulletSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(BossSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAttack == true && CurAttackPatton == null)
        {
            int Ran = Random.Range(0, 3);

            switch (Ran)
            {
                case 0:
                    CurAttackPatton = StartCoroutine(Patton01());
                    break;

                case 1:
                    CurAttackPatton = StartCoroutine(Patton02());
                    break;

                case 2:
                    CurAttackPatton = StartCoroutine(Patton03());
                    break;

                default:
                    break;
            }
        }
    }
    private IEnumerator Patton01()
    {
        yield return null;

        while(transform.position.z != 0)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 0), MoveSpeed * Time.deltaTime);
        }

        for (int j = 0; j < 5; j++)
        {
            int Ran = Random.Range(-1, 2);

            for (float i = 0; i <= 360.0f; i += 360.0f / (AttackCount + Ran))
            {
                GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Bullet.transform.eulerAngles = new Vector3(0, i, 0);

                Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
                Bullet.GetComponent<EnemyBullet>().Damage = Damage;
            }

            yield return new WaitForSeconds(AttackDelay);
        }

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton02()
    {
        yield return null;
        int CurCount = 0;
        float CurTime = 0;

        while (CurCount < 3)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, (GameMgr.Instance.Player.transform.position + new Vector3(0,0,20)), (MoveSpeed * Time.deltaTime * 2));

            CurTime += Time.deltaTime;

            if (CurTime > 1)
            {
                for (int i = -150; i >= -210; i -= 30)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, BulletSpawnPos.position, Quaternion.identity);
                    Bullet.transform.eulerAngles = new Vector3(0, i, 0);

                    Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
                    Bullet.GetComponent<EnemyBullet>().Damage = Damage;
                }

                yield return new WaitForSeconds(AttackDelay);

                CurTime = 0;
                CurCount++;
            }
        }

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton03()
    {
        yield return null;

        while(transform.position != SpawnPos)
        {
            yield return null;

            transform.position = Vector3.MoveTowards(transform.position, SpawnPos, MoveSpeed * Time.deltaTime);
        }

        for (int i = 0; i < AttackCount * 3; i++)
        {
            GameObject Bullet = Instantiate(BulletPrefab, BulletSpawnPos.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce((transform.forward * -1) * BulletSpeed * 2, ForceMode.Impulse);
            Bullet.GetComponent<EnemyBullet>().Damage = Damage;

            yield return new WaitForSeconds(AttackDelay / 15);
            transform.eulerAngles += new Vector3(0, Random.Range(-10.0f, 10.0f), 0);
        }

        transform.eulerAngles = Vector3.zero;

        CurAttackPatton = null;
        yield break;
    }

    protected override void Die()
    {
        IsDie = true;
        StartCoroutine(GameMgr.Instance.UIM.GameSet(GameEnd.Next));

        this.transform.position = new Vector3(0, 10, -38);
        this.transform.eulerAngles = new Vector3(0, 180, 0);

        this.gameObject.SetActive(false);
    }
}
