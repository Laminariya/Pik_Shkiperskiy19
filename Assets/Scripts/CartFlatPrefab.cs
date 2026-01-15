using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartFlatPrefab : MonoBehaviour
{
    
    public Image Image;
    public TMP_Text RoomsArea;
    public TMP_Text Price;
    public TMP_Text OldPrice;
    public TMP_Text PricePerMeter;
    public TMP_Text KorpusFloor;

    private MyObject _myObject;
    private Button _button;
    
    public void Init(MyObject myObject)
    {
        _myObject = myObject;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        Image.sprite = _myObject.FlatSprite;
        RoomsArea.text = _myObject.CountRooms + " комнатная, " + _myObject.Area + " м" + GameManager.instance.SymvolQuadro;
        if(_myObject.CountRooms==0)
            RoomsArea.text = "Студия, " + _myObject.Area + " м" + GameManager.instance.SymvolQuadro;
        Price.text = GameManager.instance.GetSplitPrice(_myObject.Price) + " " + GameManager.instance.SymvolRuble;
        OldPrice.text = ""; //"<s>123</s>";
        PricePerMeter.text = Mathf.RoundToInt(_myObject.Price / _myObject.Area) + " за м" + GameManager.instance.SymvolQuadro;
        KorpusFloor.text = "Корпус " + _myObject.Korpus + ", Секция " + _myObject.Section + ", Этаж " +
                           _myObject.Floor + " из " + _myObject.CountFloor;
    }

    private void OnClick()
    {
        GameManager.instance.flatPanel.Show(_myObject);
    }
    
    public void OnSendMessageOnComPort()
    {
        GameManager.instance.MessageOnFlat(_myObject.SendHouse,_myObject.SendPorch,_myObject.Number);
    }

}