using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : Singleton<WeaponContainer>
{
    public WeaponData mainWeapon;

    public List<GameObject> axeList = new List<GameObject>();
    Queue<GameObject> axeQueue = new Queue<GameObject>();

    [SerializeField] Transform container;
    [SerializeField] Transform player;

    public GameObject axeGo;

    public float attackSpeedAxe;

    public bool have_Axe;

    public int axe_Amount;

    float timer;

    private void Start()
    {
        mainWeapon = GameManager.Instance.CharSelected.StartWeapon;

        attackSpeedAxe = axeGo.GetComponent<Axe>().attackSpeed;
        axe_Amount = axeGo.GetComponent<Axe>().amount;



        if (mainWeapon.VarName == "Axe")
        {
            for (int i = 0; i < axe_Amount; i++)
            {
                GameObject go = Instantiate(mainWeapon.Prefab, container);
                axeList.Add(go);
                go.SetActive(false);

                axeGo = axeList[0];
            }
            have_Axe = true;
        }
        else
        {
            Instantiate(mainWeapon.Prefab, player);
        }
    }



    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (have_Axe)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    if (axeList != null)
                    {
                        foreach (GameObject go in axeList)
                        {
                            if (!go.activeInHierarchy)
                            {
                                axeQueue.Enqueue(go);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < axe_Amount; i++)
                        {
                            GameObject go = Instantiate(mainWeapon.Prefab, container);
                            axeList.Add(go);
                            go.SetActive(false);
                        }
                    }

                    if (axeQueue.Count >= axe_Amount)
                    {
                        for (int i = 0; i < axe_Amount; i++)
                        {
                            GameObject go = axeQueue.Dequeue();
                            go.SetActive(true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < axe_Amount; i++)
                        {
                            GameObject go = Instantiate(axeGo, container);
                            axeList.Add(go);
                        }
                    }


                    attackSpeedAxe = axeGo.GetComponent<Axe>().attackSpeed;
                    axe_Amount = axeGo.GetComponent<Axe>().amount;

                    timer = attackSpeedAxe;
                }

            }
        }
    }
}
