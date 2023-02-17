using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    const int BasePlayerSpeed = 5;
    const int BasePlayerAttackPower = 10;
    const float BasePlayerAttackDelay = 1;

    [Header("플레이어 개인 변수")]
    [SerializeField]
    private int Level = default;
    [SerializeField]
    private int CurExp = default;
    [SerializeField]
    private int MaxExp = default;

    [Header("자식 오브젝트 컴포넌트")]
    [SerializeField]
    private WeaponManager WeaponManager;

    private void Awake()
    {
        SetType(UnitType.Player);

        MoveSpeed = BasePlayerSpeed;
        AttackPower = BasePlayerAttackPower;
        AttackDelay = BasePlayerAttackDelay;

        WeaponManager = transform.GetComponentInChildren<WeaponManager>();
    }

    public void Start()
    {
        GameManager.Instance.ConnectPlayer(this);
    }

    private void Update()
    {
        if (CurExp >= MaxExp)
            LevelUp();

        Move();
    }

    private void LevelUp()
    {
        Level++;

        CurExp = 0;
        MaxExp += 100;

        GameManager.Instance.PlayerLevelUp();
    }

    private void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        this.gameObject.transform.position += new Vector3(Horizontal, Vertical, 0) * MoveSpeed * Time.deltaTime;
    }
}
