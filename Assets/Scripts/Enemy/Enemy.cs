using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 상속_정보")]
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

    [Header("적 상속_공격")]
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

    protected Coroutine StartAttack;

    [Header("적 상속_이동")]
    [SerializeField]
    protected float MoveSpeed;
    [SerializeField]
    Vector3 StartPos;
    [SerializeField]
    Vector3 EndPos;

    [SerializeField]
    protected bool IsMove = false;

    [Header("적 상속_아이템스폰")]
    public List<GameObject> ItemPool = new List<GameObject>();

    virtual protected void Start()
    {
        Attack();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        MoveTo();

        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Alpha1))
            Hit(500);
    }

    public void GetMoveToPos(Transform EPos)
    {
        StartPos = transform.position;
        EndPos = EPos.position;

        IsMove = true;
    }
    protected void MoveTo()
    {
        if (IsMove == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, EndPos, MoveSpeed * Time.deltaTime);
        }

        if (transform.position == EndPos)
            Die();
    }
    virtual protected void Attack()
    {
        StartAttack = StartCoroutine(AttackPatton());
    }
    virtual protected IEnumerator AttackPatton()
    {
        yield return null;
    }

    public void Hit(float Damage)
    {
        Hp -= Damage;
    }

    public void ItemSpawn()
    {
        int Ran = Random.Range(0, ItemPool.Count);

        Instantiate(ItemPool[Ran], this.transform.position, Quaternion.identity);
    }

    public void Die()
    {
        GameMgr.Instance.KillCount++;
        GameMgr.Instance.Score += 250;

        IsDie = true;
        EnemySpawn.DeleteCurEnemy(this);

        if (Random.Range(-3, 3) >= 0)
        {
            ItemSpawn();
        }

        Destroy(this.gameObject);
    }
}
