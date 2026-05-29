using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class BGMusicController : MonoBehaviour
{
    [Header("FMOD Event")]
    [SerializeField] private string musicEvent = "event:/Music/LevelMusic";

    private EventInstance musicInstance;

    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    public void ApplyMainRoom()
    {
        SetLocation(0f);
    }

    public void ApplyHallway()
    {
        SetLocation(1f);
    }

    public void SetLocation(float value)
    {
        musicInstance.setParameterByName("Location", value);
    }

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}