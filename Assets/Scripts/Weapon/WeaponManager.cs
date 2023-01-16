using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponData> weaponsList = new List<WeaponData>();
    public List<WeaponData> currentWeaponList = new List<WeaponData>();

    public string upgrade = string.Empty;

    public int currentWeapons = 0;

    public FloatingSprite floatingSprite;

    string newItem;
    string upgradeItem;

    int newItemNum = 38;
    int upgradeNum = 39;


    public TMP_Text left_text_anim;
    public TMP_Text mid_text_anim;
    public TMP_Text right_text_anim;

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

    string[] texts;

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



    private void Awake()
    {
        currentWeaponList.Add(GameManager.Instance.CharSelected.StartWeapon);
        currentWeapons = 1;
    }

    void LeftWeapon()
    {
        if (currentWeapons > 3)
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

        GetWeaponDescription(leftData);

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

        if (currentWeaponList.Contains(leftData))
        {
            left_text_anim.text = upgradeItem;
        }
        else
        {
            left_text_anim.text = newItem;
        }
    }

    void MidWeapon()
    {
        if (currentWeapons > 3)
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

        GetWeaponDescription(midData);

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

        if (currentWeaponList.Contains(midData))
        {
            mid_text_anim.text = upgradeItem;
        }
        else
        {
            mid_text_anim.text = newItem;
        }
    }

    void RightWeapon()
    {
        if (currentWeapons > 3)
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

        GetWeaponDescription(rightData);

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

        if (currentWeaponList.Contains(rightData))
        {
            right_text_anim.text = upgradeItem;
        }
        else
        {
            right_text_anim.text = newItem;
        }
    }


    public void GetWeaponDescription(WeaponData weaponData)
    {
        if (weaponData.VarName == "Axe")
        {
            weaponData.Prefab.GetComponent<Axe>().WeaponLevel();
        }
        if (weaponData.VarName == "Arrow")
        {
            weaponData.Prefab.GetComponent<Arrow>().WeaponLevel();
        }
        if (weaponData.VarName == "Crossbow")
        {
            weaponData.Prefab.GetComponent<Crossbow>().WeaponLevel();
        }
        if (weaponData.VarName == "Necronomicon")
        {
            weaponData.Prefab.GetComponent<Necronomicon>().WeaponLevel();
        }
    }


    public void LeftWeaponSpawn()
    {
        if (currentWeaponList.Contains(leftData))
        {
            if (leftData.VarName == "Axe")
            {
                LevelUpManager.Instance.level_Axe++;

                if (LevelUpManager.Instance.level_Axe >= 5)
                {
                    weaponsList.Remove(leftData);
                    currentWeaponList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Crossbow")
            {
                LevelUpManager.Instance.level_Crossbow++;

                if (LevelUpManager.Instance.level_Crossbow >= 5)
                {
                    weaponsList.Remove(leftData);
                    currentWeaponList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Necronomicon")
            {
                LevelUpManager.Instance.level_Necronomicon++;

                if (LevelUpManager.Instance.level_Necronomicon >= 5)
                {
                    weaponsList.Remove(leftData);
                    currentWeaponList.Remove(leftData);
                }
            }
            else if (leftData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;

                if (LevelUpManager.Instance.level_Arrow >= 5)
                {
                    weaponsList.Remove(leftData);
                    currentWeaponList.Remove(leftData);
                }
            }
        }
        else
        {
            currentWeaponList.Add(leftData);

            if (leftData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                LevelUpManager.Instance.level_Axe++;
                currentWeapons++;
            }
            else if (leftData.VarName == "Crossbow")
            {
                Instantiate(leftData.Prefab);

                LevelUpManager.Instance.level_Crossbow++;
                currentWeapons++;
            }
            else if (leftData.VarName == "Necronomicon")
            {
                Instantiate(leftData.Prefab);

                LevelUpManager.Instance.level_Necronomicon++;
                currentWeapons++;
            }
            else if (leftData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;
            }
        }

        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }

    public void MidWeaponSpawn()
    {
        if (currentWeaponList.Contains(midData))
        {
            floatingSprite.SpawnSpriteForge();
            FindObjectOfType<AudioManager>().Play("ForgeWeapon SFX");

            if (midData.VarName == "Axe")
            {
                LevelUpManager.Instance.level_Axe++;

                if (LevelUpManager.Instance.level_Axe >= 5)
                {
                    weaponsList.Remove(midData);
                    currentWeaponList.Remove(midData);
                }
            }
            else if (midData.VarName == "Crossbow")
            {
                LevelUpManager.Instance.level_Crossbow++;

                if (LevelUpManager.Instance.level_Crossbow >= 5)
                {
                    weaponsList.Remove(midData);
                    currentWeaponList.Remove(midData);
                }
            }
            else if (midData.VarName == "Necronomicon")
            {
                LevelUpManager.Instance.level_Necronomicon++;

                if (LevelUpManager.Instance.level_Necronomicon >= 5)
                {
                    weaponsList.Remove(midData);
                    currentWeaponList.Remove(midData);
                }
            }
            else if (midData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;

                if (LevelUpManager.Instance.level_Arrow >= 5)
                {
                    weaponsList.Remove(midData);
                    currentWeaponList.Remove(midData);
                }
            }
        }
        else
        {
            currentWeaponList.Add(midData);

            if (midData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                LevelUpManager.Instance.level_Axe++;

                currentWeapons++;
            }
            else if (midData.VarName == "Crossbow")
            {
                Instantiate(midData.Prefab); Instantiate(leftData.Prefab);
                LevelUpManager.Instance.level_Crossbow++;

                currentWeapons++;
            }
            else if (midData.VarName == "Necronomicon")
            {
                Instantiate(midData.Prefab);
                LevelUpManager.Instance.level_Necronomicon++;

                currentWeapons++;
            }
            else if (midData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;
            }
        }

        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }

    public void RightWeaponSpawn()
    {
        if (currentWeaponList.Contains(rightData))
        {
            floatingSprite.SpawnSpriteForge();
            FindObjectOfType<AudioManager>().Play("ForgeWeapon SFX");

            if (rightData.VarName == "Axe")
            {
                LevelUpManager.Instance.level_Axe++;

                if (LevelUpManager.Instance.level_Axe >= 5)
                {
                    weaponsList.Remove(rightData);
                    currentWeaponList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Crossbow")
            {
                LevelUpManager.Instance.level_Crossbow++;

                if (LevelUpManager.Instance.level_Crossbow >= 5)
                {
                    weaponsList.Remove(rightData);
                    currentWeaponList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Necronomicon")
            {
                LevelUpManager.Instance.level_Necronomicon++;

                if (LevelUpManager.Instance.level_Necronomicon >= 5)
                {
                    weaponsList.Remove(rightData);
                    currentWeaponList.Remove(rightData);
                }
            }
            else if (rightData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;

                if (LevelUpManager.Instance.level_Arrow >= 5)
                {
                    weaponsList.Remove(rightData);
                    currentWeaponList.Remove(rightData);
                }
            }
        }
        else
        {
            currentWeaponList.Add(rightData);

            if (rightData.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;

                LevelUpManager.Instance.level_Axe++;
                currentWeapons++;
            }
            else if (rightData.VarName == "Crossbow")
            {
                Instantiate(rightData.Prefab);

                LevelUpManager.Instance.level_Crossbow++;
                currentWeapons++;
            }
            else if (rightData.VarName == "Necronomicon")
            {
                Instantiate(rightData.Prefab);
                LevelUpManager.Instance.level_Necronomicon++;

                currentWeapons++;
            }
            else if (rightData.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;
            }
        }

        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
    }


    private void OnEnable()
    {
        texts = LanguageManager.Instance.texts;

        newItem = texts[newItemNum];
        upgradeItem = texts[upgradeNum];

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

    private bool MaxWeapons()
    {
        return currentWeapons > 3;
    }
}