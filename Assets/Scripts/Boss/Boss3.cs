using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : Boss
{
    [Header("자식 변수"), SerializeField]
    private GameObject BazierBullet;
    [SerializeField]
    private Transform BazierSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(BossSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttack == true && CurAttackPatton == null)
        {
            int Ran = Random.Range(0, 2);

            switch (Ran)
            {
                case 0:
                    CurAttackPatton = StartCoroutine(Patton01());
                    break;

                case 1:
                    CurAttackPatton = StartCoroutine(Patton02());
                    break;

                default:
                    break;
            }
        }
    }
    private IEnumerator Patton01()
    {
        yield return null;

        while (transform.position != Vector3.zero)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, MoveSpeed * Time.deltaTime);
        }

        for (int j = 0; j < 360; j += 10)
        {
            int Ran = Random.Range(-1, 2);

            for (float i = 0; i <= 360.0f; i += 360.0f / AttackCount)
            {
                GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Bullet.transform.eulerAngles = new Vector3(0, i + j, 0);

                Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * BulletSpeed, ForceMode.Impulse);
                Bullet.GetComponent<EnemyBullet>().Damage = Damage;
            }

            yield return new WaitForSeconds(AttackDelay / 15);
        }

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton02()
    {
        yield return null;

        while (transform.position != SpawnPos)
        {
            yield return null;

            transform.position = Vector3.MoveTowards(transform.position, SpawnPos, MoveSpeed * Time.deltaTime);
        }

        for (int i = 0; i < AttackCount * 10; i++)
        {
            Debug.Log("Ins");

            GameObject Bullet = Instantiate(BazierBullet, BazierSpawnPos.position, Quaternion.identity);

            Bullet.GetComponent<EnemyBazierBullet>().Init(transform, GameMgr.Instance.Player.transform, BulletSpeed / 50, 15, 15);

            yield return new WaitForSeconds(AttackDelay / 15);
        }

        CurAttackPatton = null;
        yield break;
    }
    private IEnumerator Patton03() // 보스 2 패턴 3 보류
    {
        yield return null;
        

        CurAttackPatton = null;
        yield break;
    } // 보류

    protected override void Die()
    {
        IsDie = true;
        StartCoroutine(GameMgr.Instance.UIM.GameSet(GameEnd.Clear));

        this.transform.position = new Vector3(0, 10, -38);
        this.transform.eulerAngles = new Vector3(0, 180, 0);

        this.gameObject.SetActive(false);
    }
}
