using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kart
{
    public Transform kartTrans;
    public float dist;
}
public class Standings : MonoBehaviour
{
    public ObjectList kartsContainer;
    public List<CPU> kartList;
    public List<kart> kartPosList;

    public List<Transform> wayList;

    int currentNum=1;
    // Start is called before the first frame update
    private void Awake()
    {
        wayList = CPU.waypoints;
        kartList = kartsContainer;
        float size = kartList.Count;
        Debug.Log("SIZE: "+size);

        for (int i = 0; i < size; i++)
        {
            kart newKart = new kart();
            newKart.kartTrans = kartList[i];

            CPU.Position = i+1;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentIndex = 0;

    }
}
