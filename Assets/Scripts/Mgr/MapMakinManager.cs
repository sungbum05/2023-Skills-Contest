using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakinManager : MonoBehaviour
{
    public static MapMakinManager Instance;

    [Header("운석 생성")]
    [SerializeField]
    private Transform SpawnPosZip;
    [SerializeField]
    private List<Transform> AstroSpawnPos;
    [SerializeField]
    private int CurLeft, CurRight;
    [SerializeField]
    private int MaxLeft, MaxRight;
    [SerializeField]
    private float SpawnDelay;
    [SerializeField]
    private bool IsStart = false;

    [Header("오브젝트 풀")]
    public GameObject PoolPrefab;
    public Transform PoolingObjParant;

    public Queue PoolObj = new Queue();

    // Start is called before the first frame update
    void Start()
    {
        BasicSetting();

        StartCoroutine(StartAstroWall());
    }
   
    IEnumerator StartAstroWall()
    {
        yield return null;

        IsStart = true;
        int NextPos = 0;

        while(IsStart)
        {
            if(CurLeft <= MaxLeft)
            {
                NextPos = Random.Range(0, 2);
            }

            else if(CurRight >= MaxRight)
            {
                NextPos = Random.Range(-1, 1);
            }

            else
            {
                NextPos = Random.Range(-1, 2);
            }

            LeftSpawn(NextPos);
            RightSpawn(NextPos);

            yield return new WaitForSeconds(SpawnDelay);
        }

        yield break;
    }

    public void StopAstroWall()
    {
        IsStart = false;
    }

    private void LeftSpawn(int Next)
    {
        CurLeft += Next;

        GameObject Astro = GetObj();

        Astro.transform.position = AstroSpawnPos[CurLeft].position;
    }

    private void RightSpawn(int Next)
    {
        CurRight += Next;

        GameObject Astro = GetObj();

        Astro.transform.position = AstroSpawnPos[CurRight].position;
    }

    public void BasicSetting()
    {
        if (Instance == null)
            Instance = this;

        foreach (Transform item in SpawnPosZip)
        {
            AstroSpawnPos.Add(item);
        }

        Initilrize(10);
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
        GameObject NewObj = Instantiate(PoolPrefab);

        NewObj.transform.SetParent(PoolingObjParant);
        NewObj.transform.position = Vector3.zero;
        NewObj.SetActive(false);

        return NewObj;
    }

    public static GameObject GetObj()
    {
        GameObject Obj = null;

        if (MapMakinManager.Instance.PoolObj.Count > 0)
        {
            Obj = MapMakinManager.Instance.PoolObj.Dequeue() as GameObject;

            Obj.transform.SetParent(null);
            Obj.transform.position = Vector3.zero;
            Obj.SetActive(true);
        }

        else
        {
            Obj = MapMakinManager.Instance.CreateNewObj();

            Obj.transform.SetParent(null);
            Obj.transform.position = Vector3.zero;
            Obj.SetActive(true);
        }

        return Obj;
    }

    public static void ReturnObj(GameObject ReturnObj)
    {
        ReturnObj.transform.SetParent(MapMakinManager.Instance.PoolingObjParant);
        ReturnObj.transform.position = Vector3.zero;
        ReturnObj.SetActive(false);

        MapMakinManager.Instance.PoolObj.Enqueue(ReturnObj);
    }

    #endregion
}
