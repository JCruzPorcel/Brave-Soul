using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public List<NavButtons> buttonList = new List<NavButtons>();


    void Start()
    {
        MenuManager.Instance.menuList.Clear();
        MenuManager.Instance.buttonList.Clear();
        
        foreach (GameObject go in list)
        {
            MenuManager.Instance.menuList.Add(go);
        }

        foreach (NavButtons go in buttonList)
        {
            MenuManager.Instance.buttonList.Add(go);
        }

    }
}
