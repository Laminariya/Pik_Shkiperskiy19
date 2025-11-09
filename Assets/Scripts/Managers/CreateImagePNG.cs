using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Image = UnityEngine.UI.Image;

public class CreateImagePNG : MonoBehaviour
{

    public enum TypePlane
    {
        floor, plane, furniture
    }
    
    public Image ImageTest;
    public string PlaneFloor = "//Plans//PlanFloors//";
    public string PlaneApartment = "//Plans//PlanApartments//";
    public string PlaneApartmentFurn = "//Plans//PlanApartmentFurns//";

    private GameManager _manager;
    private List<Sprite> _sprites = new List<Sprite>();
    private int _spriteIndex = 0;


    public IEnumerator Init()
    {
        Debug.Log("Create ImagePNG");
        _manager = GameManager.instance;
        //StartCoroutine(LoadFileFromUrl(_domen+_path));
        //StartCoroutine(LoadPNG(_nameFile));
        if(!Directory.Exists(Directory.GetCurrentDirectory()+PlaneFloor))
            CreateFolder(Directory.GetCurrentDirectory()+PlaneFloor);
        if(!Directory.Exists(Directory.GetCurrentDirectory()+PlaneApartment))
            CreateFolder(Directory.GetCurrentDirectory()+PlaneApartment);
        if(!Directory.Exists(Directory.GetCurrentDirectory()+PlaneApartmentFurn))
            CreateFolder(Directory.GetCurrentDirectory()+PlaneApartmentFurn);
        
        //yield return StartCoroutine(CheckPlane());
        yield return null;
    }

    private void CreateFolder(string url)
    {
        Directory.CreateDirectory(url);
    }

    //Проверка на наличие схем и изменение
    IEnumerator CheckPlane()
    {
        Debug.Log("Checking plane");
        
        int count = 0;
        foreach (var builder in _manager.MyData.Buildings)
        {
            count+=builder.MyObjects.Count;
        }

        int count2 = 0;
        foreach (var builder in _manager.MyData.Buildings)
        {
            //Debug.Log(builder.Name);
            foreach (var myObject in builder.MyObjects)
            {
                count2++;
                _manager.InfoStartPanel.text = count2.ToString()+"/"+count.ToString();
                
                //Зашружаем схемы с мебелью
                if (!File.Exists(myObject.PathFurniture) &&
                    myObject.UrlFurniture != "")
                    yield return StartCoroutine(LoadFileFromUrl(myObject.UrlFurniture,
                        myObject.PathFurniture));
                if (!File.Exists(myObject.PathFloor) &&
                    myObject.UrlFloor != "")
                    yield return StartCoroutine(LoadFileFromUrl(myObject.UrlFloor,
                        myObject.PathFloor));
                yield return null;
            }
        }
        
        Debug.Log("Checking plane END");
    }

    


    //Загрузили с сервера картинку
    IEnumerator LoadFileFromUrl(string url, string pathPlane)
    {
        Debug.Log("LoadFileFromUrl");
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        Debug.Log("Изображение загружено");
        yield return StartCoroutine(CreatePNG(pathPlane, www.downloadHandler.data));
    }

    //Сохранили картинку у нас
    public IEnumerator CreatePNG(string path, byte[] data)
    {
        // запись в файл
        using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
        {
            // запись массива байтов в файл
            yield return fstream.WriteAsync(data, 0, data.Length);
            Debug.Log("Картинка создана записан в файл");
        }
    }

    public IEnumerator LoadPNG(string urlPlane, Image image)
    {
        string url = "file://" + urlPlane;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            //Debug.Log(www.texture);
            Texture2D texture2D = www.texture;
            Sprite sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            //_sprites.Add(sprite);
            if (image != null)
                image.sprite = sprite;
        }
    }

    public IEnumerator LoadSpritePNG(string pathPlane, Sprite sprite)
    {
        string url = "file://" + pathPlane;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            sprite = _sprite;
            sprite.name = pathPlane;
        }
    }
    
    public IEnumerator LoadSpritePNG(MyObject myObject)
    {
        string url = "file://" + myObject.PathFurniture;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            texture2D.Compress(false);
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myObject.FlatSprite = _sprite;
            _sprites.Add(_sprite);
        }
        
        url = "file://" + myObject.PathFloor;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            texture2D.Compress(false);
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myObject.FloorSprite = _sprite;
            _sprites.Add(_sprite);
        }
    }
    
    //Загрузили с сервера картинку
    public IEnumerator LoadSpriteFromUrl(MyObject myObject)
    {
        //UnityWebRequest www = UnityWebRequest.Get(myObject.UrlFurniture);
        //yield return www.SendWebRequest();
        if (myObject.UrlFurniture != "")
        {
            using (WWW www = new WWW(myObject.UrlFurniture))
            {
                yield return www;
                Texture2D texture2D = null;
                try
                {
                    texture2D = www.texture;
                    texture2D.Compress(false);
                    Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height),
                        new Vector2(0.5f, 0.5f), 100.0f);
                    myObject.FlatSprite = _sprite;
                    _sprites.Add(_sprite);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
        
        //UnityWebRequest www2 = UnityWebRequest.Get(myObject.UrlFloor);
        //yield return www2.SendWebRequest();

        if (myObject.UrlFloor == "") yield break;
        using (WWW www = new WWW(myObject.UrlFloor))
        {
            yield return www;
            Texture2D texture2D = null;
            try
            {
                texture2D = www.texture;
                texture2D.Compress(false);
                Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                myObject.FloorSprite = _sprite;
                _sprites.Add(_sprite);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spriteIndex++;
            ImageTest.sprite = _sprites[_spriteIndex];
        }
    }
}
