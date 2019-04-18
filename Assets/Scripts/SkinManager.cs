using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    
    private static GameObject[] skinList;
    // Start is called before the first frame update
    void Start()
    {
        skinList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            skinList[i] = transform.GetChild(i).gameObject;
        }
    }

    // The villager is child 0
    // the wolf is child 1
    
    // Update is called once per frame
    public void ChangeToWolf()
    {
        for (int i = 0; i < skinList.Length; i++)
        {
            if (i == 0)
                skinList[i].SetActive(true);
            else 
                skinList[i].SetActive(false);
        }
        
        Debug.Log("Changed To Wolf.");
    }

    public void ChangeToVillager()
    {
        for (int i = 0; i < skinList.Length; i++)
        {
            if (i == 1)
                skinList[i].SetActive(true);
            else 
                skinList[i].SetActive(false);
        }
        Debug.Log("Changed to villager");
    }
}
