using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("상속-정보")]
    [SerializeField]
    protected KeyCode UseSkillBtn;

    [SerializeField]
    public Coroutine UseSkill;

    [Header("스킬 사용 제한")]
    [SerializeField]
    public int SkillCount;
    [SerializeField]
    public float MaxCool;
    [SerializeField]
    public float CurCool;

    virtual protected void Update()
    {
        if(CurCool > 0)
        {
            CurCool -= Time.deltaTime;
        }

        if (Input.GetKeyDown(UseSkillBtn) && CurCool <= 0 && SkillCount > 0)
        {
            UseSkillEffct();
        }

        else if(Input.GetKeyDown(UseSkillBtn) && (CurCool > 0 || SkillCount <= 0))
        {
            GameMgr.Instance.SoundList[4].Play();
        }
    }
    virtual protected void UseSkillEffct()
    {
        CurCool = MaxCool;
        SkillCount--;

        UseSkill = StartCoroutine(SkillEffect());
    }

    virtual protected IEnumerator SkillEffect()
    {
        yield return null;
    }
}
