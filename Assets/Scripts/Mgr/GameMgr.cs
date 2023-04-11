using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class RankingData
{
    public string Name;
    public int Score;
}

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;

    public GameObject Player = null;
    public UIManager UIM = null;
    public EnemySpawn EnemySpawner = null;

    public List<AudioSource> SoundList = new List<AudioSource>();

    #region 프로퍼티(힘, 민첩, 내구성, 연료)
    //무기 업그레이드
    [SerializeField]
    private int struplevel;
    public int StrUpLevel
    {
        get
        {
            return struplevel;
        }

        set
        {
            struplevel = value;
        }
    }

    //민첩 업그레이드
    [SerializeField]
    private int agiuplevel;
    public int AgiUpLevel
    {
        get
        {
            return agiuplevel;
        }

        set
        {
            agiuplevel = value;
        }
    }

    //내구도
    [SerializeField]
    private float hp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {

            if (IsGameOver == false)
            {
                if (IsShield == true)
                    return;
                else
                {
                    float OriHp = hp;

                    hp = value;

                    if (hp < OriHp)
                    {
                        IsShield = true;
                        CurShieldTime = 0.3f;
                    }
                }

                if (hp <= 0)
                {
                    hp = 0;
                    GameOver();
                }
            }
        }
    }

    //연료
    [SerializeField]
    private float stamina;
    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            if (IsGameOver == false)
            {
                stamina = value;

                if (stamina <= 0)
                {
                    stamina = 0;
                    GameOver();
                }
            }
        }
    }
    #endregion


    [Header("부가 변수")]
    public int KillCount = 0;
    public int Score = 0;
    public bool IsGameOver = false;

    public int MaxSTR = 4;
    public int MaxAGI = 4;
    public float MaxHp = 100;
    public float MaxStamina = 100;

    public float MaxShieldTime;
    public float CurShieldTime;
    public bool IsShield = false;

    public float HitTime = 0.5f;

    [Header("현재 스테이지 정보")]
    public bool IsShow = false;
    public bool IsStart = false;
    public bool IsNextStage;
    public int StageNum;
    public float PlayerTime;
    public Boss CurBoss;
    public bool IsBossStage;

    [Header("랭킹 데이터")]
    public string[] RakingName = new string[5];
    public int[] RakingScore = new int[5];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        for (int i = 0; i < 5; i++)
        {
            GameMgr.Instance.RakingScore[i] = PlayerPrefs.GetInt($"Score{i}");
            GameMgr.Instance.RakingName[i] = PlayerPrefs.GetString($"Name{i}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Wow();

        if (IsStart == true)
        {
            PlayerTime += Time.deltaTime;
            Stamina -= Time.deltaTime * 1.5f;

            if (CurShieldTime > 0 && IsShield == true)
                CurShieldTime -= Time.deltaTime;

            else if (CurShieldTime <= 0 && IsShield == true)
            {
                CurShieldTime = 0;
                IsShield = false;
            }

        }
    }

    public void InputPlayer(GameObject P)
    {
        Player = P;
    }

    public void InputUiManager()
    {
        UIM = GameObject.FindObjectOfType<UIManager>();
    }

    public void InputEnemySpawner()
    {
        EnemySpawner = GameObject.FindObjectOfType<EnemySpawn>();
    }

    public void GameOver()
    {
        IsGameOver = true;
        StartCoroutine(UIM.GameSet(GameEnd.Over));
    }

    private void Wow()
    {
        if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            StrUpLevel = MaxSTR;
            AgiUpLevel = MaxAGI;
        }
        else if (Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.GetComponent<Player>().Skills[0].SkillCount = 20;
            Player.GetComponent<Player>().Skills[0].CurCool = 0;

            Player.GetComponent<Player>().Skills[0].SkillCount = 3;
            Player.GetComponent<Player>().Skills[0].CurCool = 0;

            Player.GetComponent<Player>().Skills[0].SkillCount = 5;
            Player.GetComponent<Player>().Skills[0].CurCool = 0;
        }
        else if (Input.GetKeyDown(KeyCode.F4) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Hp = MaxHp;
        }
        else if (Input.GetKeyDown(KeyCode.F5) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Stamina = MaxStamina;
        }
        else if (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ManagerReset();

            switch (StageNum)
            {
                case 1:
                    SceneManager.LoadScene("Stage2");
                    break;
                case 2:
                    SceneManager.LoadScene("Stage3");
                    break;
                case 3:
                    SceneManager.LoadScene("Stage1");
                    break;

                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F7) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Image img in UIM.Points)
            {
                img.color = Color.green;
            }

            UIM.IsEvent = true;
            PlayerTime = 70.0f;
        }
    }

    public void ManagerReset()
    {
        Time.timeScale = 1.0f;
        EnemySpawn.ClearCurEnemy();

        IsGameOver = false;
        IsShield = false;
        IsShow = false;
        IsStart = false;
        IsNextStage = false;
        IsBossStage = false;

        Player = null;
        UIM = null;
        EnemySpawner = null;
    }

    public void GoToTitle()
    {
        Time.timeScale = 1.0f;
        EnemySpawn.ClearCurEnemy();

        IsGameOver = false;
        IsShield = false;
        CurShieldTime = 0;
        IsShow = false;
        IsStart = false;
        IsNextStage = false;
        IsBossStage = false;

        Player = null;
        UIM = null;
        EnemySpawner = null;

        Hp = MaxHp;
        Stamina = MaxStamina;
        StrUpLevel = 0;
        AgiUpLevel = 0;

        Score = 0;
        KillCount = 0;

        StageNum = 0;
        PlayerTime = 0;
        CurBoss = null;
    }
}
