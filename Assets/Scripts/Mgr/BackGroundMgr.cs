using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BackImg
{
    public Image BackImage;
    public Image NextBackImage;
}

public class BackGroundMgr : MonoBehaviour
{
    [SerializeField]
    private float MaxDownPosY;
    [SerializeField]
    private static float MoveDownSpeed;

    [SerializeField]
    List<BackImg> BackImgs;

    private void Start()
    {
        BackScrolingSpeed(500.0f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BackImgs.Count; i++)
        {
            BackImgs[i].BackImage.rectTransform.anchoredPosition += Vector2.down * MoveDownSpeed * Time.deltaTime;

            if(BackImgs[i].BackImage.rectTransform.anchoredPosition.y <= MaxDownPosY)
            {
                BackImgs[i].BackImage.rectTransform.anchoredPosition = BackImgs[i].NextBackImage.rectTransform.anchoredPosition + (Vector2.up * 1075);
            }
        }
    }

    //배경 스피드 조절
    public static void BackScrolingSpeed(float Speed)
    {
        MoveDownSpeed = Speed;
    }
}
