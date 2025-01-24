using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static WeatherSystem;

public class WeatherSystem : MonoBehaviour
{
    public enum weatherType { summer, winter };
    public weatherType currentWeather;
    private float timeValue = 30f;
    private float currentTime;
    private UnityEvent nextWeatherEvent;
    [SerializeField]private TextMeshProUGUI timeText;
    [SerializeField]private TextMeshProUGUI currentWeatherText;
    [SerializeField]private SpriteRenderer filter;
    // Start is called before the first frame update
    void Start()
    {
        currentWeather = weatherType.summer;
        currentTime = timeValue;
        if (nextWeatherEvent == null)
            nextWeatherEvent = new UnityEvent();
        nextWeatherEvent.AddListener(ChangeWeather);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateWeatherTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0)
        {
            currentTime = timeValue;
            nextWeatherEvent.Invoke();
        }

        DisplayeTime(currentTime);
        FadeFilter();
    }

    void ChangeWeather()
    {
        int currentIndex = (int)currentWeather;

        currentIndex = (currentIndex + 1) % System.Enum.GetValues(typeof(weatherType)).Length;

        currentWeather = (weatherType)currentIndex;

        if (PlayerController.instance.currentItem != "")
        {
            PlayerController.instance.currentItem = "";
            GameManager.instance.notification.notiEvent.Invoke("Clear Seed in hand ");
        }

        Debug.Log("Weather changed to: " + currentWeather);
    }
    void DisplayeTime(float timeToDisplay)
    {
        timeText.text = string.Format("{00}", ConvertToSeconds(currentTime));
        currentWeatherText.text = currentWeather.ToString();
    }
    public float ConvertToSeconds(float timeToDisplay)
    {
        return Mathf.FloorToInt(timeToDisplay % 60);
    }
    private void FadeFilter()
    {
        float targetAlpha = (currentWeather == weatherType.winter) ? 0.3137255f : 0f;

        LeanTween.value(gameObject, filter.color.a, targetAlpha, 1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnUpdate((float newAlpha) =>
            {
                Color color = filter.color;
                color.a = newAlpha;
                filter.color = color;
            });
    }
}
