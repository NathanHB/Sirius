using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SelectManager : MonoBehaviour
{
    // Here objects w/ tag 'Werewolf' change their Material when we point them.
    [SerializeField] private string selectableTagWerewolf = "Werewolf";
    [SerializeField] private string selectableTagItem = "Item";
    [SerializeField] private Material other;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material walnut;
    [SerializeField] private Material walnutOutlined;
    [SerializeField] private Material potion;
    [SerializeField] private Material potionOutlined;
    [SerializeField] private Camera cam;

    private Transform _selection;

    private List<Transform> lookedObjects = new List<Transform>();
    
    void Update()
    {

        
        if (_selection != null)
        {
            var selectRenderer = _selection.GetComponent<Renderer>();
            selectRenderer.material = defaultMaterial;
            _selection = null;
        }
            
            
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
             if (lookedObjects.Count>0)
            {
                 foreach (var elt in lookedObjects)
                {
                    if (elt.name=="rifleContent")
                        elt.GetComponent<Renderer>().material=walnut;
                    else if (elt.name=="potion")
                        elt.GetComponent<Renderer>().material=potion;
                }
            }
            

            lookedObjects.Clear();

            var select = hit.transform;
            if (select.CompareTag(selectableTagWerewolf))
            {
                var selectRenderer = select.GetComponent<Renderer>();
                if (selectRenderer != null)
                {
                    selectRenderer.material = other;
                }

                _selection = select;
            }
            else if (select.CompareTag(selectableTagItem))
            {
                lookedObjects.Add(select);
                if (select.name=="rifleContent")
                    select.GetComponent<Renderer>().material = walnutOutlined;
                else if (select.name=="potion")
                    select.GetComponent<Renderer>().material=potionOutlined;
                
                   
         
            }
            
            
        }

    }
}
