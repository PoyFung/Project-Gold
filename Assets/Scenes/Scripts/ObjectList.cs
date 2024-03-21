using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    public List<Transform> list;

    private void Awake()
    {
        list = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            list.Add(child);
        }
    }
}
