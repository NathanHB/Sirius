using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SelectManager : MonoBehaviour
{
    // Here objects w/ tag 'Werewolf' change their Material when we point them.
    [SerializeField] private string selectableTagWerewolf = "Werewolf";
    [SerializeField] private string selectableTagItem = "Item";
    [SerializeField] private Material other;
    [SerializeField] private Material defaultMaterial;
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
                    elt.GetComponent<Outline>().eraseRenderer=true;
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
                    select.GetComponent<Outline>().eraseRenderer=false;
         
            }
            
            
        }

    }
}
