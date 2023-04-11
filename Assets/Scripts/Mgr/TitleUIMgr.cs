using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIMgr : MonoBehaviour
{
    public GameObject RankingPan;
    public GameObject GuidePan;

    public List<Text> Rangkings = new List<Text>();

    public void OnStart()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void OnRank()
    {
        RankingPan.SetActive(true);

        for (int i = 0; i < Rangkings.Count; i++)
        {
            Rangkings[i].text = $"{i + 1}. {GameMgr.Instance.RakingName[i]} : {GameMgr.Instance.RakingScore[i]}";
        }
    }
    public void OffRank()
    {
        RankingPan.SetActive(false);
    }
    public void OnGuide()
    {
        GuidePan.SetActive(true);
    }
    public void OffGuide()
    {
        GuidePan.SetActive(false);
    }

    public void Save()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"Score{i}", GameMgr.Instance.RakingScore[i]);
            PlayerPrefs.SetString($"Name{i}", GameMgr.Instance.RakingName[i]);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Save();
#else
        Application.Quit();
        Save();
#endif
    }
}
