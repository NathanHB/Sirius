using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SelectManager : MonoBehaviour
{
    // Here objects w/ tag 'Werewolf' change their Material when we point them.
    [SerializeField] private string selectableTagWerewolf = "Werewolf";
    [SerializeField] private string selectableTagItem = "Item";
    [SerializeField] private Material walnut;
    [SerializeField] private Material walnutOutlined;
    [SerializeField] private Material potion;
    [SerializeField] private Material potionOutlined;
    [SerializeField] private Material teddybear;
    [SerializeField] private Material teddybearOutlined;
    [SerializeField] private Camera cam;
    private bool isWerewolf;
    public GameObject player;
    public GameObject villagerSkin;

    private List<Transform> lookedObjects = new List<Transform>();

    void Start()
    {
        isWerewolf = player.tag=="Werewolf";
    }

    void Update()
    {


        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
             if (lookedObjects.Count>0)
            {
                 foreach (var elt in lookedObjects)
                {
                    if (elt.name == "rifleContent")
                        elt.GetComponent<Renderer>().material = walnut;
                    else if (elt.name == "potion")
                        elt.GetComponent<Renderer>().material = potion;
                    else if (elt.name == "teddyBear")
                        elt.GetComponent<Renderer>().material = teddybear;
                }
            }
            
            lookedObjects.Clear();

            Transform select = hit.transform;
            
            switchClass(select);
   
            if (select.CompareTag(selectableTagItem))
            {
                lookedObjects.Add(select);
                if (select.name=="rifleContent")
                    select.GetComponent<Renderer>().material = walnutOutlined;
                else if (select.name == "potion")
                    select.GetComponent<Renderer>().material = potionOutlined;
                else if (select.name=="teddyBear")                   
                    select.GetComponent<Renderer>().material = teddybearOutlined;
            }
            
            
        }

    }


    void switchClass(Transform select)
    {
        if (select.CompareTag(selectableTagItem) && Input.GetKeyDown(KeyCode.F) && villagerSkin.tag == "Untagged")
        {
            switch (select.name)
            {
                case "rifleContent":
                    villagerSkin.tag = "hunter";
                    break;
                case "potion":
                    villagerSkin.tag = "wizard";
                    break;
                case "teddyBear":
                    villagerSkin.tag = "littleGirl";
                    break;
                default:
                    villagerSkin.tag = "Untagged";
                    break;
            }  
            
            
           select.tag = "Untagged";
           select.parent.gameObject.SetActive(false);
        }   
    }
}
