using UnityEngine;

public class FMOD_3D_SFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath;
    FMOD.Studio.EventInstance eventInstance;
    public Transform attachObject;

    void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(eventInstance, attachObject);
        eventInstance.start();
        eventInstance.release();
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
