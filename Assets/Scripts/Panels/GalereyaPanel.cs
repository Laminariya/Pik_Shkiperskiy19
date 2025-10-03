using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalereyaPanel : MonoBehaviour
{
    
    private GameManager _manager;
    
    public List<Sprite> lobbySprites = new List<Sprite>();
    public List<Sprite> otdelkaSprites = new List<Sprite>();
    public List<Sprite> intererSprites = new List<Sprite>();
    public List<Sprite> paradnieSprites = new List<Sprite>();
    public List<Sprite> blagoustrouystoSprites = new List<Sprite>();

    public Button b_Left;
    public Button b_Right;

    public Button b_Lobby;
    public Button b_Otdelka;
    public Button b_Inter;
    public Button b_Paradnie;
    public Button b_Blagoustrouysto;
    
    private List<Sprite> _sprites = new List<Sprite>();
    
    private int _spriteIndex = 0;
    private Image _image;
    
    
    public void Init()
    {
        _manager = GameManager.instance;
        _image = GetComponent<Image>();
        b_Left.onClick.AddListener(OnLeftClick);
        b_Right.onClick.AddListener(OnRightClick);
        b_Lobby.onClick.AddListener(OnLobbyClick);
        b_Otdelka.onClick.AddListener(OnOtdelkaClick);
        b_Inter.onClick.AddListener(OnInterClick);
        b_Paradnie.onClick.AddListener(OnParadnieClick);
        b_Blagoustrouysto.onClick.AddListener(OnBlagoustrouystoClick);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnLobbyClick();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnLeftClick()
    {
        _spriteIndex--;
        if(_spriteIndex < 0)
            _spriteIndex = 0;
        _image.sprite = _sprites[_spriteIndex];
    }

    private void OnRightClick()
    {
        _spriteIndex++;
        if(_spriteIndex >= _sprites.Count)
            _spriteIndex = _sprites.Count-1;
        _image.sprite = _sprites[_spriteIndex];
    }

    private void OnLobbyClick()
    {
        _sprites.Clear();
        _sprites = new List<Sprite>(lobbySprites);
        _image.sprite = _sprites[0];
    }
    
    private void OnOtdelkaClick()
    {
        _sprites.Clear();
        _sprites = new List<Sprite>(otdelkaSprites);
        _image.sprite = _sprites[0];
    }

    private void OnInterClick()
    {
        _sprites.Clear();
        _sprites = new List<Sprite>(intererSprites);
        _image.sprite = _sprites[0];
    }

    private void OnParadnieClick()
    {
        _sprites.Clear();
        _sprites = new List<Sprite>(paradnieSprites);
        _image.sprite = _sprites[0];
    }

    private void OnBlagoustrouystoClick()
    {
        _sprites.Clear();
        _sprites = new List<Sprite>(blagoustrouystoSprites);
        _image.sprite = _sprites[0];
    }
    
}
