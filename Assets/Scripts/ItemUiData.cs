using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiData : MonoBehaviour
{
    public int currentAmount = 10;
    //public string seedName;
    //public string seedPodName;
    public string resourceName;
    public string weather;
    public Image itemIm;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI nameText;
    public void Start()
    {

    }
    private void Update()
    {
        amountText.text = currentAmount.ToString();
        nameText.text = resourceName.ToString();
    }
 
    public void SendItemName()
    {
        PlayerController.instance.currentItem = this.resourceName;
    }
    public void ClickSound()
    {
        GameManager.instance.soundManager.PlayeClick();
    }
}
