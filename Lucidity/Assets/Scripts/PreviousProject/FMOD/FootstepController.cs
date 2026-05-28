using FMODUnity;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class FootstepController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioResource walkingTileSound;
    [SerializeField] private AudioResource runningTileSound;
    [SerializeField] private AudioResource walkingWoodSound;
    [SerializeField] private AudioResource runningWoodSound;
    [SerializeField] private PlayerInputObserver inputObserver;
    [SerializeField] private Rigidbody playerRef;

    private const float MinVelocity = 0.1f;

    private float MaterialValue;
    private bool isRunning;
    public float distance = 2f;
    public float volume = 2f;
    public LayerMask lm;

    [SerializeField] PlayerMovement playerMovement;

    private RaycastHit rh;

    public Transform feet;

    Vector3 pos;
    GameObject go;

    private void Start()
    {
    }

    private void Update()
    {

    }

    void PlayFootstepSound()
    {
        if (!IsMoving())
        {
            StopFootsteps();
            return;
        }

        MaterialCheck();
        RunCheck();
        audioSource.Play();
    }

    private bool IsMoving()
    {
        Vector3 velocity = playerRef.linearVelocity;
        velocity.y = 0f;
        return velocity.magnitude > MinVelocity;
    }

    void MaterialCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rh, distance, lm))
        {
            switch (rh.collider.tag)
            {
                case "Tile":
                    MaterialValue = 1;
                    break;
                case "Wood":
                default:
                    MaterialValue = 0;
                    break;
            }
        }
        else
        {
            MaterialValue = 0;
        }
    }

    void RunCheck()
    {
        isRunning = inputObserver.IsPressingRun;


        if (!isRunning)
        {
            switch (MaterialValue)
            {
                case 1:
                    audioSource.resource = walkingTileSound;
                    break;
                case 0:
                default:
                    audioSource.resource = walkingWoodSound;
                    break;
            }
        }
        else
        {
            switch (MaterialValue)
            {
                case 1:
                    audioSource.resource = runningTileSound;
                    break;
                case 0:
                default:
                    audioSource.resource = runningWoodSound;
                    break;
            }
        }
    }

    private void StopFootsteps()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.resource = null;
        }
    }
}