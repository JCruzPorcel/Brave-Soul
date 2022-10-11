using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();

    public static void PreLoad(string type, GameObject objectToPool, int amount)
    {
        int id = objectToPool.GetInstanceID();

        GameObject parent = new GameObject();
        parent.name = type + ": " + objectToPool.name + " Pool";
        parents.Add(id, parent);


        pool.Add(id, new Queue<GameObject>());

        for (int i = 0; i < amount; i++)
        {
            CreateObject(objectToPool, objectToPool.name, i);
        }
    }

    static void CreateObject(GameObject objectToPool, string tag, int? amount)
    {
        int id = objectToPool.GetInstanceID();

        GameObject go = Instantiate(objectToPool) as GameObject;
        go.transform.SetParent(GetParent(id).transform);
        go.name = tag + " " + amount;

        go.SetActive(false);

        pool[id].Enqueue(go);
    }

    public static GameObject GetParent(int parentID)
    {
        GameObject parent;
        parents.TryGetValue(parentID, out parent);

        return parent;
    }

    public static GameObject GetObject(GameObject objectToPool)
    {
        int id = objectToPool.GetInstanceID();

        if (pool[id].Count == 0)
        {
            CreateObject(objectToPool, null, null);
        }

        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;
    }

    public static void RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    {
        int id = objectToPool.GetInstanceID();

        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive(false);
    }
}
