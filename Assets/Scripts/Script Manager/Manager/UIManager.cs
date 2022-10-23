using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{/*
    // LVL UP - RANDOM WEAPON SELECTOR
    [Header("Left Item")]
    [SerializeField] TMP_Text name1;
    [SerializeField] TMP_Text description1;
    [SerializeField] Image image1;

    [SerializeField] GameObject evContainer1;
    [SerializeField] Image evImage1;
    [SerializeField] TMP_Text evDescription1;

    [Header("Middle Item")]
    [SerializeField] TMP_Text name2;
    [SerializeField] TMP_Text description2;
    [SerializeField] Image image2;

    [SerializeField] GameObject evContainer2;
    [SerializeField] Image evImage2;
    [SerializeField] TMP_Text evDescription2;

    [Header("Right Item")]
    [SerializeField] TMP_Text name3;
    [SerializeField] TMP_Text description3;
    [SerializeField] Image image3;

    [SerializeField] GameObject evContainer3;
    [SerializeField] Image evImage3;
    [SerializeField] TMP_Text evDescription3;

    [Space(30)]
    [SerializeField] GameObject hud;

    int randomItem;
    int lastItem1;
    int lastItem2;
    int lastItem3;

    //WEAPON VARIABLE AND REFERENCE'S
    //Have this Weapon?    
    public bool hAxe, hNecronomicon, hCrossbow, hProjectile;
    //Weapon Attack Speed
    public float tAxe, tNecronomicon, tCrossbow, tProjectile;
    //Weapon Level
    public int lAxe, lNecronomicon, lCrossbow, lProjectile;
    //Weapon Damage
    public int dAxe, dNecronomicon, dCrossbow, dProjectile;
    //More Weapon
    public int mAxe, mNecronomicon;

    [Space(50)]
    public List<WeaponData> weaponList = new List<WeaponData>();
    static Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        foreach (WeaponData weapon in weaponList)
        {
            dictionary.Add(weapon.VarName, weapon.Prefab);

            ObjectPooling.PreLoad(weapon.VarName, weapon.Prefab, weapon.Amount);
        }
    }
    public void LevelUp()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        hud.SetActive(true);

        randomItem = Random.Range(0, weaponList.Count);

        lastItem1 = randomItem;

        name1.text = weaponList[lastItem1].ItemName.ToString();
        description1.text = weaponList[lastItem1].Description.ToString();
        image1.sprite = weaponList[lastItem1].Image;

        if (weaponList[lastItem1].CanEvolve)
        {
            evContainer1.SetActive(true);
            evImage1.sprite = weaponList[lastItem1].EvImage;
            evDescription1.text = weaponList[lastItem1].EvDescription.ToString();
        }
        else
        {
            evContainer1.SetActive(false);
        }

        randomItem = Random.Range(0, weaponList.Count);

        while (randomItem == lastItem1)
        {
            randomItem = Random.Range(0, weaponList.Count);
        }

        lastItem2 = randomItem;

        name2.text = weaponList[lastItem2].ItemName.ToString();
        description2.text = weaponList[lastItem2].Description.ToString();
        image2.sprite = weaponList[lastItem2].Image;

        if (weaponList[lastItem2].CanEvolve)
        {
            evContainer2.SetActive(true);
            evImage2.sprite = weaponList[lastItem2].EvImage;
            evDescription2.text = weaponList[lastItem2].EvDescription.ToString();
        }
        else
        {
            evContainer2.SetActive(false);
        }

        randomItem = Random.Range(0, weaponList.Count);

        while (randomItem == lastItem1 || randomItem == lastItem2)
        {
            randomItem = Random.Range(0, weaponList.Count);
        }

        lastItem3 = randomItem;

        name3.text = weaponList[lastItem3].ItemName.ToString();
        description3.text = weaponList[lastItem3].Description.ToString();
        image3.sprite = weaponList[lastItem3].Image;

        if (weaponList[lastItem3].CanEvolve)
        {
            evContainer3.SetActive(true);
            evImage3.sprite = weaponList[lastItem3].EvImage;
            evDescription3.text = weaponList[lastItem3].EvDescription.ToString();
        }
        else
        {
            evContainer3.SetActive(false);
        }
    }
    public void AddLeftWeapon()
    {
        WeaponCheck(lastItem1);

        hud.SetActive(false);
        PlayerController.Instance.pointsLvl--;
        if (PlayerController.Instance.pointsLvl > 0)
        {
            LevelUp();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }
    public void AddMiddleWeapon()
    {
        WeaponCheck(lastItem2);

        hud.SetActive(false);
        PlayerController.Instance.pointsLvl--;

        if (PlayerController.Instance.pointsLvl > 0)
        {
            LevelUp();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }
    public void AddRightWeapon()
    {
        WeaponCheck(lastItem3);

        hud.SetActive(false);
        PlayerController.Instance.pointsLvl--;

        if (PlayerController.Instance.pointsLvl > 0)
        {
            LevelUp();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }
    void WeaponCheck(int value)
    {
        string randomKey = dictionary.Keys.ToArray()[value];

        switch (randomKey)
        {
            case "Axe":
                hAxe = true;
                lAxe++;

                break;

            case "Crossbow":

                break;

            case "Necronomicon":

                break;

            case "Projectile":

                break;

            default:
                Debug.Log(randomKey + ": WEAPON NOT FOUND");

                break;
        }
    }

    public void Axe(WeaponData weaponData)
    {
        if (hAxe)
        {
            if (tAxe >= 0f)
            {
                tAxe -= Time.deltaTime;
            }
            else
            {
                tAxe = 0f;
            }

            switch (lAxe)
            {
                case 1:
                    dAxe = weaponData.Damage;
                    Debug.Log(lAxe);
                    break;
                case 2:
                    dAxe += 5;
                    mAxe++;
                    Debug.Log(lAxe);

                    break;
                case 3:
                    dAxe += 10;
                    mAxe++;
                    Debug.Log(lAxe);
                    break;
                case 4:
                    dAxe += 5;
                    Debug.Log(lAxe);
                    break;
                case 5:
                    dAxe += 6;
                    Debug.Log(lAxe);
                    mAxe++;
                    break;
                case 6:
                    mAxe++;
                    Debug.Log(lAxe);
                    break;
                default:
                    Debug.Log("Error Axe Max Level Can't Override");
                    break;
            }



            if (tAxe == .0f)
            {
                tAxe = weaponData.AttackSpeed;
            }
        }
    }*/
}
