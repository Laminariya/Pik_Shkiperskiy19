using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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
    private Coroutine _coroutine;

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

        //Image.sprite = _myObject.FlatSprite;
        //TODO грузим спрайт этажа асинхронно
        RoomsArea.text = _myObject.CountRooms + " комнатная, " + _myObject.Area + " м" + _manager.SymvolQuadro;
        if(_myObject.CountRooms==0)
            RoomsArea.text = "Студия, " + _myObject.Area + " м" + _manager.SymvolQuadro;
        Price.text = _manager.GetSplitPrice(_myObject.Price) + " " + _manager.SymvolRuble;
        OldPrice.text = "";// "<s>123</s>";
        PricePerMeter.text = Mathf.RoundToInt(_myObject.Price / _myObject.Area).ToString() + " за м" + _manager.SymvolQuadro;
        Floor.text = _myObject.Floor + " из " + _myObject.CountFloor;
        Korpus.text = _myObject.Korpus.ToString();
        OnPlanerClick();
        Debug.Log(myObject.PathFurniture);
        Debug.Log(myObject.PathFloor);
        Debug.Log(myObject.UrlFurniture);
        Debug.Log(myObject.UrlFloor);
        if(_coroutine!=null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(LoadFloor());
    }

    public void Hide()
    {
        if(_coroutine!=null)
            StopCoroutine(_coroutine);
        if (_myObject != null && _myObject.FloorSprite!=null)
        {
            Destroy(_myObject.FloorSprite.texture);
            Destroy(_myObject.FloorSprite);
            _myObject.FloorSprite = null;
        }
        gameObject.SetActive(false);
    }

    private void OnPlanerClick()
    {
        b_Planer.enabled = false;
        b_Floor.enabled = true;
        ButtonPanel.sprite = PlanerActive;
        if (_myObject.FlatSprite != null)
            Image.sprite = _myObject.FlatSprite;
        else
        {
            Image.sprite = _spriteFloor;
        }
    }

    private void OnFloorClick()
    {
        b_Floor.enabled = false;
        b_Planer.enabled = true;
        ButtonPanel.sprite = FloorActive;
        if (_myObject.FloorSprite != null) Image.sprite = _myObject.FloorSprite;
        else Image.sprite = _spriteFloor;
    }

    private void OnBack()
    {
        Hide();
    }

    IEnumerator LoadFloor()
    {
        if (_myObject.UrlFloor == "") yield break;
        
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_myObject.UrlFloor))
        {
            yield return webRequest.SendWebRequest();

            Texture2D texture2D = null;
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D cashTexture2D = DownloadHandlerTexture.GetContent(webRequest);
                //texture2D = DownloadHandlerTexture.GetContent(webRequest);
                texture2D = new Texture2D(cashTexture2D.width, cashTexture2D.height, TextureFormat.RGBA4444, false);
                texture2D.SetPixels(cashTexture2D.GetPixels());
                texture2D.Apply();
                texture2D.Compress(true);

                Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f), 100.0f);
                _myObject.FloorSprite = _sprite;
                if (b_Planer.enabled) Image.sprite = _myObject.FloorSprite;
                Destroy(cashTexture2D);
            }
            else
            {
                Debug.LogError($"Ошибка загрузки: {webRequest.error}");
            }
                
        }
    }

}
