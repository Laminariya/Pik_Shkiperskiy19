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
    [HideInInspector] public ChoseOnParameterPanel choseOnParameterPanel;

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
        choseOnParameterPanel = FindObjectOfType<ChoseOnParameterPanel>(true);
        
        await serializeXML.Init();
        
        galereyaPanel.Init();
        startPanel.Init();
        osobennostyPanel.Init();
        myDataClass.Init();
        flatPanel.Init();
        
        choseOnParameterPanel.Init();
        StartCoroutine(StartGame());

        foreach (var building in MyData.Buildings)
        {
            //Debug.Log("XX: "+ building.Korpus + " " + building.MyObjects.Count);
            foreach (var myObject in building.MyObjects)
            {
                // if (myObject.Number == 221)
                // {
                //     Debug.Log(myObject.CountRooms);
                //     Debug.Log(myObject.ObjectClass.Type);
                //     Debug.Log(myObject.ObjectClass.Category);
                //     Debug.Log(myObject.ObjectClass.Name);
                //     Debug.Log(myObject.ObjectClass.Studio);
                // }
                // if (myObject.Number == 56)
                // {
                //     Debug.Log(myObject.CountRooms);
                //     Debug.Log(myObject.ObjectClass.Type);
                //     Debug.Log(myObject.ObjectClass.Category);
                //     Debug.Log(myObject.ObjectClass.Name);
                //     Debug.Log(myObject.ObjectClass.Studio);
                // }
                // if (myObject.Number == 227)
                // {
                //     Debug.Log(myObject.CountRooms);
                //     Debug.Log(myObject.ObjectClass.Type);
                //     Debug.Log(myObject.ObjectClass.Category);
                //     Debug.Log(myObject.ObjectClass.Name);
                //     Debug.Log(myObject.ObjectClass.Studio);
                // }
                //Debug.Log(myObject.CountRooms);
            }
        }

    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         flatPanel.Show(MyData.Buildings[0].MyObjects[5]);
    //     }
    // }

    IEnumerator StartGame()
    {
        loadPanel.SetActive(true);
        Debug.Log("Load Panel");
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

    public void MessageOnHouse(int house, int porch, bool isOn = true)
    {
        //Debug.Log(house+" " + porch);
        //HH02PP0300000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "02";
        string por = porch.ToString("X");
        if(por.Length==1) por = "0" + por;
        str += por;
        if (isOn) str += "0300000000";
        else str += "0000000000";
        Debug.Log("Mess House");
        //sendComPort.AddMessage(str);
    }

    public void MessageOnFlat(int house, int porch, int flat, bool isOn = true)
    {
        //HH01FFFF03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "01";
        string f = flat.ToString("X");
        if (f.Length == 1) f = "000" + f;
        else if (f.Length == 2) f = "00" + f;
        else if (f.Length == 3) f = "0" + f;
        if (isOn) f += "03000000";
        else f += "00000000";
        str += f;
        Debug.Log("Mess Flat");
        //sendComPort.AddMessage(str);
    }

    public void MessageOnFloor(int house, int porch, int floor)
    {
        //HH03SSXX03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "03";
        string f = floor.ToString("X");
        if (f.Length == 1) f = "0" + f;
        str += f;
        string s = porch.ToString("X");
        if (s.Length == 1) s = "0" + s;
        str += s + "03000000";
        Debug.Log("Mess Floor");
        //sendComPort.AddMessage(str);
    }

    public void MessageOffAllLight()
    {
        Debug.Log("Mess OffAll");
        //sendComPort.AddMessage("007F060100000000"); //Погасить всё!!!
    }

    public void MessageOnDemo()
    {
        Debug.Log("Mess Demo");
        //sendComPort.AddMessage("0064010000000000"); //Включить демо!
    }

    public void OnLogoClicked()
    {
        //mainPanel.OnBackMainPage();
        //floorPanel.Hide();
        //choseFlatOnParameter.OnClose();
        //cartFlatPanel.Hide();
    }

}
