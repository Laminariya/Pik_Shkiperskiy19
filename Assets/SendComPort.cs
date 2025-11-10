using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class SendComPort : MonoBehaviour
{

    public TMP_Text InfoData;
   
    
    private Queue<byte> _queue = new Queue<byte>();
    
    private bool _isSend;
    private string _read;
    private Coroutine _coroutine1;
    private Coroutine _coroutine2;
    private Coroutine _coroutine3;
    private Coroutine _coroutine4;
    private Coroutine _coroutine5;

    public Color StartColor1;
    public Color EndColor1;
    public Color StartColor2;
    public Color EndColor2;
    public Color StartColor3;
    public Color EndColor3;
    public Color StartColor4;
    public Color EndColor4;
    public Color StartColor5;
    public Color EndColor5;

    public Color PressButtonColor;
    public Color _startColorButton;

    public GameObject StartPanel;
    public Button StartButton;
    public TMP_Dropdown Dropdown;

    private int _valueLed;
    private BluetoothManager _bluetooth;

    public void Init()
    {
        _bluetooth = FindObjectOfType<BluetoothManager>(true);
        StartPanel.SetActive(true); //TODO Немного меняем стартовую панель на сцене!!!
        StartButton.onClick.AddListener(StartProject);
        
        _valueLed = 120;
        //CreatPort();
        //CheckPorts();
        _isSend = true;
        
        
        //OffAll();
      
        //MySendMessage(191);
    }

    private void StartProject() //Запускаем по кнопке!!!
    {
        StartPanel.SetActive(false);
        OffAll();
    }

    public void OffAll()
    {
        MySendMessage(160);
    }

    private void OnDestroy()
    {
        
    }

    private async Task MySendMessage(int number)
    {
        _isSend = false;
        //Debug.Log(number);
        InfoData.text = "X ";
        try
        {
            //byte[] data = new byte[0];
            //data[0] = str;
            InfoData.text += number + " ";
            byte[] data = BitConverter.GetBytes(number);
            Debug.Log(data.Length + " " + data[0]);
            string n = number.ToString("X");
            //TODO Заменить на отправку через Блютуз!!!
            InfoData.text += n;
            _bluetooth.WriteData(n); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
        
        _isSend = true;
        //mySerialPort.DiscardInBuffer();
        //mySerialPort.DiscardOutBuffer();
    }

    public void AddMessage(byte message)
    {
        _queue.Enqueue(message);
        Debug.Log(_queue.Count + " "  + _isSend);
    }

    

    IEnumerator OnLed(Image image, Color startColor, Color endColor)
    {
        image.color = endColor;
        bool isUp = false;
        float time = 0;
        while (true)
        {
            if (isUp)
            {
                time += Time.deltaTime;
                image.color = Color.LerpUnclamped(image.color, endColor, Time.deltaTime);
                if (time>=1.5f)
                {
                    isUp = false;
                    time = 0;
                }
            }
            else
            {
                time += Time.deltaTime;
                image.color = Color.Lerp(image.color, startColor, Time.deltaTime);
                if (time>=1.5f)
                {
                    time = 0;
                    isUp = true;
                }
            }
            yield return null;
        }
    }

}
