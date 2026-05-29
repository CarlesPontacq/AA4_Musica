using UnityEngine;
using FMODUnity;

public class FMOD_DoorAudio : MonoBehaviour
{
    [Header("FMOD Events")]
    [SerializeField] private string openEvent = "event:/Doors/OpenDoor";
    [SerializeField] private string closeEvent = "event:/Doors/CloseDoor";

    public void PlayOpen(Vector3 position)
    {
        RuntimeManager.PlayOneShot(openEvent, position);
    }

    public void PlayClose(Vector3 position)
    {
        RuntimeManager.PlayOneShot(closeEvent, position);
    }
}