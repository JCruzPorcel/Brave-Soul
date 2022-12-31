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

    int random_Mid;


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

    int random_Right;




    void LeftWeapon()
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

    bool MaxWeapons()
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

        Debug.Log(maxWeapon);

        return maxWeapon;

    }


    private void OnEnable()
    {
        LeftWeapon();
        MidWeapon();
        RightWeapon();
    }

    private void OnDisable()
    {
        leftData = null;
        midData = null;
        rightData = null;
    }


    public void LeftWeaponSpawn()
    {
        Debug.Log("L");
    }

    public void MidWeaponSpawn()
    {
        Debug.Log("M");
    }

    public void RightWeaponSpawn()
    {
        Debug.Log("R");
    }

}