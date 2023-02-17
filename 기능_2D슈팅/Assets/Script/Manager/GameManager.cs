using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�� ���� ������Ʈ")]
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

    //������ �Ŵ������� �������� ���� ����� �׼����� ���� ����
    public void PlayerLevelUp()
    {
        int RandomWeaponCount = Random.Range(0, WeaponManager.Weapons.Count);
        int RandomAccessoryCount = Random.Range(0, WeaponManager.Accessories.Count);

        LevelUpManager.SettingLevelUpPanel(WeaponManager.Weapons[RandomWeaponCount], WeaponManager.Accessories[RandomAccessoryCount]);
    }

    #region ������Ʈ �Ҵ� �Լ�(�ڱ� ��ü���� ����)
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
