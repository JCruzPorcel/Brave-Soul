using System.Collections;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime = 1f;

    public void FadeOut(float time)
    {
        fadeOutTime = time;
        StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        float currentTime = 0f;
        float originalVolume = audioSource.volume;

        while (currentTime < fadeOutTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(originalVolume, 0f, currentTime / fadeOutTime);
            yield return null;
        }
        audioSource.Stop();
    }
}
