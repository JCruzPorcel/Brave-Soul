using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponData> weaponsList = new List<WeaponData>();
    public List<WeaponData> currentWeaponList = new List<WeaponData>();

    public string upgrade = string.Empty;

    int currentWeapons = 0;

    public FloatingSprite floatingSprite;

    #region Left Panel
    [Space(10)]
    [Header("LEFT")]

    //Left
    [SerializeField] TMP_Text left_Weapon_Name;

    [SerializeField] TMP_Text left_Weapon_Description;

    [SerializeField] Image left_Weapon_Image;

    [SerializeField] GameObject left_Box_Upgrade;

    [SerializeField] Image left_Weapon_Upgrade_Image;

    [SerializeField] TMP_Text left_Upgrade_Text;

    [SerializeField] WeaponData leftData;

    int random_Left;

    #endregion

    #region Mid Panel
    [Space(10)]
    [Header("MID")]

    //Mid
    [SerializeField] TMP_Text mid_Weapon_Name;

    [SerializeField] TMP_Text mid_Weapon_Description;

    [SerializeField] Image mid_Weapon_Image;

    [SerializeField] GameObject mid_Box_Upgrade;

    [SerializeField] Image mid_Weapon_Upgrade_Image;

    [SerializeField] TMP_Text mid_Upgrade_Text;

    [SerializeField] WeaponData midData;

    [SerializeField] GameObject midCanvas;

    int random_Mid;

    #endregion

    #region Right Panel
    [Space(10)]
    [Header("RIGHT")]

    //Right
    [SerializeField] TMP_Text right_Weapon_Name;

    [SerializeField] TMP_Text right_Weapon_Description;

    [SerializeField] Image right_Weapon_Image;

    [SerializeField] GameObject right_Box_Upgrade;

    [SerializeField] Image right_Weapon_Upgrade_Image;

    [SerializeField] TMP_Text right_Upgrade_Text;

    [SerializeField] WeaponData rightData;

    [SerializeField] GameObject rightCanvas;

    int random_Right;

    #endregion


    private void Start()
    {
        currentWeaponList.Add(GameManager.Instance.CharSelected.StartWeapon);
        currentWeapons = 1;
    }


    void LeftWeapon()
    {
        if (currentWeapons >= 3)
        {
            if (currentWeaponList.Count == 0)
            {
                MenuManager.Instance.InGame();
                LevelUpManager.Instance.maxLevel = true;
                return;
            }
        }
        else
        {
            if (weaponsList.Count == 0)
            {
                MenuManager.Instance.InGame();
                LevelUpManager.Instance.maxLevel = true;
                return;
            }
        }

        if (!MaxWeapons())
        {
            random_Left = Random.Range(0, weaponsList.Count);
            leftData = weaponsList[random_Left];
        }
        else
        {
            random_Left = Random.Range(0, currentWeaponList.Count);
            leftData = currentWeaponList[random_Left];
        }


        while (leftData == rightData || leftData == midData)
        {
            if (!MaxWeapons())
            {
                random_Left = Random.Range(0, weaponsList.Count);
                leftData = weaponsList[random_Left];
            }
            else
            {
                random_Left = Random.Range(0, currentWeaponList.Count);
                leftData = currentWeaponList[random_Left];
            }
        }

        left_Weapon_Name.text = leftData.ItemName.ToString();
        left_Weapon_Description.text = leftData.Description.ToString();
        left_Weapon_Image.sprite = leftData.WeaponImage;

        if (leftData.CanUpgrade)
        {
            left_Weapon_Upgrade_Image.sprite = leftData.UpgradeImage;
            left_Upgrade_Text.text = upgrade.ToString();
            left_Box_Upgrade.SetActive(true);
        }
        else
        {
            left_Box_Upgrade.SetActive(false);
        }
    }

    void MidWeapon()
    {
        if (currentWeapons >= 3)
        {
            if (currentWeaponList.Count < 3)
            {
                midCanvas.SetActive(false);
                return;
            }
        }
        else
        {
            if (weaponsList.Count < 3)
            {
                midCanvas.SetActive(false);
                return;
            }
        }

        if (!MaxWeapons())
        {
            random_Mid = Random.Range(0, weaponsList.Count);
            midData = weaponsList[random_Mid];
        }
        else
        {
            random_Mid = Random.Range(0, currentWeaponList.Count);
            midData = currentWeaponList[random_Mid];
        }

        while (midData == leftData || midData == rightData)
        {
            if (!MaxWeapons())
            {
                random_Mid = Random.Range(0, weaponsList.Count);
                midData = weaponsList[random_Mid];
            }
            else
            {
                random_Mid = Random.Range(0, currentWeaponList.Count);
                midData = currentWeaponList[random_Mid];
            }
        }

        mid_Weapon_Name.text = midData.ItemName.ToString();
        mid_Weapon_Description.text = midData.Description.ToString();
        mid_Weapon_Image.sprite = midData.WeaponImage;

        if (midData.CanUpgrade)
        {
            mid_Weapon_Upgrade_Image.sprite = midData.UpgradeImage;
            mid_Upgrade_Text.text = upgrade.ToString();
            mid_Box_Upgrade.SetActive(true);
        }
        else
        {
            mid_Box_Upgrade.SetActive(false);
        }
    }

    void RightWeapon()
    {
        if (currentWeapons >= 3)
        {
            if (currentWeaponList.Count < 2)
            {
                rightCanvas.SetActive(false);
                return;
            }
        }
        else
        {
            if (weaponsList.Count < 2)
            {
                rightCanvas.SetActive(false);
                return;
            }
        }

        if (!MaxWeapons())
        {
            random_Right = Random.Range(0, weaponsList.Count);
            rightData = weaponsList[random_Right];
        }
        else
        {
            random_Right = Random.Range(0, currentWeaponList.Count);
            rightData = currentWeaponList[random_Right];
        }


        while (rightData == midData || rightData == leftData)
        {
            if (!MaxWeapons())
            {
                random_Right = Random.Range(0, weaponsList.Count);
                rightData = weaponsList[random_Right];
            }
            else
            {
                random_Right = Random.Range(0, currentWeaponList.Count);
                rightData = currentWeaponList[random_Right];
            }
        }

        right_Weapon_Name.text = rightData.ItemName.ToString();
        right_Weapon_Description.text = rightData.Description.ToString();
        right_Weapon_Image.sprite = rightData.WeaponImage;

        if (rightData.CanUpgrade)
        {
            right_Weapon_Upgrade_Image.sprite = rightData.UpgradeImage;
            right_Upgrade_Text.text = upgrade.ToString();
            right_Box_Upgrade.SetActive(true);
        }
        else
        {
            right_Box_Upgrade.SetActive(false);
        }
    }



    private bool MaxWeapons()
    {
        bool maxWeapon;

        if (currentWeapons > 3)
        {
            maxWeapon = true;
        }
        else
        {
            maxWeapon = false;
        }

        return maxWeapon;
    }

    private void OnEnable()
    {
        MidWeapon();
        RightWeapon();
        LeftWeapon();
    }

    private void OnDisable()
    {
        leftData = null;
        midData = null;
        rightData = null;
    }


    public void LeftWeaponSpawn()
    {
        bool itOnTheList = false;

        foreach (WeaponData weapon in currentWeaponList)
        {
            if (leftData.VarName == weapon.VarName)
            {
                itOnTheList = true;
            }
        }

        if (!itOnTheList)
        {
            currentWeaponList.Add(leftData);

            if (leftData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                GetStatsManager.Instance.level_Axe = 1;

                currentWeapons++;
            }
            else if (leftData.VarName == "Crossbow")
            {
                Instantiate(leftData.Prefab);
                GetStatsManager.Instance.level_Crossbow = 1;

                currentWeapons++;
            }
            else if (leftData.VarName == "Necronomicon")
            {
                Instantiate(leftData.Prefab);
                GetStatsManager.Instance.level_Necronomicon = 1;

                currentWeapons++;
            }
            else if (leftData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow = 1;
            }
        }
        else
        {
            if (leftData.VarName == "Axe")
            {
                GetStatsManager.Instance.level_Axe++;

                if (GetStatsManager.Instance.level_Axe >= 5)
                {
                    currentWeaponList.Remove(leftData);
                    weaponsList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Crossbow")
            {
                GetStatsManager.Instance.level_Crossbow++;

                if (GetStatsManager.Instance.level_Crossbow >= 5)
                {
                    currentWeaponList.Remove(leftData);
                    weaponsList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Necronomicon")
            {
                GetStatsManager.Instance.level_Necronomicon++;

                if (GetStatsManager.Instance.level_Necronomicon >= 5)
                {
                    currentWeaponList.Remove(leftData);
                    weaponsList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow++;

                if (GetStatsManager.Instance.level_Arrow >= 5)
                {
                    currentWeaponList.Remove(leftData);
                    weaponsList.Remove(leftData);
                }
            }
        }
        floatingSprite.SpawnSpriteForge();
        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }

    public void MidWeaponSpawn()
    {
        bool itOnTheList = false;

        foreach (WeaponData weapon in currentWeaponList)
        {
            if (midData.VarName == weapon.VarName)
            {
                itOnTheList = true;
            }
        }

        if (!itOnTheList)
        {
            currentWeaponList.Add(midData);

            if (midData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                GetStatsManager.Instance.level_Axe = 1;

                currentWeapons++;
            }
            else if (midData.VarName == "Crossbow")
            {
                Instantiate(midData.Prefab);
                GetStatsManager.Instance.level_Crossbow = 1;

                currentWeapons++;
            }
            else if (midData.VarName == "Necronomicon")
            {
                Instantiate(midData.Prefab);
                GetStatsManager.Instance.level_Necronomicon = 1;

                currentWeapons++;
            }
            else if (midData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow = 1;
            }
        }
        else
        {
            if (midData.VarName == "Axe")
            {
                GetStatsManager.Instance.level_Axe++;

                if (GetStatsManager.Instance.level_Axe >= 5)
                {
                    currentWeaponList.Remove(midData);
                    weaponsList.Remove(midData);
                }
            }
            else if (midData.VarName == "Crossbow")
            {
                GetStatsManager.Instance.level_Crossbow++;

                if (GetStatsManager.Instance.level_Crossbow >= 5)
                {
                    currentWeaponList.Remove(midData);
                    weaponsList.Remove(midData);
                }
            }
            else if (midData.VarName == "Necronomicon")
            {
                GetStatsManager.Instance.level_Necronomicon++;

                if (GetStatsManager.Instance.level_Necronomicon >= 5)
                {
                    currentWeaponList.Remove(midData);
                    weaponsList.Remove(midData);
                }
            }
            else if (midData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow++;

                if (GetStatsManager.Instance.level_Arrow >= 5)
                {
                    currentWeaponList.Remove(midData);
                    weaponsList.Remove(midData);
                }
            }
        }

        floatingSprite.SpawnSpriteForge();
        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }

    public void RightWeaponSpawn()
    {
        bool itOnTheList = false;

        foreach (WeaponData weapon in currentWeaponList)
        {
            if (rightData.VarName == weapon.VarName)
            {
                itOnTheList = true;
            }
        }

        if (!itOnTheList)
        {
            currentWeaponList.Add(rightData);

            if (rightData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                GetStatsManager.Instance.level_Axe = 1;

                currentWeapons++;
            }
            else if (rightData.VarName == "Crossbow")
            {
                Instantiate(rightData.Prefab);
                GetStatsManager.Instance.level_Crossbow = 1;

                currentWeapons++;
            }
            else if (rightData.VarName == "Necronomicon")
            {
                Instantiate(rightData.Prefab);
                GetStatsManager.Instance.level_Necronomicon = 1;

                currentWeapons++;
            }
            else if (rightData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow = 1;
            }
        }
        else
        {
            if (rightData.VarName == "Axe")
            {
                GetStatsManager.Instance.level_Axe++;

                if (GetStatsManager.Instance.level_Axe >= 5)
                {
                    currentWeaponList.Remove(rightData);
                    weaponsList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Crossbow")
            {
                GetStatsManager.Instance.level_Crossbow++;

                if (GetStatsManager.Instance.level_Crossbow >= 5)
                {
                    currentWeaponList.Remove(rightData);
                    weaponsList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Necronomicon")
            {
                GetStatsManager.Instance.level_Necronomicon++;

                if (GetStatsManager.Instance.level_Necronomicon >= 5)
                {
                    currentWeaponList.Remove(rightData);
                    weaponsList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Arrow")
            {
                GetStatsManager.Instance.level_Arrow++;

                if (GetStatsManager.Instance.level_Arrow >= 5)
                {
                    currentWeaponList.Remove(rightData);
                    weaponsList.Remove(rightData);
                }
            }
        }

        floatingSprite.SpawnSpriteForge();
        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }
}