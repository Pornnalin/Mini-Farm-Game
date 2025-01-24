using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantData", menuName = "Planting/PlantData")]
public class PlantData : ScriptableObject
{
    [Header("Plant Data")]
    public string plantName;
    public Sprite seedSprite;
    public Sprite sproutSprite;
    public Sprite fullyGrownSprite;
    public Sprite itemHarvestSprite;
    public float growthDuration = 20f;
    public string requiredWeather;
    [Space]
    [Header("UI Data")]
    public Sprite seedBoxSprite;
    public string seedName;
    public string seedPodName;
    public string itemName;
    public int amount;
}

