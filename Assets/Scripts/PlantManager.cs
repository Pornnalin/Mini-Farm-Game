using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{    
    public GameObject prefabPlant;
    public List<PlantData> plantData;
    public List<Transform> plantTrans;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        plantTrans= new List<Transform>();
        GameObject[] transform = GameObject.FindGameObjectsWithTag("PlantPot");
        foreach (GameObject plantPot in transform)
        {
            plantTrans.Add(plantPot.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlantData FindPlantData(string seedPodName)
    {
        if(seedPodName =="") return null;
        for (int i = 0; i < plantData.Count; i++) 
        {
            if (plantData[i].seedPodName == seedPodName) 
            {
                return plantData[i]; 
            }
        }

        Debug.Log("Plant not found: " + seedPodName); 
        return null; 
    }
}
