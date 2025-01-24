using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WeatherSystem weatherSystem;
    public PlantManager plantManager;
    public Inventory inventory;
    public Notification notification;
    public SoundManager soundManager;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        weatherSystem = GetComponent<WeatherSystem>();
        plantManager = GetComponent<PlantManager>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        notification = GetComponent<Notification>();
        soundManager = GetComponent<SoundManager>();
    }

  
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartTimeworld());

    }
    IEnumerator StartTimeworld()
    {
        yield return new WaitForSeconds(1f);
        weatherSystem.UpdateWeatherTimer();
    }

  
}
