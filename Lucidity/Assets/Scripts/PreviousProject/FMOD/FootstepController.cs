using FMODUnity;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [Header("FMOD Event")]
    [FMODUnity.EventRef]
    [SerializeField] private string footstepEventPath = "event:/Footsteps"; // Ajusta la ruta según tu proyecto

    [Header("References")]
    [SerializeField] private PlayerInputObserver inputObserver;
    [SerializeField] private Rigidbody playerRef;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform feet;

    [Header("Raycast Settings")]
    public float distance = 2f;
    public LayerMask lm;

    private FMOD.Studio.EventInstance footstepInstance;
    private const float MinVelocity = 0.1f;
    private bool isPlaying = false;

    // Valores para los parámetros labeled
    private enum SurfaceType
    {
        Wood = 0,
        Tile = 1
    }

    private enum PlayerSpeed
    {
        Walk = 0,
        Run = 1
    }

    private SurfaceType currentSurface = SurfaceType.Wood;
    private PlayerSpeed currentSpeed = PlayerSpeed.Walk;

    private void Start()
    {
        footstepInstance = RuntimeManager.CreateInstance(footstepEventPath);
        RuntimeManager.AttachInstanceToGameObject(footstepInstance, feet != null ? feet : transform);
    }

    private void Update()
    {
        MaterialCheck();
        RunCheck();

        UpdateFMODParameters();
    }

    void PlayFootstepSound()
    {
        if (!IsMoving()) return;

        footstepInstance.start();
        Debug.Log("Surface: " + currentSurface.ToString() + " - Speed: " + currentSpeed.ToString());

        isPlaying = true;
    }

    private bool IsMoving()
    {
        Vector3 velocity = playerRef.linearVelocity;
        velocity.y = 0f;
        return velocity.magnitude > MinVelocity;
    }

    void MaterialCheck()
    {
        RaycastHit rh;
        if (Physics.Raycast(transform.position, Vector3.down, out rh, distance, lm))
        {
            switch (rh.collider.tag)
            {
                case "Tile":
                    currentSurface = SurfaceType.Tile;
                    break;
                case "Wood":
                    currentSurface = SurfaceType.Wood;
                    break;
                default:
                    currentSurface = SurfaceType.Wood;
                    break;
            }
        }
        else
        {
            currentSurface = SurfaceType.Wood;
        }
    }

    void RunCheck()
    {
        isPlaying = inputObserver.IsPressingRun;

        if (!isPlaying)
        {
            currentSpeed = PlayerSpeed.Walk;
        }
        else
        {
            currentSpeed = PlayerSpeed.Run;
        }
    }

    private void UpdateFMODParameters()
    {
        footstepInstance.setParameterByName("SurfaceType", (float)currentSurface);
        footstepInstance.setParameterByName("PlayerSpeed", (float)currentSpeed);
    }

    private void StopFootsteps()
    {
        if (footstepInstance.isValid())
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isPlaying = false;
        }
    }

    private void OnDestroy()
    {
        if (footstepInstance.isValid())
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            footstepInstance.release();
        }
    }
}