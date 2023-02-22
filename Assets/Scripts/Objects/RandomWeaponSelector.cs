using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;


public class RandomWeaponSelector : MonoBehaviour
{
    public List<WeaponData> weapons;
    public WeaponData currentWeapon;
    public Image currentWeaponImage;
    public Sprite chestImage; 
    public float animationTime = 3f;
    public float initialAnimationSpeed = 0.1f;
    [SerializeField] Animator animator;
    AudioManager audioManager;

    public GameObject levelUp;

    public GameObject button;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();        
    }

    public void ChangeWeapon()
    {
        int randomIndex = Random.Range(0, weapons.Count);
        StartCoroutine(ShowWeaponAnimation(randomIndex));

        audioManager.Play("RandomWeapon SFX");
        button.SetActive(false);
    }

    IEnumerator ShowWeaponAnimation(int weaponIndex)
    {
        float animationSpeed = initialAnimationSpeed;
        float timeBetweenChanges = animationSpeed;
        int weaponsShown = 0;

        while (true)
        {
            WeaponData current = weapons[Random.Range(0, weapons.Count)];
            currentWeapon = current;

            currentWeaponImage.sprite = current.WeaponImage;

            if (weaponsShown >= 25)
            {
                break;
            }

            yield return new WaitForSeconds(timeBetweenChanges);
            weaponsShown++;
            timeBetweenChanges += (initialAnimationSpeed / weaponsShown);
        }

        audioManager.Stop("RandomWeapon SFX");
        audioManager.Play("SelectWeapon SFX");

        StartCoroutine(GiveWeapon(currentWeapon));
        //currentWeapon = weapons[weaponIndex];
    }


    IEnumerator GiveWeapon(WeaponData weapon)
    {
        levelUp.SetActive(true);
        WeaponManager.Instance.PlayerGetWeapon(weapon);
        levelUp.SetActive(false);

        MenuManager.Instance.OpenChest();

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
        MenuManager.Instance.InGame();
    }


    private void OnEnable()
    {
        currentWeaponImage.sprite = chestImage;
        button.SetActive(true);
    }
}
