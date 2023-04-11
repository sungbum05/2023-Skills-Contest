using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("보스 상속_정보")]
    [SerializeField]
    public float MaxHp;
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;

            if (hp <= 0)
                Die();
        }
    }
    [SerializeField]
    public bool IsDie = false;

    [Header("보스 상속_공격")]
    [SerializeField]
    protected bool IsAttack = false;
    [SerializeField]
    protected GameObject BulletPrefab;
    [SerializeField]
    protected float AttackDelay;
    [SerializeField]
    protected float AttackCount;
    [SerializeField]
    protected float Damage;
    [SerializeField]
    protected float BulletSpeed;

    protected Coroutine CurAttackPatton;

    [Header("적 상속_이동")]
    [SerializeField]
    protected Vector3 SpawnPos;
    [SerializeField]
    protected float MoveSpeed;
    protected Coroutine BossSpawnCorutine;

    private void Start()
    {
        //StartCoroutine(BossSpawnIEnum());
    }

    public void BossSpawn()
    {
        BossSpawnCorutine = StartCoroutine(BossSpawnIEnum());
    }


    public IEnumerator BossSpawnIEnum() //보스 출연 연출
    {
        yield return new WaitForSeconds(0.5f);

        GameMgr.Instance.UIM.StartPanfadeIn(1.0f);

        yield return null;
        while(this.transform.position.z < 38.0f)
        {
            yield return null;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, 38.0f), MoveSpeed * Time.deltaTime);
        }

        transform.position = SpawnPos;
        transform.eulerAngles = Vector3.zero;
        GameMgr.Instance.UIM.BossHpBar.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        GameMgr.Instance.UIM.StartPanFadeOut(0.0f);

        yield return new WaitForSeconds(1.0f);
        IsAttack = true;
    }

    public void Hit(float Damage)
    {
        Hp -= Damage;
    }

    virtual protected void Die()
    {
        

    }
}
