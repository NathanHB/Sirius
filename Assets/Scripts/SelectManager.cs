using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    // Here objects w/ tag 'Werewolf' change their Material when we point them.
    [SerializeField] private string selectableTag = "Werewolf";
    [SerializeField] private Material other;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    void Update()
    {
        
        if (_selection != null)
        {
            var selectRenderer = _selection.GetComponent<Renderer>();
            selectRenderer.material = defaultMaterial;
            _selection = null;
        }
            
            
        var ray = Camera.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var select = hit.transform;
            if (select.CompareTag(selectableTag))
            {
                var selectRenderer = select.GetComponent<Renderer>();
                if (selectRenderer != null)
                {
                    selectRenderer.material = other;
                }

                _selection = select;
            }
            
            
        }

    }
}
