using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuffExplainBoxUpdate : MonoBehaviour
{
    private Camera camera;
    private TextMeshProUGUI _expBox => transform.GetComponentInChildren<TextMeshProUGUI>();

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        transform.position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 150, 5));
    }
    
    
    
    
    

    public void SetExplain(string _exp)
    {
        _expBox.text = _exp;
    }
}
