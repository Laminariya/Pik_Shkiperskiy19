using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    [HideInInspector] public SerializeXML serializeXML;
    
    [HideInInspector] public FeedClass Feed;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        LoadJSON();
    }
    
    private async Task LoadJSON()
    {
        serializeXML = FindObjectOfType<SerializeXML>(true);
        await serializeXML.Init();

        //StartCoroutine(StartGame());
    }

    
}
