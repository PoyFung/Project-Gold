using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    public List<Transform> list;

    private void Awake()
    {
        /*
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            list.Add(tr);
        }
        list.Remove(list[0]);
        */
        list = new List<Transform>();

        // Iterate over immediate children
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            list.Add(child);
        }
    }
}
