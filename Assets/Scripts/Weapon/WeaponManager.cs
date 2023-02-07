using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;


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

    PlayerInput playerInput;

    InputSystemUIInputModule inputModule;


    private void Awake()
    {
        currentWeaponList.Add(GameManager.Instance.CharSelected.StartWeapon);
        currentWeapons = 1;


        playerInput = GetComponent<PlayerInput>();

        inputModule = MenuManager.Instance.GetComponent<InputSystemUIInputModule>();

        playerInput.uiInputModule= inputModule;
    }

    private void Start()
    {
        LeftWeapon();
        MidWeapon();
        RightWeapon();
    }


    public void OnSelectWeaponLeft(InputValue value)
    {
        LeftWeaponSpawn();
    }

    public void OnSelectWeaponMid(InputValue value)
    {
        MidWeaponSpawn();
    }

    public void OnSelectWeaponRight(InputValue value)
    {
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

        leftData = GetNewData(leftData, midData, rightData, random_Left);
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

        midData = GetNewData(midData, leftData, rightData, random_Mid);
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
        rightData = GetNewData(rightData, midData, leftData, random_Right);
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


    WeaponData GetNewData(WeaponData data, WeaponData compareData1, WeaponData compareData2, int randomIntName)
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


        while (data == compareData1 || data == compareData2)
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

    void PlayerGetWeapon(WeaponData data)
    {
        if (currentWeaponList.Contains(data))
        {
            if (data.VarName == "Axe")
            {
                LevelUpManager.Instance.level_Axe++;

                if (LevelUpManager.Instance.level_Axe >= 5)
                {
                    weaponsList.Remove(data);
                    currentWeaponList.Remove(data);
                }
            }
            else if (data.VarName == "Crossbow")
            {
                LevelUpManager.Instance.level_Crossbow++;

                if (LevelUpManager.Instance.level_Crossbow >= 5)
                {
                    weaponsList.Remove(data);
                    currentWeaponList.Remove(data);
                }
            }
            else if (data.VarName == "Necronomicon")
            {
                LevelUpManager.Instance.level_Necronomicon++;

                if (LevelUpManager.Instance.level_Necronomicon >= 5)
                {
                    weaponsList.Remove(data);
                    currentWeaponList.Remove(data);
                }
            }
            else if (data.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;

                if (LevelUpManager.Instance.level_Arrow >= 5)
                {
                    weaponsList.Remove(data);
                    currentWeaponList.Remove(data);
                }
            }
        }
        else
        {
            currentWeaponList.Add(data);

            if (data.VarName == "Axe")
            {
                WeaponContainer.Instance.have_Axe = true;
                LevelUpManager.Instance.level_Axe++;
                currentWeapons++;
            }
            else if (data.VarName == "Crossbow")
            {
                Instantiate(data.Prefab);

                LevelUpManager.Instance.level_Crossbow++;
                currentWeapons++;
            }
            else if (data.VarName == "Necronomicon")
            {
                Instantiate(data.Prefab);

                LevelUpManager.Instance.level_Necronomicon++;
                currentWeapons++;
            }
            else if (data.VarName == "Arrow")
            {
                LevelUpManager.Instance.level_Arrow++;
            }
        }

        ResetData();

        PlayerController.Instance.pointsLvl--;
        LevelUpManager.Instance.WindowLevelState();
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