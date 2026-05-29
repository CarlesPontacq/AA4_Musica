using UnityEngine;

public class AudioZone : MonoBehaviour
{
    public enum ZoneType
    {
        MainRoom,
        Hallway
    }

    [SerializeField] private ZoneType zone;
    [SerializeField] private BGMusicController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (zone)
        {
            case ZoneType.MainRoom:
                controller.ApplyMainRoom();
                break;

            case ZoneType.Hallway:
                controller.ApplyHallway();
                break;
        }
    }
}