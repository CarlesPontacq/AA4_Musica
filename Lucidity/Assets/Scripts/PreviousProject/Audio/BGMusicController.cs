using System.Collections;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource bass;
    [SerializeField] private AudioSource instruments;
    [SerializeField] private AudioSource melody;

    [Header("Zones")]
    [SerializeField] private MusicZoneConfig mainRoom;
    [SerializeField] private MusicZoneConfig hallway;

    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine fadeRoutine;

    public void ApplyMainRoom()
    {
        ApplyConfig(mainRoom);
    }

    public void ApplyHallway()
    {
        ApplyConfig(hallway);
    }

    private void ApplyConfig(MusicZoneConfig config)
    {
        if (config == null) return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTo(config));
    }

    private IEnumerator FadeTo(MusicZoneConfig target)
    {
        float t = 0f;

        float bStart = bass.volume;
        float iStart = instruments.volume;
        float mStart = melody.volume;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float k = t / fadeDuration;

            bass.volume = Mathf.Lerp(bStart, target.bassVolume, k);
            instruments.volume = Mathf.Lerp(iStart, target.instrumentsVolume, k);
            melody.volume = Mathf.Lerp(mStart, target.melodyVolume, k);

            yield return null;
        }

        bass.volume = target.bassVolume;
        instruments.volume = target.instrumentsVolume;
        melody.volume = target.melodyVolume;
    }
}