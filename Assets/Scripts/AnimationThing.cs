using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationThing : MonoBehaviour
{
    [SerializeField]private CanvasGroup canvasGroup;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float loopSpeed = 1f;
    [SerializeField] private GameObject cloud;
    [SerializeField] private Transform startCloud;
    [SerializeField] private Transform endCloud;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 1;
        LeanTween.alphaCanvas(canvasGroup, 0, fadeSpeed).setEase(LeanTweenType.easeInOutSine)
           .setOnComplete(() =>
           {
               canvasGroup.interactable = false;
               canvasGroup.blocksRaycasts = false;
           });

        LoopCloud();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LoopCloud()
    {
        LeanTween.moveLocal(cloud, endCloud.localPosition, loopSpeed)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() =>
            {
                cloud.transform.localPosition = startCloud.localPosition;
                LoopCloud();
            });
    }

  
}
