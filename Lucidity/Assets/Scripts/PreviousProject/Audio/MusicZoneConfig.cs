using UnityEngine;

[System.Serializable]
public class MusicZoneConfig
{
    [Range(0f, 1f)] public float bassVolume = 1f;
    [Range(0f, 1f)] public float instrumentsVolume = 1f;
    [Range(0f, 1f)] public float melodyVolume = 1f;
}