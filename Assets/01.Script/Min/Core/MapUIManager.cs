using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUIManager : MonoSingleTon<MapUIManager>
{
    public GameObject mapPanel;

   public void OnBattle(bool _isOn)
    {
        mapPanel.SetActive(!_isOn);        
    }
    
}
