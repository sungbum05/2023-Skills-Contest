using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameEnd
{
    Clear, Next, Over
}

public class UIManager : MonoBehaviour
{
    public Image HpImage;
    public Image StaminaImage;

    public List<Image> StrLevelImg;
    public List<Image> AgiLevelImg;

    public Text Score;
    #region KKK
    [Header("페이드")]
    [SerializeField]
    private float FadeSpeed;
    [SerializeField]
    private Image BlackPanel;

    public Coroutine PanFade;
    public Coroutine HpFade;

    [Header("보스 체력")]
    public Boss StageBoss;
    public GameObject BossHpBar;
    public Image FrontBossHp;

    [Header("스테이지 맵")]
    public Text StageTxt;
    public List<Image> Points = new List<Image>();
    public bool IsEvent = false;

    [Header("스킬 텍스트")]
    public List<Text> CoolText = new List<Text>();
    public List<Text> CountText = new List<Text>();

    [Header("게임 종료 판")]
    public List<GameObject> GameOverPan;

    [Header("게임 클리어 텍스트")]
    public Text ClearTime;
    public Text ClearKillCount;
    public Text ClearHp;
    public Text ClearScore;
    [Header("게임 넥스트 텍스트")]
    public Text GameNextTime;
    public Text GameNextKillCount;
    public Text GameNextHp;
    public Text GameNextScore;
    [Header("게임 오버 텍스트")]
    public Text GameOverBecouse;
    public Text GameOverTime;
    public Text GameOverKillCount;
    public Text GameOverHp;
    public Text GameOverScore;
    [Header("랭킹 텍스트")]
    public string Name;
    public Text RankingScore;
    public InputField InputName;
    public List<Text> Rangkings = new List<Text>();
    #endregion

    public MapMakinManager MapMakinManager;
    public GameObject DustEffect;

    private void Start()
    {
        Time.timeScale = 1.0f;

        GameMgr.Instance.InputUiManager();
        GameStart();
    }

    void Update()
    {
        HpBarFix();
        StaminaBarFix();
        StrLevelFix();
        AgiLevelFix();
        ScoreFix();

        if (GameMgr.Instance.Player != null && GameMgr.Instance.UIM != null && GameMgr.Instance.EnemySpawner != null)
        {
            if (StageBoss.gameObject.activeSelf == true && StageBoss)
            {
                FrontBossHp.fillAmount = StageBoss.Hp / StageBoss.MaxHp;
            }

            else
            {
                FrontBossHp.fillAmount = 0;

            }

            if (GameMgr.Instance.IsStart == true && GameMgr.Instance.IsBossStage == false && GameMgr.Instance.IsBossStage == false)
            {
                Points[(int)GameMgr.Instance.PlayerTime / 10].color = Color.green;

                if ((int)GameMgr.Instance.PlayerTime / 10 == 3 && IsEvent == false)
                {
                    Debug.Log("Event 2");
                    IsEvent = true;

                    DustEffect.SetActive(true);
                }

                else if ((int)GameMgr.Instance.PlayerTime / 10 == 7 && IsEvent == true)
                {
                    DustEffect.SetActive(false);

                    GameMgr.Instance.IsBossStage = true;
                    Debug.Log("Boss");
                    IsEvent = false;

                    StageBoss.gameObject.SetActive(true);
                    StageBoss.BossSpawn();
                }
            }

            if (GameMgr.Instance.IsShow == false && GameMgr.Instance.IsStart == false && GameMgr.Instance.IsNextStage == false)
            {
                StartCoroutine(GameStart());
            }

            #region 플레이어 스킬 ui
            CountText[0].text = $"X {GameMgr.Instance.Player.GetComponent<Player>().Skills[0].SkillCount}";

            if (GameMgr.Instance.Player.GetComponent<Player>().Skills[1].CurCool > 0)
            {
                CoolText[1].gameObject.SetActive(true);
                CoolText[1].text = $"{(int)GameMgr.Instance.Player.GetComponent<Player>().Skills[1].CurCool + 1}";
            }

            else
            {
                CoolText[1].gameObject.SetActive(false);
            }

            CountText[1].text = $"X {GameMgr.Instance.Player.GetComponent<Player>().Skills[1].SkillCount}";

            if (GameMgr.Instance.Player.GetComponent<Player>().Skills[2].CurCool > 0)
            {
                CoolText[2].gameObject.SetActive(true);
                CoolText[2].text = $"{(int)GameMgr.Instance.Player.GetComponent<Player>().Skills[2].CurCool + 1}";
            }

            else
            {
                CoolText[2].gameObject.SetActive(false);
            }

            CountText[2].text = $"X {GameMgr.Instance.Player.GetComponent<Player>().Skills[2].SkillCount}";
            #endregion
        }
    }
    void HpBarFix()
    {
        HpImage.fillAmount = GameMgr.Instance.Hp / GameMgr.Instance.MaxHp;
    }
    void StaminaBarFix()
    {
        StaminaImage.fillAmount = GameMgr.Instance.Stamina / GameMgr.Instance.MaxStamina;
    }
    public void StrLevelFix()
    {
        for (int i = 0; i <= GameMgr.Instance.StrUpLevel; i++)
        {
            StrLevelImg[i].color = Color.yellow;
        }
    }
    public void AgiLevelFix()
    {
        for (int i = 0; i <= GameMgr.Instance.AgiUpLevel; i++)
        {
            AgiLevelImg[i].color = Color.yellow;
        }
    }
    public void ScoreFix()
    {
        Score.text = $"Score: {GameMgr.Instance.Score}";
    }
    public void StartPanfadeIn(float _Time)
    {
        if (PanFade == null)
            PanFade = StartCoroutine(FadeIn(_Time));
    }
    public void StartPanFadeOut(float _Time)
    {
        if (PanFade == null)
            PanFade = StartCoroutine(FadeOut(_Time));
    }
    IEnumerator FadeIn(float _Time)
    {
        yield return new WaitForSeconds(_Time);

        Color Fade = new Color(0, 0, 0, 0);
        BlackPanel.color = Fade;

        while (BlackPanel.color.a < 1)
        {
            yield return null;
            BlackPanel.color = Fade;

            Fade.a += Time.deltaTime * FadeSpeed;
        }

        Debug.Log("nu");
        PanFade = null;
        yield break;
    }
    IEnumerator FadeOut(float _Time)
    {
        yield return new WaitForSeconds(_Time);

        Color Fade = new Color(0, 0, 0, 1);
        BlackPanel.color = Fade;

        while (BlackPanel.color.a > 0)
        {
            yield return null;
            BlackPanel.color = Fade;

            Fade.a -= Time.deltaTime * FadeSpeed;
        }

        PanFade = null;
        yield break;
    }

    public IEnumerator GameStart()
    {
        GameMgr.Instance.IsShow = true;

        StartPanFadeOut(0.0f);
        yield return new WaitForSeconds(0.3f);
        while (GameMgr.Instance.Player.transform.position != new Vector3(0, 0, -17))
        {
            yield return null;
            GameMgr.Instance.Player.transform.position = Vector3.MoveTowards(GameMgr.Instance.Player.transform.position, new Vector3(0, 0, -17), 15 * Time.deltaTime);
        }

        GameMgr.Instance.StageNum++;
        GameMgr.Instance.IsStart = true;

        GameMgr.Instance.PlayerTime = 0;
        GameMgr.Instance.CurBoss = StageBoss;

        StageTxt.text = $"Stage {GameMgr.Instance.StageNum}";
        GameMgr.Instance.EnemySpawner.StartSpawnEnemy();

        yield break;
    }

    public IEnumerator GameSet(GameEnd EndType)
    {
        GameMgr.Instance.IsStart = false;

        GameMgr.Instance.CurBoss = null;

        switch (EndType)
        {
            case GameEnd.Clear:
                Time.timeScale = 0.3f;
                GameMgr.Instance.Score += ((int)GameMgr.Instance.Hp * 100) + (GameMgr.Instance.KillCount * 100) + (500 - (int)GameMgr.Instance.PlayerTime);

                GameOverPan[0].SetActive(true);

                ClearTime.text = $"Time: {(int)GameMgr.Instance.PlayerTime / 60}:{(int)GameMgr.Instance.PlayerTime % 60}";
                ClearKillCount.text = $"Kill Count: {GameMgr.Instance.KillCount}";
                ClearHp.text = $"Hp: {GameMgr.Instance.Hp} / {GameMgr.Instance.MaxHp}";
                ClearScore.text = $"Score: {GameMgr.Instance.Score}";
                break;

            case GameEnd.Next:
                Debug.Log("Next");
                Time.timeScale = 0.3f;
                GameMgr.Instance.Score += ((int)GameMgr.Instance.Hp * 100) + (GameMgr.Instance.KillCount * 100) + (500 - (int)GameMgr.Instance.PlayerTime);

                GameOverPan[1].SetActive(true);

                GameNextTime.text = $"Time: {(int)GameMgr.Instance.PlayerTime / 60}:{(int)GameMgr.Instance.PlayerTime % 60}";
                GameNextKillCount.text = $"Kill Count: {GameMgr.Instance.KillCount}";
                GameNextHp.text = $"Hp: {GameMgr.Instance.Hp} / {GameMgr.Instance.MaxHp}";
                GameNextScore.text = $"Score: {GameMgr.Instance.Score}";

                break;

            case GameEnd.Over:
                Time.timeScale = 0.3f;
                GameMgr.Instance.Score += ((int)GameMgr.Instance.Hp * 100) + (GameMgr.Instance.KillCount * 100) + (500 - (int)GameMgr.Instance.PlayerTime);

                GameOverPan[2].SetActive(true);

                GameOverBecouse.text = $"You DIe Because: {(GameMgr.Instance.Stamina < 0 ? ("Stamina Zero") : ("HpZero"))}";
                GameOverTime.text = $"Time: {(int)GameMgr.Instance.PlayerTime / 60}:{(int)GameMgr.Instance.PlayerTime % 60}";
                GameOverKillCount.text = $"Kill Count: {GameMgr.Instance.KillCount}";
                GameOverHp.text = $"Hp: {GameMgr.Instance.Hp} / {GameMgr.Instance.MaxHp}";

                GameOverScore.text = $"Score: {GameMgr.Instance.Score}";
                break;
            default:
                break;
        }

        yield break;
    }

    public void OnRankingBoard()
    {
        foreach (var item in GameOverPan)
        {
            item.SetActive(false);
        }


        GameOverPan[3].SetActive(true);

        for (int i = 0; i < Rangkings.Count; i++)
        {
            Rangkings[i].text = $"{i + 1}. {GameMgr.Instance.RakingName[i]} : {GameMgr.Instance.RakingScore[i]}";
        }

        RankingScore.text = $"Score: {GameMgr.Instance.Score}";


    }

    public void NameCheck()
    {
        if (GameMgr.Instance.RakingScore[4] < GameMgr.Instance.Score)
        {
            GameMgr.Instance.RakingName[4] = InputName.text;
            GameMgr.Instance.RakingScore[4] = GameMgr.Instance.Score;

            int Count = 3;
            int CurCount = 4;

            while(Count != -1)
            {
                if (GameMgr.Instance.RakingScore[CurCount] >= GameMgr.Instance.RakingScore[Count])
                {
                    int Score = GameMgr.Instance.RakingScore[CurCount];
                    string Name = GameMgr.Instance.RakingName[CurCount];

                    GameMgr.Instance.RakingScore[CurCount] = GameMgr.Instance.RakingScore[Count];
                    GameMgr.Instance.RakingName[CurCount] = GameMgr.Instance.RakingName[Count];

                    GameMgr.Instance.RakingScore[Count] = Score;
                    GameMgr.Instance.RakingName[Count] = Name;
                }

                CurCount--;
                Count--;
            }

            GameMgr.Instance.GoToTitle();
            SceneManager.LoadScene("Title");
        }
    }

    public void NextStage()
    {
        if (GameMgr.Instance.StageNum == 1)
        {
            GameMgr.Instance.ManagerReset();
            SceneManager.LoadScene("Stage2");
        }

        else if (GameMgr.Instance.StageNum == 2)
        {
            GameMgr.Instance.ManagerReset();
            SceneManager.LoadScene("Stage3");
        }
    }
}
