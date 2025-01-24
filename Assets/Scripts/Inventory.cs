using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemUiData> itemDatas;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform content;
    // Start is called before the first frame update

    void Start()
    {
        InitItems();
    }
    private void Update()
    {
        OnOffItemBtn();
    }
    void InitItems()
    {
        for (int i = 0; i < GameManager.instance.plantManager.plantData.Count; i++)
        {
            PlantData plantData = GameManager.instance.plantManager.plantData[i];
            GameObject go = Instantiate(itemPrefab);
            go.name = plantData.seedPodName;
            go.transform.SetParent(content);
            ItemUiData itemUiData = go.GetComponent<ItemUiData>();
            itemUiData.currentAmount = plantData.amount;
            itemUiData.weather = plantData.requiredWeather;
            itemUiData.resourceName = plantData.seedPodName;
            itemUiData.itemIm.sprite = plantData.seedBoxSprite;
            itemDatas.Add(itemUiData);
        }
    }
    public void AddOrUpdateItem(PlantData plantData, bool isProduct)
    {
        //update
        for (int i = 0; i < itemDatas.Count; i++)
        {
            if ((isProduct && itemDatas[i].resourceName == plantData.itemName) ||
                (!isProduct && itemDatas[i].resourceName == plantData.seedPodName))
            {
                itemDatas[i].currentAmount++;
                Debug.Log("Add item: " + (isProduct ? plantData.itemName : plantData.seedPodName));
                return;
            }
        }

        //add
        GameObject go = Instantiate(itemPrefab);
        go.transform.SetParent(content);
        ItemUiData itemUiData = go.GetComponent<ItemUiData>();
        go.name = isProduct ? plantData.itemName : plantData.seedPodName;
        itemUiData.currentAmount = 1;
        itemUiData.weather = isProduct? "": plantData.requiredWeather;
        itemUiData.resourceName = isProduct ? plantData.itemName : plantData.seedPodName;
        itemUiData.itemIm.sprite = isProduct ? plantData.itemHarvestSprite : plantData.seedBoxSprite;


        itemDatas.Add(itemUiData);
        Debug.Log("Add new item: " + (isProduct ? plantData.itemName : plantData.seedPodName));

    }
    public void ModiflyItem(PlantData plantData)
    {

        ItemUiData foundItem = itemDatas.Find(item => item.resourceName == plantData.seedPodName);

        foundItem.currentAmount--;
        if (foundItem.currentAmount == 0)
        {
            itemDatas.Remove(foundItem);
            PlayerController.instance.currentItem = "";
            Destroy(foundItem.gameObject);
        }

        Debug.Log(foundItem);
    }
    public void OnOffItemBtn()
    {
        string currentWeather = GameManager.instance.weatherSystem.currentWeather.ToString().ToLower();

        foreach (ItemUiData item in itemDatas)
        {
            Button btn = item.GetComponent<Button>();
            if (item.weather == currentWeather)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
        }

    }

}

