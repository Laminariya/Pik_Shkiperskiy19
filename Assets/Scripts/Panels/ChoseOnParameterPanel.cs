using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseOnParameterPanel : MonoBehaviour
{

    public DubleSlider DubleSliderArea;
    public DubleSlider DubleSliderPrice;
    public DubleSlider DubleSliderFloor;
    public Button b_Show;
    public Button b_Reset;
    public Button b_Back;
    public Transform PrefabParent;
    public GameObject CartFlatPrefab;

    public Button b_St;
    public Button b_1;
    public Button b_2;
    public Button b_3;

    public Button b_Korpus1;
    public Button b_Korpus2;
    
    public TMP_Text MinArea;
    public TMP_Text MaxArea;
    public TMP_Text MinPrice;
    public TMP_Text MaxPrice;
    public TMP_Text MinFloor;
    public TMP_Text MaxFloor;
    
    private GameManager _manager;
    private List<CartFlatPrefab> _cartFlatPrefabs = new List<CartFlatPrefab>();
    
    private int _St;
    private int _1;
    private int _2;
    private int _3;
    
    private int _Korpus1;
    private int _Korpus2;
    
    private float _minArea;
    private float _maxArea;
    private float _minPrice;
    private float _maxPrice;
    private int _minFloor;
    private int _maxFloor;
    
    public void Init()
    {
        _manager = GameManager.instance;
        b_Show.onClick.AddListener(OnShowClick);
        b_Reset.onClick.AddListener(OnResetClick);
        b_Back.onClick.AddListener(OnBackClick);
        b_St.onClick.AddListener(OnStClick);
        b_1.onClick.AddListener(On1Click);
        b_2.onClick.AddListener(On2Click);
        b_3.onClick.AddListener(On3Click);
        b_Korpus1.onClick.AddListener(OnKorpus1Click);
        b_Korpus2.onClick.AddListener(OnKorpus2Click);
        DubleSliderArea.Action += OnDoubleSliderArea;
        DubleSliderPrice.Action += OnDoubleSliderPrice;
        DubleSliderFloor.Action += OnDoubleSliderFloor;
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnResetClick();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowKorpus1()
    {
        Show();
        _Korpus1 = 1;
        _Korpus2 = -1;
        b_Korpus1.image.color = Color.white;
        b_Korpus2.image.color = Color.clear;
    }

    public void ShowKorpus2()
    {
        Show();
        _Korpus1 = -1;
        _Korpus2 = 2;
        b_Korpus1.image.color = Color.clear;
        b_Korpus2.image.color = Color.white;
    }
    
    private void OnShowClick()
    {
        GameManager.instance.MessageOffAllLight();
        Debug.Log(_1 + " " + _2 + " " + _3 + " " + _St);
        for (int i = 0; i < _cartFlatPrefabs.Count; i++)
        {
            Destroy(_cartFlatPrefabs[i].gameObject);
        }
        _cartFlatPrefabs.Clear();
        
        foreach (var building in GameManager.instance.MyData.Buildings)
        {
            if (building.Korpus == _Korpus1 || building.Korpus == _Korpus2)
            {
                foreach (var myObject in building.MyObjects)
                {
                    if ((myObject.CountRooms == _1 ||
                         myObject.CountRooms == _2
                         || myObject.CountRooms == _3 || myObject.CountRooms == _St)
                        && myObject.Area <= _maxArea && myObject.Area >= _minArea &&
                        myObject.Price <= _maxPrice && myObject.Price >= _minPrice &&
                        myObject.Floor <= _maxFloor && myObject.Floor >= _minFloor)
                    {
                        CartFlatPrefab cart = Instantiate(CartFlatPrefab, PrefabParent).GetComponent<CartFlatPrefab>();
                        cart.Init(myObject);
                        _cartFlatPrefabs.Add(cart);
                        cart.OnSendMessageOnComPort();
                    }
                }
            }
        }
    }

    private void OnResetClick()
    {
        _St = 0;
        _1 = -1;
        _2 = -1;
        _3 = -1;
        _Korpus1 = 1;
        _Korpus2 = 2;
        b_Korpus1.image.color = Color.white;
        b_Korpus2.image.color = Color.white;
        b_St.image.color = Color.white;
        b_1.image.color = Color.clear;
        b_2.image.color = Color.clear;
        b_3.image.color = Color.clear;
        foreach (var flatPrefab in _cartFlatPrefabs)
        {
            Destroy(flatPrefab.gameObject);
        }
        _cartFlatPrefabs.Clear();
        ReloadSliders();
    }

    private void OnBackClick()
    {
        Hide();
    }

    private void OnStClick()
    {
        //Студию вычисляем по myObject.ObjectClass.Studio Если true то студия, если null то нет
        if (b_St.image.color == Color.clear)
        {
            b_St.image.color = Color.white;
            _St = 0;
        }
        else
        {
            b_St.image.color = Color.clear;
            _St = -1;
        }
        ReloadSliders();
    }

    private void On1Click()
    {
        if (b_1.image.color == Color.clear)
        {
            b_1.image.color = Color.white;
            _1 = 1;
        }
        else
        {
            b_1.image.color = Color.clear;
            _1 = -1;
        }
        ReloadSliders();
    }

    private void On2Click()
    {
        if (b_2.image.color == Color.clear)
        {
            b_2.image.color = Color.white;
            _2 = 2;
        }
        else
        {
            b_2.image.color = Color.clear;
            _2 = -1;
        }
        ReloadSliders();
    }

    private void On3Click()
    {
        if (b_3.image.color == Color.clear)
        {
            b_3.image.color = Color.white;
            _3 = 3;
        }
        else
        {
            b_3.image.color = Color.clear;
            _3 = -1;
        }
        ReloadSliders();
    }
    
    private void OnKorpus1Click()
    {
        if (b_Korpus1.image.color == Color.clear)
        {
            b_Korpus1.image.color = Color.white;
            _Korpus1 = 1;
        }
        else
        {
            b_Korpus1.image.color = Color.clear;
            _Korpus1 = -1;
        }
        ReloadSliders();
    }
    
    private void OnKorpus2Click()
    {
        if (b_Korpus2.image.color == Color.clear)
        {
            b_Korpus2.image.color = Color.white;
            _Korpus2 = 2;
        }
        else
        {
            b_Korpus2.image.color = Color.clear;
            _Korpus2 = -1;
        }
        ReloadSliders();
    }
    
     private void OnDoubleSliderArea(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in _manager.MyData.Buildings)
        {
            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 ||
                     myObject.CountRooms == _St)
                    && myObject.Area > max)
                {
                    max = myObject.Area;
                }
            }

            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 ||
                     myObject.CountRooms == _St)
                    && myObject.Area < min)
                {
                    min = myObject.Area;
                }
            }
        }

        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minArea = min + DubleSliderArea.LeftSlider.value * _delta;
        _maxArea = max - (1 - DubleSliderArea.RightSlider.value) * _delta;
        string min1Str = Math.Round(min, 1).ToString();
        string max1Str = Math.Round(max, 1).ToString();
        string min2Str = Math.Round(_minArea, 1).ToString();
        string max2Str = Math.Round(_maxArea, 1).ToString();
        MinArea.text = "от "+ min2Str;
        MaxArea.text = "до "+max2Str;

        if (min1Str != min2Str || max1Str != max2Str)
        {
            //FilterPointArea.Show(Math.Round(_minArea, 1) + "-" + Math.Round(_maxArea, 1) + "м2");
        }
        else
        {
            //FilterPointArea.Hide();
        }
        //ReloadCountFlat();
        
        //AreaRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //AreaRect.offsetMax-=Vector2.one;
    }

    private void OnDoubleSliderPrice(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in GameManager.instance.MyData.Buildings)
        {
            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 ||
                     myObject.CountRooms == _St)
                    && myObject.Price > max)
                {
                    max = myObject.Price;
                }
            }

            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 ||
                     myObject.CountRooms == _St)
                    && myObject.Price < min)
                {
                    min = myObject.Price;
                }
            }
        }

        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minPrice = min + DubleSliderPrice.LeftSlider.value * _delta;
        _maxPrice = max - (1 - DubleSliderPrice.RightSlider.value) * _delta;
        MinPrice.text =
            "от " + GameManager.instance.GetSplitPrice((int)_minPrice) + " " +
            _manager.SymvolRuble; //Math.Round(_minPrice, 1).ToString(); //_manager.GetShortPrice()
        MaxPrice.text =
            "до " + GameManager.instance.GetSplitPrice((int)_maxPrice) + " " +
            _manager.SymvolRuble; //Math.Round(_maxPrice, 1).ToString(); //_manager.GetShortPrice()

        string min1Str = Math.Round(min, 1).ToString();
        string max1Str = Math.Round(max, 1).ToString();
        string min2Str = Math.Round(_minPrice, 1).ToString();
        string max2Str = Math.Round(_maxPrice, 1).ToString();

        if (min1Str != min2Str || max1Str != max2Str)
        {
            // FilterPointPrice.Show(GameManager.Instance.GetShortPrice((int)_minPrice) + "-" +
            //                     GameManager.Instance.GetShortPrice((int)_maxPrice)  + "Р");
        }
        else
        {
            //FilterPointPrice.Hide();
        }

        //PriceRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //PriceRect.offsetMax-=Vector2.one;
    }

    private void OnDoubleSliderFloor(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in GameManager.instance.MyData.Buildings)
        {
            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 ||
                     myObject.CountRooms == _St)
                    && myObject.Floor > max)
                {
                    max = myObject.Floor;
                }
            }

            foreach (var myObject in building.MyObjects)
            {
                if ((myObject.CountRooms == _1 || myObject.CountRooms == _2 || myObject.CountRooms == _3 || myObject.CountRooms == _St)
                    && myObject.Floor < min)
                {
                    min = myObject.Floor;
                }
            }
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minFloor = (int)(min + DubleSliderFloor.LeftSlider.value * _delta);
        _maxFloor = (int)(max - (1 - DubleSliderFloor.RightSlider.value) * _delta);
        MinFloor.text = _minFloor.ToString();
        MaxFloor.text = _maxFloor.ToString();
        
        if (_minFloor != (int)min || _maxFloor != (int)max)
        {
            //FilterPointFloor.Show(_minFloor + "-" + _maxFloor);
        }
        else
        {
            //FilterPointFloor.Hide();
        }
        
        //FloorRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //FloorRect.offsetMax-=Vector2.one;
    }
    
    public void ReloadSliders()
    {
        DubleSliderArea.Init();
        DubleSliderFloor.Init();
        DubleSliderPrice.Init();
        OnDoubleSliderArea(1f);
        OnDoubleSliderFloor(1f);
        OnDoubleSliderPrice(1f);
    }

    private void CheckAllOffButtons()
    {
        if (_1 == -1 && _2 == -1 && _3 == -1 && _St == -1)
        {
            _St = 9;
            _1 = 1;
            _2 = 2;
            _3 = 3;
        }
    }

    private void CheckResetButtons()
    {
        if (_1 != -1 && _2 != -1 && _3 != -1 && _St != -1)
        {
            if (b_St.image.color == Color.white && b_1.image.color == Color.white && b_2.image.color == Color.white &&
                b_3.image.color == Color.white) return;
            _St = -1;
            _1 = -1;
            _2 = -1;
            _3 = -1;
        }
    }

}
