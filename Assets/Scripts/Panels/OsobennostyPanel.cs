using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsobennostyPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public Button b_Left;
    public Button b_Right;
    
    [Header("Нижнее меню")]
    public Button b_Genplan;
    public Button b_ViborKvartiri;
    public Button b_Galereya;
    //public Button b_Osobennosty;

    [Header("Верхнее меню")] 
    public Button b_Blagoustroystvo;
    public Button b_Infrastruktura;
    public Button b_Lokaciya;
    public Button b_Lobby;
    public Button b_Planirovki;
    public Button b_Parkovki;
    public Button b_Ingeneriya;
    public Button b_Uniesystemi;
    
    [Header("Особенности")]
    public List<GameObject> lokacija = new List<GameObject>();
    public List<GameObject> infrostructura = new List<GameObject>();
    public List<GameObject> blagoustroystvo = new List<GameObject>();
    public List<GameObject> lobby = new List<GameObject>();
    public List<GameObject> planirivki = new List<GameObject>();
    public List<GameObject> parkovki = new List<GameObject>();
    public List<GameObject> ingeneriya = new List<GameObject>();
    public List<GameObject> umniesystemi = new List<GameObject>();
    
    private List<GameObject> _panels = new List<GameObject>();
    private int _selectedPanel = 0;
    private List<GameObject> _allPanels = new List<GameObject>();
    
    public void Init()
    {
        _manager = GameManager.instance;
        b_Left.onClick.AddListener(OnLeftClick);
        b_Right.onClick.AddListener(OnRightClick);
        b_Genplan.onClick.AddListener(OnGenplan);
        b_ViborKvartiri.onClick.AddListener(OnViborKvartiri);
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Lokaciya.onClick.AddListener(OnLokaciya);
        b_Blagoustroystvo.onClick.AddListener(OnBlagoustroystvo);
        b_Infrastruktura.onClick.AddListener(OnInfrastruktura);
        b_Lobby.onClick.AddListener(OnLobby);
        b_Planirovki.onClick.AddListener(OnPlanirovki);
        b_Parkovki.onClick.AddListener(OnParkovki);
        b_Ingeneriya.onClick.AddListener(OnIngeneriya);
        b_Uniesystemi.onClick.AddListener(OnUmniesystemi);

        SpashalImageClass[] spashalImageClasses = GetComponentsInChildren<SpashalImageClass>(true);
        foreach (var imageClass in spashalImageClasses)
        {
            imageClass.Init();
        }
        _allPanels.AddRange(lokacija);
        _allPanels.AddRange(infrostructura);
        _allPanels.AddRange(blagoustroystvo);
        _allPanels.AddRange(lobby);
        _allPanels.AddRange(planirivki);
        _allPanels.AddRange(parkovki);
        _allPanels.AddRange(ingeneriya);
        _allPanels.AddRange(umniesystemi);
        Hide();
    }

    private void HideAllPanels()
    {
        foreach (var o in _allPanels)
        {
            o.SetActive(false);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnLokaciya();
    }

    public void Hide()
    {
        HideAllPanels();
        gameObject.SetActive(false);
    }

    private void OnLeftClick()
    {
        foreach (var panel in _panels)   
        {
            panel.SetActive(false);
        }
        _selectedPanel--;
        if (_selectedPanel <= 0)
        {
            _selectedPanel = 0;
            b_Left.gameObject.SetActive(false);
        }
        b_Right.gameObject.SetActive(true);
        _panels[_selectedPanel].SetActive(true);
    }

    private void OnRightClick()
    {
        foreach (var panel in _panels)   
        {
            panel.SetActive(false);
        }
        _selectedPanel++;
        if (_selectedPanel >= _panels.Count-1)
        {
            _selectedPanel = _panels.Count - 1;
            b_Right.gameObject.SetActive(false);
        }
        b_Left.gameObject.SetActive(true);
        _panels[_selectedPanel].SetActive(true);
    }


    //Нижнее меню!!!
    private void OnGenplan()
    {
        
    }

    private void OnViborKvartiri()
    {
        
    }

    private void OnGalereya()
    {
        
    }

    private void OnOsobennosty()
    {
        
    }

    
    //Верхнее меню!!!
    private void OnBlagoustroystvo()
    {
        InitPanels(blagoustroystvo);
    }

    private void OnInfrastruktura()
    {
        InitPanels(infrostructura);
    }

    private void OnLokaciya()
    {
        InitPanels(lokacija);
    }
    
    private void OnLobby()
    {
        InitPanels(lobby);
    }
    
    private void OnPlanirovki()
    {
        InitPanels(planirivki);
    }
    
    private void OnParkovki()
    {
        InitPanels(parkovki);
    }
    
    private void OnIngeneriya()
    {
        InitPanels(ingeneriya);
    }
    
    private void OnUmniesystemi()
    {
        InitPanels(umniesystemi);
    }

    private void InitPanels(List<GameObject> panels)
    {
        HideAllPanels();
        _panels.Clear();
        _panels = new List<GameObject>(panels);
        _panels[0].SetActive(true);
        if (_panels.Count == 1)
        {
            b_Right.gameObject.SetActive(false);
            b_Left.gameObject.SetActive(false);
        }
        else
        {
            b_Right.gameObject.SetActive(true);
            b_Left.gameObject.SetActive(false);
        }

        foreach (var panel in _panels)
        {
            SpashalImageClass[] spashalImage = panel.GetComponentsInChildren<SpashalImageClass>(true);
            foreach (var imageClass in spashalImage)
            {
                imageClass.Show();
            }
        }
    }

}
