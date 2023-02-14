using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomWeaponSelector : MonoBehaviour
{
    public List<WeaponData> weapons;
    public WeaponData currentWeapon;
    public Image currentWeaponImage;
    public float animationTime = 3f;
    public float initialAnimationSpeed = 0.1f;
    [SerializeField] Animator animator;

    public GameObject button;

    public void ChangeWeapon()
    {
        int randomIndex = Random.Range(0, weapons.Count);
        StartCoroutine(ShowWeaponAnimation(randomIndex));
        button.SetActive(false);
    }

    IEnumerator ShowWeaponAnimation(int weaponIndex)
    {
        float animationSpeed = initialAnimationSpeed;
        float timeBetweenChanges = animationSpeed;
        int weaponsShown = 0;

        animator.SetBool("SizeAnim", true);

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

        currentWeapon = weapons[weaponIndex];


        GiveWeapon(currentWeapon);
    }


    void GiveWeapon(WeaponData weapon)
    {
        Debug.Log("SIuuu");
    }
}
