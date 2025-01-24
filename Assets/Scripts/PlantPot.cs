using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlantPot : MonoBehaviour
{
    [SerializeField] private float currentTime;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject plantItem;
    [SerializeField] private SpriteRenderer plantIm;
    [SerializeField] private PlantData plantData;
    [SerializeField] private float sppedUp = 10f;
    public bool isHavePlant = false;
    public bool isCanHarvest = false;
    public enum stagePlant
    {
        none,
        seed,
        sprout,
        fullyGrow
    }
    public stagePlant currentStagePlant;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHavePlant && countdownText != null)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0;
            }

            if (!isCanHarvest)
            {
                UpdateAnimation();
            }
            DisplayTime();
        }
    }
    void DisplayTime()
    {
        float sec = GameManager.instance.weatherSystem.ConvertToSeconds(currentTime);
        countdownText.text = sec.ToString();
    }
    public void SpawnPlant()
    {
        if (isHavePlant)
        {
            return;
        }


        plantData = GameManager.instance.plantManager.FindPlantData(PlayerController.instance.currentItem);

        if (plantData == null || string.IsNullOrEmpty(plantData.plantName))
        {
            Debug.LogWarning("Invalid plant data or plant name is missing!");
            return;
        }
        GameManager.instance.soundManager.PlayeClick();
        GameManager.instance.inventory.ModiflyItem(plantData);

        //Debug.Log(plantData.growthDuration);

        if (this.transform.childCount == 0)
        {
            // สร้างพืชใหม่
            plantItem = Instantiate(GameManager.instance.plantManager.prefabPlant);
            plantItem.transform.SetParent(this.transform, false);
            plantItem.transform.localPosition = Vector2.zero;


            plantIm = plantItem.transform.GetChild(0).GetComponent<SpriteRenderer>();
            TextMeshProUGUI text = plantItem.transform.Find("Canvas/CountDown").GetComponent<TextMeshProUGUI>();
            countdownText = text;

            if (plantData != null)
            {

                currentTime = plantData.growthDuration;
                plantIm.sprite = plantData.seedSprite;
                isHavePlant = true;
                //Debug.Log("Plant spawned with data: " + plantData.name);
            }
            else
            {
                //Debug.Log("No plant data found for the current seed.");
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);

            if (plantData != null)
            {
                currentTime = plantData.growthDuration;
                plantIm.sprite = plantData.seedSprite;
                isHavePlant = true;
                //Debug.Log("Plant spawned with data: " + plantData.name);
            }
            else
            {
                //Debug.Log("No plant data found for the current seed.");
            }
        }

    }
    public void HarvestItem()
    {
        if (!isCanHarvest) return;
        GameManager.instance.soundManager.PlayeClick();
        GameManager.instance.notification.notiEvent.Invoke(plantData.plantName);
        GameManager.instance.inventory.AddOrUpdateItem(plantData, true);
        //Debug.Log("Harvest" + plantItem.name);
        Reset();

    }
    public void SpeedUpPlant()
    {
        if (currentStagePlant == stagePlant.seed)
        {
            currentTime -= sppedUp;
            GameManager.instance.soundManager.PlayeSpeedUp();
        }

    }
    private void UpdateAnimation()
    {
        if (currentTime >= 11 && currentTime <= 20)
        {
            currentStagePlant = stagePlant.seed;

        }
        else if (currentTime >= 1 && currentTime <= 10)
        {
            currentStagePlant = stagePlant.sprout;
            plantIm.sprite = plantData.sproutSprite;
        }
        else if (currentTime <= 0)
        {
            plantIm.sprite = plantData.fullyGrownSprite;
            currentStagePlant = stagePlant.fullyGrow;
            if (plantIm.GetComponent<PolygonCollider2D>() == null)
            {
                plantIm.AddComponent<PolygonCollider2D>().isTrigger = true;
            }
            isCanHarvest = true;
        }

    }
    public void Reset()
    {
        plantData = null;
        currentTime = 0;
        isHavePlant = false;
        isCanHarvest = false;
        currentStagePlant = stagePlant.none;
        PolygonCollider2D collider = plantIm.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            Destroy(collider);
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
