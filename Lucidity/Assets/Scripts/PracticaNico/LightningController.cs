using FMOD.Studio;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("Lightning")]

    [SerializeField] private GameObject[] lightnings;

    [Range(1f, 2f)]
    [SerializeField] private float firstLightnintTimer = 1.7f;

    public float lightningMinDuration = 0.125f;
    public float lightningMaxDuration = 0.75f;

    public float nextLightningMinWaiting = 3.5f;
    public float nextLightningMaxWaiting = 7.7f;

    [Header("Thunder - FMOD")]
    [FMODUnity.EventRef]
    [SerializeField]
    private string thunderEventPath;

    [SerializeField] private float thunderMinDelay = 0.5f;
    [SerializeField] private float thunderMaxDelay = 3.5f;

    private FMOD.Studio.EventInstance thunderEventInstance;

    void Start()
    {
        foreach (var l in lightnings)
            l.SetActive(false);

        Invoke(nameof(CallLightning), firstLightnintTimer);
    }

    void CallLightning()
    {
        int randomLightning = Random.Range(0, lightnings.Length);
        float lightningDuration = Random.Range(lightningMinDuration, lightningMaxDuration);

        lightnings[randomLightning].SetActive(true);
        Invoke(nameof(EndLightning), lightningDuration);

        CallThunderWithDelay();

        float rand = Random.Range(nextLightningMinWaiting, nextLightningMaxWaiting);
        Invoke(nameof(CallLightning), rand);
    }

    void EndLightning()
    {
        foreach (var l in lightnings)
            l.SetActive(false);
    }

    private void CallThunderWithDelay()
    {
        if (SFXManager.Instance == null) return;

        float delay = Random.Range(thunderMinDelay, thunderMaxDelay);
        Invoke(nameof(PlayThunder), delay);
    }

    private void PlayThunder()
    {
        thunderEventInstance = FMODUnity.RuntimeManager.CreateInstance(thunderEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(thunderEventInstance, transform);
        thunderEventInstance.start();
        thunderEventInstance.release();
    }

    private void OnDestroy()
    {
        if (thunderEventInstance.isValid())
        {
            thunderEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            thunderEventInstance.release();
        }
    }
}
