using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public Button b_Galereya;
    public Button b_Osobennosty;
    public Button b_ChoseFlat;

    public Button b_Demo;
    public Button b_Light;

    public Sprite LightOff;
    public Sprite LightOn;
    public Sprite DemoOff;
    public Sprite DemoOn;
    
    public void Init()
    {
        _manager = GameManager.instance;
        b_Galereya.onClick.AddListener(OnGalereyaClick);
        b_Osobennosty.onClick.AddListener(OnOsobennostyClick);
        b_Demo.onClick.AddListener(OnDemoClick);
        b_Light.onClick.AddListener(OnLightClick);
        b_ChoseFlat.onClick.AddListener(OnChoseFlatClick);
    }

    private void OnGalereyaClick()
    {
        _manager.galereyaPanel.Show();
    }

    private void OnOsobennostyClick()
    {
        _manager.osobennostyPanel.Show();
    }

    private void OnDemoClick()
    {
        if (b_Demo.image.sprite == DemoOn)
        {
            //Выключаем демо
            b_Demo.image.sprite = DemoOn;
        }
        else
        {
            b_Demo.image.sprite = DemoOff;
            //Включаем демо
        }
    }

    public void OnLightClick()
    {
        if (b_Light.image.sprite == LightOn)
        {
            //Выключаем подсветку
            b_Light.image.sprite = LightOn;
        }
        else
        {
            b_Light.image.sprite = LightOff;
            //Включаем подсветку
        }
    }

    private void OnChoseFlatClick()
    {
        _manager.choseOnParameterPanel.Show();
    }

}
