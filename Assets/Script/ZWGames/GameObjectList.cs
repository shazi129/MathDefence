using System.Collections.Generic;
using UnityEngine;

public class GameObjectList : MonoBehaviour
{
    /// <summary>
    /// for ui editor. save editor binder.
    /// </summary>
    public List<GameObject> GameObjectLst;

    public GameObject Find(string name)
    {
        foreach (var item in GameObjectLst)
        {
            if (item && item.name == name)
                return item;
        }
        return null;
    }

    public T Find<T>(string name) where T : Component
    {
        foreach (var item in GameObjectLst)
        {
            if (item && item.name == name)
            {
                return item.GetComponent<T>();
            }
        }
        return null;
    }

    public int Count { get { return GameObjectLst == null ? 0 : GameObjectLst.Count; } }

    public GameObject getIndex(int index)
    {
        if (GameObjectLst != null && index < GameObjectLst.Count)
        {
            return GameObjectLst[index];
        }
        return null;
    }
}