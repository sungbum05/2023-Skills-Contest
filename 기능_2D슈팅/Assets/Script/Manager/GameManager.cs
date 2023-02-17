using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("인 게임 컴포넌트")]
    [SerializeField]
    private Player Player;
    [SerializeField]
    private WeaponManager WeaponManager;
    [SerializeField]
    private LevelUpManager LevelUpManager;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        else if(Instance != this)
            Destroy(Instance);

        DontDestroyOnLoad(Instance);
    }

    //레벨업 매니져한테 랜덤으로 나온 무기와 액세서리 정보 전달
    public void PlayerLevelUp()
    {
        int RandomWeaponCount = Random.Range(0, WeaponManager.Weapons.Count);
        int RandomAccessoryCount = Random.Range(0, WeaponManager.Accessories.Count);

        LevelUpManager.SettingLevelUpPanel(WeaponManager.Weapons[RandomWeaponCount], WeaponManager.Accessories[RandomAccessoryCount]);
    }

    #region 컴포넌트 할당 함수(자기 객체에서 실행)
    public void ConnectPlayer(Object Component)
    {
        Player = Component as Player;
    }
    public void ConnectWeaponManager(Object Component)
    {
        WeaponManager = Component as WeaponManager;
    }
    public void  ConnectLevelUpManager(Object Component) 
    {
        LevelUpManager = Component as LevelUpManager;
    }
    #endregion
}
