using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Notification : MonoBehaviour
{
    [SerializeField] private GameObject prefabNoti;

    [HideInInspector] public UnityEvent<string> notiEvent = new UnityEvent<string>();

    void Start()
    {
        notiEvent.AddListener(ShowNoti);
    }

    private void ShowNoti(string message)
    {
        StartCoroutine(WaitAndDisplay(message));
    }

    IEnumerator WaitAndDisplay(string message)
    {
        LeanTween.cancel(gameObject);
        GameObject go = Instantiate(prefabNoti);
        go.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(go, new Vector3(1, 1, 1), .7f).setEase(LeanTweenType.easeOutElastic);
        go.transform.SetParent(GameObject.Find("NotiContent").transform, false);      
        go.transform.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YAY!! " + message;

        yield return new WaitForSeconds(3f);

        Destroy(go);
    }
}
