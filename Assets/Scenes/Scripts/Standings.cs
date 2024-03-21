using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class kart
{
    [SerializeField]
    public PlayerController playerKart=null;
    public CPU cpuKart=null;
    public int numWay;
    public float dist;
}
public class Standings : MonoBehaviour
{
    public List<kart> kartList;

    // Start is called before the first frame update
    private void Awake()
    {
        kartList = new List<kart>();
        for (int i = 0; i < transform.childCount; i++)
        {
            kart newKart = new kart();
            Transform childTrans = transform.GetChild(i);

            if (childTrans.GetComponent<PlayerController>() != null)
            {
                PlayerController childPlayer = childTrans.GetComponent<PlayerController>();
                newKart.playerKart = childPlayer;
                newKart.dist = childPlayer.distFromWaypoint;
                newKart.numWay = childPlayer.passedWaypoints;
                kartList.Add(newKart);
                childPlayer.Position = i + 1;
            }

            else if (childTrans.GetComponent<CPU>()!=null)
            {
                CPU childCPU = childTrans.GetComponent<CPU>();
                newKart.cpuKart = childCPU;
                newKart.dist = childCPU.distFromWaypoint;
                newKart.numWay = childCPU.passedWaypoints;
                kartList.Add(newKart);
                childCPU.Position = i + 1;
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        for (int i = 0; i < kartList.Count; i++)
        {
            if (kartList[i].playerKart!=null)
            {
                PlayerController currentKart = kartList[i].playerKart;
                kartList[i].dist = currentKart.distFromWaypoint;
                kartList[i].numWay = currentKart.passedWaypoints;
                kartList = kartList.OrderByDescending(kart => kart.numWay).ToList();
                kartList = kartList.GroupBy(kart => kart.numWay)
                           .SelectMany(group => group.OrderBy(kart => kart.dist))
                           .ToList();

                currentKart.Position = kartList.IndexOf(kartList[i])+1;
                GUI.Pos = kartList.IndexOf(kartList[i]) + 1;
            }

            else if (kartList[i].cpuKart != null)
            {
                CPU currentKart = kartList[i].cpuKart;
                kartList[i].dist = currentKart.distFromWaypoint;
                kartList[i].numWay = currentKart.passedWaypoints;
                kartList = kartList.OrderByDescending(kart => kart.numWay).ToList();
                kartList = kartList.GroupBy(kart => kart.numWay)
                           .SelectMany(group => group.OrderBy(kart => kart.dist))
                           .ToList();
                currentKart.Position = kartList.IndexOf(kartList[i])+1;
            }
        }
    }
}
