using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public Button b_Galereya;
    public Button b_Osobennosty;
    
    public void Init()
    {
        _manager = GameManager.instance;
        b_Galereya.onClick.AddListener(OnGalereyaClick);
        b_Osobennosty.onClick.AddListener(OnOsobennostyClick);
    }

    private void OnGalereyaClick()
    {
        _manager.galereyaPanel.Show();
    }

    private void OnOsobennostyClick()
    {
        _manager.osobennostyPanel.Show();
    }


}
