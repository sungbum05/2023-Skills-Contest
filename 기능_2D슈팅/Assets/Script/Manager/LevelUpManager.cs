using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelUpPanel = null;

    [SerializeField]
    private Weapon RandomWeapon = null;
    [SerializeField]
    private Accessory RandomAccessory = null;

    private void Start()
    {
        GameManager.Instance.ConnectLevelUpManager(this);
    }

    public void SetActiveLevelUpPanel(bool Check)
    {
        LevelUpPanel.SetActive(Check);
    }

    public void SettingLevelUpPanel(Weapon weapon, Accessory accessory)
    {
        RandomWeapon = weapon;
        RandomAccessory = accessory;
    }
}
