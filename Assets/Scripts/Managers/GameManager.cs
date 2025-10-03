using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    [HideInInspector] public SerializeXML serializeXML;
    [HideInInspector] public GalereyaPanel galereyaPanel;
    [HideInInspector] public StartPanel startPanel;
    [HideInInspector] public OsobennostyPanel osobennostyPanel;
    
    [HideInInspector] public FeedClass Feed;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        LoadJSON();
    }
    
    private async Task LoadJSON()
    {
        serializeXML = FindObjectOfType<SerializeXML>(true);
        galereyaPanel = FindObjectOfType<GalereyaPanel>(true);
        startPanel = FindObjectOfType<StartPanel>(true);
        osobennostyPanel = FindObjectOfType<OsobennostyPanel>(true);
        
        await serializeXML.Init();

        galereyaPanel.Init();
        startPanel.Init();
        osobennostyPanel.Init();
        //StartCoroutine(StartGame());
    }
    
    

    
}
