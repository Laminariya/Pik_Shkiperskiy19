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
    [HideInInspector] public FlatPanel flatPanel;

    public GameObject loadPanel;
    public TMP_Text InfoStartPanel;
    
    [HideInInspector] public FeedClass Feed;
    [HideInInspector] public MyData MyData;
    [HideInInspector] public string SymvolQuadro = "<sup>2</sup>";
    [HideInInspector] public string SymvolRuble = "\u20BD";

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
        flatPanel = FindObjectOfType<FlatPanel>(true);
        
        await serializeXML.Init();

        galereyaPanel.Init();
        startPanel.Init();
        osobennostyPanel.Init();
        myDataClass.Init();
        flatPanel.Init();
        
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            flatPanel.Show(MyData.Buildings[0].MyObjects[5]);
        }
    }

    IEnumerator StartGame()
    {
        loadPanel.SetActive(true);
        
        yield return StartCoroutine(createImagePng.Init());
        yield return StartCoroutine(myDataClass.CreateSprites());
        
        loadPanel.SetActive(false);
    }

    public string GetSplitPrice(int price)
    {
        string result = price.ToString();
        int count = result.Length;

        if (count > 3)
            result = result.Insert(result.Length - 3, " ");
        if(count > 6)
            result = result.Insert(result.Length - 7, " ");
        if(count > 9)
            result = result.Insert(result.Length - 11, " ");
        return result;
    }
    
    public string GetShortPrice(int price)
    {
        string p = (price / 1000000f).ToString();
        if(p.Length>=4)
            p = p.Substring(0, 4);
        return p;
    }


}
