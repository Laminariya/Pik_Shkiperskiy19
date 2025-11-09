using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System.Xml.Serialization;
using UnityEditor;

public class SerializeXML : MonoBehaviour
{

    private HttpClient Client = new HttpClient();
    private string _json;
    [HideInInspector] public FeedClass _feedClass;
    //public string _feedURL;
    
    //ссылка на json
    private string _url = "https://feed.storage-s3.ru/feed_flat_regions_new.xml";
    private GameManager _manager;
    
    
    public async Task Init(GameManager manager)
    {
        _manager = manager;
        await LoadJSON(_url);
    }

    private async Task LoadJSON(string url)
    {
        var uri = new Uri(url);

        var result = await Client.GetAsync(uri);
        string str = await result.Content.ReadAsStringAsync();
        //Debug.Log(str);
        XmlSerializer serializer = new XmlSerializer(typeof(FeedClass));
        
        using (StringReader reader = new StringReader(str))
        {
            Debug.Log("CC");
            try
            {
                Debug.Log("Try");
                _feedClass = (FeedClass)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }

            GameManager.instance.Feed = _feedClass;
            //Debug.Log("XX "+_feedClass.Objects.Count);
            // foreach (var obj in _feedClass.Objects)
            // {
            //     Debug.Log(obj.Id);
            // }
            reader.Close();
        }

        _manager.InfoStartPanel.text += "\r Load Feed Complete";
        result.Dispose();
        serializer = null;

        // foreach (var objectClass in _feedClass.Objects)
        // {
        //     if (objectClass.BuildingSection == "Корпус 1" && objectClass.ApartmentNumber == 167)
        //     {
        //         Debug.Log(objectClass.Name);
        //     }
        // }
        
    }

}


    [XmlRoot("realty-feed"), Serializable]
    public class FeedClass
    {
        [XmlElement("generation-date")] 
        public string CreateDate;
        
        [XmlElement("offer")] 
        public List<ObjectClass> Objects = new List<ObjectClass>();
    }

    [Serializable]
    public class ObjectClass
    {
        [XmlAttribute("internal-id")] public string Id;

        [XmlElement("type")] public string Type;

        [XmlElement("property-type")] public string PropertyType;

        [XmlElement("category")] public string Category;
        
        [XmlElement("url")] public string Url;
        
        [XmlElement("blockUrl")] public string BlockUrl;

        [XmlElement("creation-date")] public string CreationDate;
        
        [XmlElement("location")] public Locations Location;
        
        [XmlElement("image_floor_plan")] public string ImageFloorPlan;
        
        [XmlElement("svg_floor_plan")] public string SvgFloorPlan;
        
        [XmlElement("sales-agent")] public SalesAgent SalesAgent;
        
        [XmlElement("benefits")] public Benefits Benefits;
        
        [XmlElement("apartment-number")] public int ApartmentNumber;
        
        [XmlElement("rooms")] public int Rooms;
        
        [XmlElement("name")] public string Name;
        
        [XmlElement("floor")] public int floor;
        
        [XmlElement("floors-total")] public int FloorsTotal;
        
        [XmlElement("description")] public string Description;
        
        [XmlElement("area")] public Area Area;
        
        //[XmlElement("SeparateWcsCount" , IsNullable = true)] public int? SeparateWcsCount;
        
        [XmlElement("mortgage")] public int Mortgage;
        
        [XmlElement("building-name")] public string BuildingName;
        
        [XmlElement("building-section")] public string BuildingSection;
        
        [XmlElement("building-type")] public string BuildingType;
        
        [XmlElement("building-phase")] public string BuildingPhase;
        
        [XmlElement("built-year")] public string BuiltYear;
        
        [XmlElement("ready-quarter")] public int ReadyQuarter;
        
        [XmlElement("building-state")] public string BuildingState;
        [XmlElement("keys")] public string Keys;
        [XmlElement("lift")] public int Lift;
        [XmlElement("span")] public string Span;
        [XmlElement("windowView")] public string WindowView;
        [XmlElement("image2D")] public string Image2D;
        [XmlElement("image2DSvg")] public string Image2DSvg;
        [XmlElement("image3D")] public string Image3D;
        
        [XmlElement("images")] public Images Images;
        
        [XmlElement("studio")] public string Studio;
        [XmlElement("status")] public int Status;
        [XmlElement("parkingType")] public string ParkingType;
        [XmlElement("kitchen")] public string Kitchen;
        [XmlElement("livingArea")] public float LivingArea;
        [XmlElement("kitchenArea")] public float KitchenArea;
        [XmlElement("renovation")] public string Renovation;
        [XmlElement("ceilingHeight")] public float ceilingHeight;
        [XmlElement("options")] public Options Options; //???
        [XmlElement("gp")] public string Gp;
        [XmlElement("latitude")] public string Latitude;
        [XmlElement("longitude")] public string longitude;
        
        [XmlElement("metro")] public Metro Metro;
    }
    
    [Serializable]
    public class SalesAgent
    {
        [XmlElement("phone")] public string Phone;
        [XmlElement("category")] public string Category;
        [XmlElement("organization")] public string Organization;
        [XmlElement("url")] public string Url;
        [XmlElement("email")] public string Email;
        [XmlElement("photo")] public string Photo;
    }
    
    // [Serializable]
    // public class Coordinates
    // {
    //     [XmlElement("Lat")] public string Lat;
    //     [XmlElement("Lng")] public string Lng;
    // }
    
    [Serializable]
    public class Benefits
    {
        [XmlElement("mortgage")]
        public Mortgage Mortgage;
    }

    [Serializable]
    public class Mortgage
    {
        [XmlElement("benefit")]
        public List<Benefit> Benefits = new List<Benefit>();
    }
    
    [Serializable]
    public class Benefit
    {
        [XmlElement("value")] public int Value;
        [XmlElement("currency")] public string Currency;
        [XmlElement("name")] public string Name;
        [XmlElement("isMain")] public string isMain;
        [XmlElement("minMonthFee")] public string MinMonthFee;
    }
    
    // [Serializable]
    // public class Phones
    // {
    //     [XmlElement("PhoneSchema")] public List<PhoneSchema> PhoneSchema = new List<PhoneSchema>();
    // }
    
    [Serializable]
    public class PhoneSchema
    {
        [XmlElement("CountryCode")] public string CountryCode;
        [XmlElement("Number")] public string Number;
    }
    
    [Serializable]
    public class Locations
    {
        [XmlElement("country")] public string Country;
        [XmlElement("region")] public string Region;
        [XmlElement("locality-name")] public string LocalityName;
        [XmlElement("address")] public string Address;
        [XmlElement("korpus")] public string Korpus;
        [XmlElement("section")] public string Section;
    }

    // [Serializable]
    // public class Developer
    // {
    //     [XmlElement("Name")] public string Name;
    // }
    
    [Serializable]
    public class Area
    {
        [XmlElement("value")] public float Value;
        [XmlElement("unit")] public string Unit;
    }
    
    [Serializable]
    public class Images
    {
        [XmlElement("image1")] public string Image1;
        [XmlElement("image2")] public string Image2;
        [XmlElement("image3")] public string Image3;
        [XmlElement("image4")] public string Image4;
        [XmlElement("image5")] public string Image5;
        [XmlElement("image6")] public string Image6;
        [XmlElement("image7")] public string Image7;
        [XmlElement("image8")] public string Image8;
        [XmlElement("image9")] public string Image9;
        [XmlElement("image10")] public string Image10;
    }
    
    // [Serializable]
    // public class RoomDefinitions
    // {
    //     [XmlElement("Room")] public List<Room> Room;
    // }
    
    [Serializable]
    public class Room
    {
        [XmlElement("Area")] public float Area;
    }
    
    // [Serializable]
    // public class Building
    // {
    //     [XmlElement("FloorsCount")] public int FloorsCount;
    //     [XmlElement("CeilingHeight")] public float CeilingHeight;
    //     [XmlElement("PassengerLiftsCount")] public int PassengerLiftsCount;
    //     [XmlElement("CargoLiftsCount")] public int CargoLiftsCount;
    //     [XmlElement("Parking")] public Parking Parking;
    //     [XmlElement("Deadline")] public Deadline Deadline;
    //     [XmlElement("MaterialType")] public string MaterialType;
    // }
    
    [Serializable]
    public class Parking
    {
        [XmlElement("Type")] public string Type;
    }
    
    [Serializable]
    public class Deadline
    {
        [XmlElement("Quarter")] public string Quarter;
        [XmlElement("Year")] public int Year;
        [XmlElement("IsComplete")] public bool IsComplete;
    }

    // [Serializable]
    // public class BargainTerms
    // {
    //     [XmlElement("Price")] public int Price;
    //     [XmlElement("Currency")] public string Currency;
    //     [XmlElement("SaleType")] public string SaleType;
    //     [XmlElement("MortgageAllowed")] public bool MortgageAllowed;
    //     [XmlElement("ActionId")] public int ActionId;
    // }
    
    // [Serializable]
    // public class LayoutPhoto
    // {
    //     [XmlElement("FullUrl")] public string FullUrl;
    //     [XmlElement("IsDefault")] public bool IsDefault;
    // }
    
    // [Serializable]
    // public class Photos
    // {
    //     [XmlElement("PhotoSchema")] public List<PhotoSchema> PhotoSchema = new List<PhotoSchema>();
    // }
    
    [Serializable]
    public class PhotoSchema
    {
        [XmlElement("FullUrl")] public string FullUrl;
        [XmlElement("IsDefault")] public bool IsDefault;
    }

    [Serializable]
    public class Metro
    {
        [XmlElement("name")] public string Name;
        [XmlElement("time-on-transport")] public string TimeOnTransport; //???
        [XmlElement("time-on-foot")] public string TimeOnFoot;
    }

    [Serializable]
    public class Options
    {
        [XmlElement("aluminumProfile")] public string AluminumProfile;
        [XmlElement("livingKitchen")] public string LivingKitchen; 
        [XmlElement("highCeiling")] public string HighCeiling;
        [XmlElement("vidVoDvor")] public string VidVoDvor;
    }

    

