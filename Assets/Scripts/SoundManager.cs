using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip speedUpSound;
    [SerializeField] private AudioClip getSound;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource audioSourceBG;
    [SerializeField] private AudioSource audioSourceSFX;
    [SerializeField] private AudioSource audioSourceHitSFX;
    // Start is called before the first frame update

    public void Start()
    {
        StartCoroutine(StartMusic());
    }
    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(1f);
        PlayeMusicBG();
    }
    public void PlayeMusicBG()
    {
        audioSourceBG.clip = bgSound;
        audioSourceBG.Play();
        audioSourceBG.loop = true;
    }
    public void PlayeClick()
    {
        audioSourceSFX.clip = clickSound;
        audioSourceSFX.Play();
    }
    public void PlayeSpeedUp()
    {
        audioSourceSFX.clip = speedUpSound;
        audioSourceSFX.Play();
    }
    public void PlayeGetItem()
    {
        audioSourceSFX.clip = getSound;
        audioSourceSFX.Play();
    }
    public void PlayeFail()
    {
        audioSourceSFX.clip = failSound;
        audioSourceSFX.Play();
    }
    public void PlayeHit()
    {
        audioSourceHitSFX.clip = hitSound;
        audioSourceHitSFX.Play();
    }
}
