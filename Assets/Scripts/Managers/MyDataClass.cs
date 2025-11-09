using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyDataClass : MonoBehaviour
{

    private GameManager _manager;
    private MyData _myData;
    private string _nameJK = "Шкиперский 19";

    public void Init()
    {
        Debug.Log("Create My Data");
        _manager = GameManager.instance;
        CreateData();
    }

    private void CreateData()
    {
        _myData = new MyData();
        _myData.Buildings = new List<MyBuilding>();
        List<string> nameHouse = new List<string>();
        foreach (var objectClass in _manager.Feed.Objects) 
        {
            if (objectClass.BuildingName==_nameJK && !nameHouse.Contains(objectClass.BuildingSection))
            {
                //Debug.Log(objectClass.BuildingSection);
                nameHouse.Add(objectClass.BuildingSection);
            }
        }

        foreach (var house in nameHouse)
        {
            Debug.Log(house);
            MyBuilding building = new MyBuilding();
           
            foreach (var objectClass in _manager.Feed.Objects)
            {
                if (objectClass.BuildingName==_nameJK && objectClass.BuildingSection==house)
                {
                    MyObject myObject = new MyObject(objectClass);
                    building.MyObjects.Add(myObject);
                }
            }
          
            building.BuildingName = building.MyObjects[0].BuioldingName;
            building.BuildingSection = building.MyObjects[0].BuioldingSection;
            string[] split = building.MyObjects[0].BuioldingSection.Split(" ");
            building.Korpus = int.Parse(split[1]);
            _myData.Buildings.Add(building);
        }

        
        foreach (var building in _myData.Buildings)
        {
            int maxPrice = 0;
            int minPrice = int.MaxValue;
            float maxArea = 0;
            float minArea = float.MaxValue;
            int maxFloor = 0;
            int minFloor = int.MaxValue;
            foreach (var myObject in building.MyObjects)
            {
                if (myObject.Price > maxPrice) maxPrice = myObject.Price;
                if (myObject.Price < minPrice) minPrice = myObject.Price;
                
                if (myObject.Area > maxArea) maxArea = myObject.Area;
                if (myObject.Area < minArea) minArea = myObject.Area;
                
                if (myObject.Floor > maxFloor) maxFloor = myObject.Floor;
                if (myObject.Floor < minFloor) minFloor = myObject.Floor;
            }
        
            building.MaxPrice = maxPrice;
            building.MinPrice = minPrice;
            building.MaxArea = maxArea;
            building.MinArea = minArea;
            building.MaxFloor = maxFloor;
            building.MinFloor = minFloor;
        }
       
        _manager.MyData = _myData;
    }

    public IEnumerator CreateSprites()
    {
        int count = 0;
        foreach (var building in _myData.Buildings) 
        {
            foreach (var myObject in building.MyObjects)
            {
                count++;
            }
        }
        string str = _manager.InfoStartPanel.text;
        int count2 = 0;
        foreach (var building in _myData.Buildings)
        {
            foreach (var myObject in building.MyObjects)
            {
                yield return StartCoroutine(_manager.createImagePng.LoadSpriteFromUrl(myObject));
                count2++;
                _manager.InfoStartPanel.text = str + "\r\n" + "Load Image: " +count2 + "/" + count;
            }
        }
    }

}

[Serializable]
public class MyData
{
    public List<MyBuilding> Buildings = new List<MyBuilding>();
}

[Serializable]
public class MyBuilding
{
    public List<MyObject> MyObjects = new List<MyObject>();
    public int Korpus;
    public int Section;
    public float MinArea;
    public float MaxArea;
    public int MinPrice;
    public int MaxPrice;
    public int MinFloor;
    public int MaxFloor;
    public string BuildingName;
    public string BuildingSection;
}

[Serializable]
public class MyObject
{
    //public MyBuilding Building;
    public ObjectClass ObjectClass;
    public int CountRooms;
    public float Area;
    public int Korpus;
    public int Section;
    public int Floor;
    public int CountFloor;
    public float CeilingHeight; //Находиться в Билдинге
    public int Number;
    public int Price;
    public string UrlFurniture;
    public string UrlFloor;
    public string PathFurniture;
    public string PathFloor;
    public bool IsFree;
    public Sprite FlatSprite;
    public Sprite FloorSprite;
    public string Decoration;
    public int NumberOnFloor;
    public int Status;
    public string BuioldingName;
    public string BuioldingSection;

    public MyObject(ObjectClass objectClass)
    {
        ObjectClass = objectClass;
        CountRooms = ObjectClass.Rooms;
        if(objectClass.Studio=="true")
            CountRooms = 0;
        Area = ObjectClass.Area.Value;
        string[] split = ObjectClass.BuildingSection.Split(" ");
        Korpus = int.Parse(split[1]);
        BuioldingName = ObjectClass.BuildingName;
        Floor = ObjectClass.floor;
        CountFloor = ObjectClass.FloorsTotal;
        CeilingHeight = ObjectClass.ceilingHeight;
        Number = ObjectClass.ApartmentNumber;
        Price = ObjectClass.Benefits.Mortgage.Benefits[0].Value;
        UrlFurniture = ObjectClass.Image2D;
        UrlFloor = ObjectClass.ImageFloorPlan;
        Status = ObjectClass.Status;
        Section = 1;
        BuioldingSection = ObjectClass.BuildingSection;
        // if (ObjectClass.Decoration == "without")
        //     Decoration = "Без отделки";
        // else if (ObjectClass.Decoration == "preFine")
        // {
        //     Decoration = "Whitebox";
        // }
        // else
        // {
        //     Decoration = "";
        // }

        string id = Korpus + "_" + Number;
        PathFurniture = Directory.GetCurrentDirectory() + "//Plans//PlanApartmentFurns//" + id + ".png";
        PathFloor = Directory.GetCurrentDirectory() + "//Plans//PlanFloors//" + id + ".png";
    }

}


