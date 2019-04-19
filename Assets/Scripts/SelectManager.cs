﻿using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    // Here objects w/ tag 'Werewolf' change their Material when we point them.
    [SerializeField] private string selectableTagWerewolf = "Werewolf";
    [SerializeField] private string selectableTagItem = "Item";
    [SerializeField] private Material other;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Camera cam;


    private Transform _selection;

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
                
  
                
            }
            
            
        }

    }
}
