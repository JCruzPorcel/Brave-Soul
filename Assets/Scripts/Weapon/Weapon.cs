using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    private void OnEnable()
    {
        switch (weaponData.VarName)
        {
            case "Axe":
                UIManager.Instance.Axe(weaponData);
                Debug.Log(weaponData.VarName);
                break;
            case "Crossbow":
                Debug.Log(weaponData.VarName);
                break;
            case "Necronomicon":
                Debug.Log(weaponData.VarName);
                break;
            default:
                Debug.Log("Weapon:" + weaponData.VarName + "Not Found.");
                break;
        }
    }
}