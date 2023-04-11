using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [Header("오브젝트 풀")]
    public Transform PoolingObjParant;
    public Queue PoolObj = new Queue();

    [Header("Move")]
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private Vector2 MaxPos;
    [SerializeField]
    private Vector2 MinPos;

    [Header("Attack_GunRot")]
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private int CurRot;
    [SerializeField]
    private float RotSpeed;

    [Header("Attack_Fire")]
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private List<Transform> FirePos;
    [SerializeField]
    private float Force;
    [SerializeField]
    private float CurAttackDelay;
    [SerializeField]
    private float MaxAttackDelay;

    [Header("Skill")]
    public List<Skill> Skills = new List<Skill>();
    public GameObject Shield;

    void Start()
    {
        GameMgr.Instance.InputPlayer(this.gameObject);

        if (Instance == null)
            Instance = this;

        Initilrize(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.IsStart == true)
        {
            Move();
            GunRotation();

            if(GameMgr.Instance.IsShield == true)
            {
                Shield.SetActive(true);
                Shield.transform.localScale = Vector3.one * 11;
            }

            else
            {
                Shield.SetActive(false);
                Shield.transform.localScale = Vector3.one;
            }
        }
    }

    void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(Horizontal, 0, Vertical) * (MoveSpeed + (GameMgr.Instance.AgiUpLevel * 2.5f)) * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, MinPos.x, MaxPos.x), 
            transform.position.y, 
            Mathf.Clamp(this.transform.position.z, MinPos.y, MaxPos.y));
    }

    void GunRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            CurRot = 0;
            Fire();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CurRot = -1;
            Fire();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CurRot = 1;
            Fire();
        }
        else
            CurRot = 0;

        Gun.transform.Rotate(Vector3.up * CurRot * RotSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if (CurAttackDelay >= 0)
            CurAttackDelay -= Time.deltaTime;

        else
        {
            CurAttackDelay = MaxAttackDelay;

            for (int i = 0; i <= GameMgr.Instance.StrUpLevel; i++)
            {
                GameObject Bullet = Instantiate(BulletPrefab);

                Bullet.transform.position = FirePos[GameMgr.Instance.StrUpLevel].transform.GetChild(i).position;

                Bullet.GetComponent<Rigidbody>().AddForce(Gun.transform.forward * Force, ForceMode.Impulse);
            }
        }
    }

    #region ObjPool
    public void Initilrize(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            PoolObj.Enqueue(CreateNewObj());
        }
    }

    public GameObject CreateNewObj()
    {
        GameObject NewObj = Instantiate(BulletPrefab);

        NewObj.transform.SetParent(PoolingObjParant);
        NewObj.transform.position = Vector3.zero;
        NewObj.SetActive(false);

        return NewObj;
    }

    public static GameObject GetObj()
    {
        GameObject Obj = null;

        if (Player.Instance.PoolObj.Count > 0)
        {
            Obj = Player.Instance.PoolObj.Dequeue() as GameObject;

            Obj.transform.SetParent(null);
            Obj.transform.position = Vector3.zero;
            Obj.SetActive(true);
        }

        else
        {
            Obj = Player.Instance.CreateNewObj();

            Obj.transform.SetParent(null);
            Obj.transform.position = Vector3.zero;
            Obj.SetActive(true);
        }

        return Obj;
    }

    public static void ReturnObj(GameObject ReturnObj)
    {
        ReturnObj.transform.SetParent(Player.Instance.PoolingObjParant);
        ReturnObj.transform.position = Vector3.zero;
        ReturnObj.SetActive(false);

        Player.Instance.PoolObj.Enqueue(ReturnObj);
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameMgr.Instance.Hp -= 10;
        }

        else if (other.CompareTag("Boss"))
        {
            GameMgr.Instance.Hp -= 10;
        }
    }
}
