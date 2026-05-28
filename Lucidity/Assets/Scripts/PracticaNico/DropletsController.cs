using FMOD.Studio;
using UnityEngine;

public class DropletsController : MonoBehaviour
{
    [Header("Droplet")]
    [Range(1f, 2f)]
    [SerializeField] private float firstDroplettTimer = 1.7f;

    public float nextDropletMinWaiting = 1f;
    public float nextDropletMaxWaiting = 3.5f;

    [Header("Droplet - FMOD")]
    [FMODUnity.EventRef]
    [SerializeField]
    private string dropletEventPath;

    [SerializeField] private float dropletMinDelay = 0.1f;
    [SerializeField] private float dropletMaxDelay = 1f;

    private FMOD.Studio.EventInstance dropletEventInstance;

    void Start()
    {
        Invoke(nameof(CallDroplet), firstDroplettTimer);
    }

    void CallDroplet()
    {
        CallThunderWithDelay();

        float rand = Random.Range(nextDropletMinWaiting, nextDropletMaxWaiting);
        Invoke(nameof(CallDroplet), rand);
    }

    private void CallThunderWithDelay()
    {
        if (SFXManager.Instance == null) return;

        float delay = Random.Range(dropletMinDelay, dropletMaxDelay);
        Invoke(nameof(PlayThunder), delay);
    }

    private void PlayThunder()
    {
        dropletEventInstance = FMODUnity.RuntimeManager.CreateInstance(dropletEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(dropletEventInstance, transform);
        dropletEventInstance.start();
        dropletEventInstance.release();
    }

    private void OnDestroy()
    {
        if (dropletEventInstance.isValid())
        {
            dropletEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            dropletEventInstance.release();
        }
    }
}
