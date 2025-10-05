using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    [HideInInspector] public SerializeXML serializeXML;
    [HideInInspector] public GalereyaPanel galereyaPanel;
    [HideInInspector] public StartPanel startPanel;
    [HideInInspector] public OsobennostyPanel osobennostyPanel;
    [HideInInspector] public MyDataClass myDataClass;
    [HideInInspector] public CreateImagePNG createImagePng;

    public GameObject loadPanel;
    public TMP_Text InfoStartPanel;
    
    [HideInInspector] public FeedClass Feed;
    [HideInInspector] public MyData MyData;

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
        myDataClass = FindObjectOfType<MyDataClass>(true);
        createImagePng = FindObjectOfType<CreateImagePNG>(true);
        
        await serializeXML.Init();

        galereyaPanel.Init();
        startPanel.Init();
        osobennostyPanel.Init();
        myDataClass.Init();
        StartCoroutine(StartGame());

        foreach (var building in MyData.Buildings)
        {
            Debug.Log("XX: "+ building.Korpus + " " + building.MyObjects.Count);
            foreach (var myObject in building.MyObjects)
            {
                Debug.Log(myObject.Status); //На этаже
               
            }
        }

    }

    IEnumerator StartGame()
    {
        loadPanel.SetActive(true);
        
        yield return StartCoroutine(createImagePng.Init());
        yield return StartCoroutine(myDataClass.CreateSprites());
        
        loadPanel.SetActive(false);
    }




}
