using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlatPanel : MonoBehaviour
{

    public Image Image;
    public Image ButtonPanel;
    public Button b_Planer;
    public Button b_Floor;
    public Sprite PlanerActive;
    public Sprite FloorActive;

    public TMP_Text RoomsArea;
    public TMP_Text Price;
    public TMP_Text OldPrice;
    public TMP_Text PricePerMeter;
    public TMP_Text Floor;
    public TMP_Text Korpus;
    public Button b_Back;

    private MyObject _myObject;
    private Sprite _spriteFloor;
    private GameManager _manager;
    
    public void Init()
    {
        _manager = GameManager.instance;
        b_Floor.onClick.AddListener(OnFloorClick);
        b_Planer.onClick.AddListener(OnPlanerClick);
        b_Back.onClick.AddListener(OnBack);
        Hide();
    }

    public void Show(MyObject myObject)
    {
        _myObject = myObject;
        gameObject.SetActive(true);

        Image.sprite = _myObject.PlanSprite;
        //TODO грузим спрайт этажа асинхронно
        RoomsArea.text = _myObject.CountRooms + " комнатная, " + _myObject.Area + " м" + _manager.SymvolQuadro;
        Price.text = _manager.GetSplitPrice(_myObject.Price) + " " + _manager.SymvolRuble;
        OldPrice.text = "<s>123</s>";
        PricePerMeter.text = Mathf.RoundToInt(_myObject.Price / _myObject.Area).ToString() + " за м" + _manager.SymvolQuadro;
        Floor.text = _myObject.Floor + " из " + _myObject.CountFloor;
        Korpus.text = _myObject.Korpus.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnPlanerClick()
    {
        b_Planer.enabled = false;
        b_Floor.enabled = true;
        ButtonPanel.sprite = PlanerActive;
        Image.sprite = _myObject.PlanSprite;
    }

    private void OnFloorClick()
    {
        b_Floor.enabled = false;
        b_Planer.enabled = true;
        ButtonPanel.sprite = FloorActive;
        Image.sprite = _spriteFloor;
    }

    private void OnBack()
    {
        Hide();
    }

}
