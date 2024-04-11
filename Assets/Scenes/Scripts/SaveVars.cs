using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVars : MonoBehaviour
{
    public static List <float> savedTimes = new List <float>();
    public static string key = "floatList";
    public bool hasRun = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
        foreach (var x in savedTimes)
        {
            Debug.Log(x.ToString());
            //PlayerPrefs.DeleteAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (hasRun == false)
        {
            addTime(2.4f);
            hasRun = true;
        }
        */
    }

    public static void addTime(float newTime)
    {
        if (savedTimes.Count == GameNavigation.TrialResults.Count)
        {
            for (int i = 0; i < savedTimes.Count; i++)
            {
                if (newTime < savedTimes[i])
                {
                    savedTimes.RemoveAt(savedTimes.Count - 1);
                    savedTimes.Add(newTime);
                    savedTimes.Sort();
                    Save();
                    break;
                }
            }
        }

        if (savedTimes.Count < GameNavigation.TrialResults.Count)
        {
            savedTimes.Add(newTime);
            savedTimes.Sort();
            Save();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetString(key, string.Join(",", savedTimes));
    }

    public static void Load()
    {
        savedTimes.Clear();
        if (PlayerPrefs.HasKey(key))
        {
            foreach (string s in PlayerPrefs.GetString(key).Split(',')) savedTimes.Add(float.Parse(s));
        }
    }
}
