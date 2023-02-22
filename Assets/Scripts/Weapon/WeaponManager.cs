using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponManager : Singleton<WeaponManager>
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

    string[] texts;

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

    #endregion



    private void Awake()
    {
        currentWeaponList.Add(GameManager.Instance.CharSelected.StartWeapon);
        currentWeapons = 1;
    }

    private void Start()
    {
        LeftWeapon();
        MidWeapon();
        RightWeapon();
    }

    public void SelectWeaponLeft()
    {
        if (leftData == null) return;

        LeftWeaponSpawn();
    }

    public void SelectWeaponMid()
    {
        if (midData == null) return;

        MidWeaponSpawn();
    }

    public void SelectWeaponRight()
    {
        if (rightData == null) return;

        RightWeaponSpawn();
    }


    void LeftWeapon()
    {
        if (MaxWeapons())
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

        leftData = GetNewData(leftData, midData, rightData);
        SetNewData(leftData, left_Weapon_Name, left_Weapon_Description, left_Weapon_Image, left_Box_Upgrade, left_Weapon_Upgrade_Image, left_Upgrade_Text, left_text_anim);
    }

    void MidWeapon()
    {
        if (MaxWeapons())
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

        midData = GetNewData(midData, leftData, rightData);
        SetNewData(midData, mid_Weapon_Name, mid_Weapon_Description, mid_Weapon_Image, mid_Box_Upgrade, mid_Weapon_Upgrade_Image, mid_Upgrade_Text, mid_text_anim);
    }

    void RightWeapon()
    {
        if (MaxWeapons())
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

        rightData = GetNewData(rightData, midData, leftData);
        SetNewData(rightData, right_Weapon_Name, right_Weapon_Description, right_Weapon_Image, right_Box_Upgrade, right_Weapon_Upgrade_Image, right_Upgrade_Text, right_text_anim);
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
        PlayerGetWeapon(leftData);
    }

    public void MidWeaponSpawn()
    {
        PlayerGetWeapon(midData);
    }

    public void RightWeaponSpawn()
    {
        PlayerGetWeapon(rightData);
    }


    WeaponData GetNewData(WeaponData data, WeaponData compareData1, WeaponData compareData2)
    {/*
        if (!MaxWeapons())
        {
            randomIntName = Random.Range(0, weaponsList.Count);
            data = weaponsList[randomIntName];
        }
        else
        {
            randomIntName = Random.Range(0, currentWeaponList.Count);
            data = currentWeaponList[randomIntName];
        }*/

        int randomIntName = 0;

        while (data == compareData1 || data == compareData2 || data == null)
        {
            if (!MaxWeapons())
            {
                randomIntName = Random.Range(0, weaponsList.Count);
                data = weaponsList[randomIntName];
            }
            else
            {
                randomIntName = Random.Range(0, currentWeaponList.Count);
                data = currentWeaponList[randomIntName];
            }
        }

        return data;
    }

    void SetNewData(WeaponData data, TMP_Text name, TMP_Text description, Image sprite, GameObject upgradeBox, Image upgradeImage, TMP_Text upgradeText, TMP_Text textAnim)
    {
        GetWeaponDescription(data);

        name.text = data.ItemName.ToString();
        description.text = data.Description.ToString();
        sprite.sprite = data.WeaponImage;


        if (data.CanUpgrade)
        {
            upgradeImage.sprite = data.UpgradeImage;
            upgradeText.text = upgrade.ToString();
            upgradeBox.SetActive(true);
        }
        else
        {
            upgradeBox.SetActive(false);
        }

        if (currentWeaponList.Contains(data))
        {
            textAnim.text = upgradeItem;
        }
        else
        {
            textAnim.text = newItem;
        }
    }

    public void PlayerGetWeapon(WeaponData data)
    {
        string weaponName = data.VarName;
        int maxWeaponLevel = 5;
        LevelUpManager levelUpManager = LevelUpManager.Instance;
        bool containsWeapon = currentWeaponList.Contains(data);

        if (containsWeapon)
        {
            switch (weaponName)
            {
                case "Axe":
                    if (++levelUpManager.level_Axe >= maxWeaponLevel)
                    {
                        weaponsList.Remove(data);
                        currentWeaponList.Remove(data);
                    }
                    break;
                case "Crossbow":
                    if (++levelUpManager.level_Crossbow >= maxWeaponLevel)
                    {
                        weaponsList.Remove(data);
                        currentWeaponList.Remove(data);
                    }
                    break;
                case "Necronomicon":
                    if (++levelUpManager.level_Necronomicon >= maxWeaponLevel)
                    {
                        weaponsList.Remove(data);
                        currentWeaponList.Remove(data);
                    }
                    break;
                case "Arrow":
                    if (++levelUpManager.level_Arrow >= maxWeaponLevel)
                    {
                        weaponsList.Remove(data);
                        currentWeaponList.Remove(data);
                    }
                    break;
            }
        }
        else
        {
            currentWeaponList.Add(data);
            currentWeapons++;

            switch (weaponName)
            {
                case "Axe":
                    WeaponContainer.Instance.have_Axe = true;
                    levelUpManager.level_Axe++;
                    break;
                case "Crossbow":
                    Instantiate(data.Prefab);
                    levelUpManager.level_Crossbow++;
                    break;
                case "Necronomicon":
                    Instantiate(data.Prefab);
                    levelUpManager.level_Necronomicon++;
                    break;
                case "Arrow":
                    levelUpManager.level_Arrow++;
                    break;
            }
        }

        ResetData();

        PlayerController playerController = PlayerController.Instance;
        playerController.pointsLvl--;
        levelUpManager.WindowLevelState();
    }

    void ResetData()
    {
        leftData = null;
        midData = null;
        rightData = null;

        MidWeapon();
        RightWeapon();
        LeftWeapon();
    }

    private void OnEnable()
    {
        texts = LanguageManager.Instance.texts;

        newItem = texts[newItemNum];
        upgradeItem = texts[upgradeNum];
    }

    private bool MaxWeapons()
    {
        return currentWeapons > 3;
    }
}