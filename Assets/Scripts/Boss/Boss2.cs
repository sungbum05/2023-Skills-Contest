using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
{
    //[Header("자식 변수"), SerializeField]


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
            int Ran = 2; //Random.Range(0, 3);

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
        List<Rigidbody> ForceList = new List<Rigidbody>();

        for (int i = 0; i < 3; i++)
        {
            float RanX = Random.RandomRange(-20.0f, 20.0f);
            float RanZ = Random.RandomRange(-20.0f, 20.0f);

            transform.position = new Vector3(RanX, transform.position.y, RanZ);

            for (float j = 0; j <= 360.0f; j += 360.0f / (AttackCount))
            {
                GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Bullet.transform.eulerAngles = new Vector3(0, j, 0);

                ForceList.Add(Bullet.GetComponent<Rigidbody>());
                Bullet.GetComponent<EnemyBullet>().Damage = Damage;
            }

            yield return new WaitForSeconds(0.5f);
        }

        transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.5f);

        foreach  (Rigidbody bullet in ForceList)
        {
            bullet.AddForce(bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(1.0f);

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton02()
    {
        yield return null;

        while (transform.position.z != 0)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 0), MoveSpeed * Time.deltaTime);
        }

        for (int i = 0; i < AttackCount * 5; i++)
        {
            GameObject Bullet1 = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Bullet1.GetComponent<Rigidbody>().AddForce((transform.forward * -1) * BulletSpeed * 2, ForceMode.Impulse);
            Bullet1.GetComponent<EnemyBullet>().Damage = Damage;

            GameObject Bullet2 = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Bullet2.GetComponent<Rigidbody>().AddForce((transform.forward) * BulletSpeed * 2, ForceMode.Impulse);
            Bullet2.GetComponent<EnemyBullet>().Damage = Damage;

            GameObject Bullet3 = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Bullet3.GetComponent<Rigidbody>().AddForce((transform.right * -1) * BulletSpeed * 2, ForceMode.Impulse);
            Bullet3.GetComponent<EnemyBullet>().Damage = Damage;

            GameObject Bullet4 = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Bullet4.GetComponent<Rigidbody>().AddForce((transform.right) * BulletSpeed * 2, ForceMode.Impulse);
            Bullet4.GetComponent<EnemyBullet>().Damage = Damage;

            yield return new WaitForSeconds(AttackDelay / 15);
            transform.eulerAngles += new Vector3(0, Random.Range(-10.0f, 10.0f), 0);
        }

        transform.eulerAngles = Vector3.zero;

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton03() // 보스 2 패턴 3 보류
    {
        yield return null;
        float CurCool = 0;
        Vector3 Distance = Vector3.zero;

        for (int i = 0; i < 3; i++)
        {
            while (transform.position != Vector3.zero)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, MoveSpeed * Time.deltaTime);
            }

            while (true)
            {
                yield return null;
                transform.LookAt((GameMgr.Instance.Player.transform.position) * -1);
                Distance = (GameMgr.Instance.Player.transform.position - transform.position).normalized;

                if (CurCool > 1)
                {
                    CurCool = 0;
                    break;
                }

                else
                    CurCool += Time.deltaTime;
            }

            while(true)
            {
                yield return null;

                this.transform.Translate(Vector3.back * MoveSpeed * 2 * Time.deltaTime);

                if (CurCool > 0.5)
                {
                    transform.eulerAngles = Vector3.zero;
                    CurCool = 0;

                    break;
                }

                else
                    CurCool += Time.deltaTime;
            }
        }

        CurAttackPatton = null;
        yield break;
    } // 보류

    protected override void Die()
    {
        IsDie = true;
        StartCoroutine(GameMgr.Instance.UIM.GameSet(GameEnd.Next));

        this.transform.position = new Vector3(0, 10, -38);
        this.transform.eulerAngles = new Vector3(0, 180, 0);

        this.gameObject.SetActive(false);
    }
}
