using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPos
{
    Top_0,
    Top_1,
    Top_2,
    Top_3,
    LSide_0,
    LSide_1,
    LSide_2,
    RSide_0,
    RSide_1,
    RSide_2,
    Bottom_0,
    Bottom_1,
    Bottom_2,
    Bottom_3
}
public enum EnemyType
{
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4,
    Astro
}

[System.Serializable]
public class EnemyData
{
    public SpawnPos From;
    public SpawnPos To;
    public EnemyType EnemyType;
    public float NextSpawnDelay;
}

public class EnemySpawn : MonoBehaviour
{
    public Transform SpawnPosZip;
    public GameObject[] EnemyArray;

    public Dictionary<SpawnPos, Transform> PosDic = new Dictionary<SpawnPos, Transform>();
    public Dictionary<EnemyType, GameObject> EnemyTypeDic = new Dictionary<EnemyType, GameObject>();

    public List<EnemyData> SpawnPatton = new List<EnemyData>();

    public static List<Enemy> CurSpawnEnemy = new List<Enemy>();

    public Coroutine EnemySpawnCorutine;

    // Start is called before the first frame update
    void Start()
    {
        GameMgr.Instance.InputEnemySpawner();
        BasicSetting();

        //StartCoroutine(StartSpawn());
    }

    void BasicSetting()
    {
        for (int i = 0; i < SpawnPosZip.childCount; i++)
        {
            PosDic.Add((SpawnPos)i, SpawnPosZip.GetChild(i));
        }

        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyTypeDic.Add((EnemyType)i, EnemyArray[i]);
        }
    }

    public void StartSpawnEnemy()
    {
        EnemySpawnCorutine = StartCoroutine(StartSpawn());
    }

    public void StopEnemySpawn()
    {
        StopCoroutine(EnemySpawnCorutine);
    }

    private IEnumerator StartSpawn()
    {
        yield return null;
        for (int i = 0; i < SpawnPatton.Count; i++)
        {
            GameObject Enemy = Instantiate(EnemyTypeDic[SpawnPatton[i].EnemyType], transform.position, Quaternion.identity);

            Enemy.transform.position = PosDic[SpawnPatton[i].From].position;

            Enemy.GetComponent<Enemy>().GetMoveToPos(PosDic[SpawnPatton[i].To]);
            AddCurEnemy(Enemy.GetComponent<Enemy>());

            yield return new WaitForSeconds(SpawnPatton[i].NextSpawnDelay);

            if (GameMgr.Instance.IsBossStage == true)
                break;
        }

        yield break;
    }

    public static void ClearCurEnemy()
    {
        CurSpawnEnemy.Clear();
    }

    public static void AddCurEnemy(Enemy enemy)
    {
        CurSpawnEnemy.Add(enemy);
        Debug.Log(CurSpawnEnemy.Count);
    }

    public static void DeleteCurEnemy(Enemy enemy)
    {
        CurSpawnEnemy.Remove(enemy);
        Debug.Log(CurSpawnEnemy.Count);
    }
}
