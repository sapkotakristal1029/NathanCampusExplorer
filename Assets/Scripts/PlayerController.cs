using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    [Header("Navigation Components")]
    public NavMeshAgent agent;
    public LineRenderer pathRenderer;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public Vector3 thirdPersonOffset = new Vector3(0, 180, -100);
    public Vector3 firstPersonOffset = new Vector3(0, 1.8f, 0.2f);

    [HideInInspector] public bool isThirdPerson = true;

    
    public System.Action onDestinationReached;
    private bool hasReached = false;
    private bool hasMoved = false;

    private AudioSource footstepAudio;


    // Initializes navigation agent and audio source
    void Start()
    {
        // Debug.Log(isNavigating);
        if (agent != null)
        {
            agent.speed = 100f;
            agent.acceleration = 40f;
            agent.angularSpeed = 50f;
        }

        footstepAudio = GetComponent<AudioSource>();
        if (footstepAudio != null)
        {
            footstepAudio.loop = true;
            footstepAudio.playOnAwake = false;
        }
    }

     // Updates camera position and handles footstep audio & path rendering
    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 offset = isThirdPerson ? thirdPersonOffset : firstPersonOffset;
            Vector3 desiredPosition = transform.position + offset;

            cameraTransform.position = desiredPosition;

            if (isThirdPerson)
            {
                cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
                
            }

            else
                cameraTransform.rotation = transform.rotation;
        }

        HandleFootstepAudio();
        DrawPath();
    }


    private void HandleFootstepAudio()
    {
        if (agent == null || footstepAudio == null) return;

        bool isWalking = agent.velocity.magnitude > 0.1f;

        if (isWalking && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
        else if (!isWalking && footstepAudio.isPlaying)
        {
            footstepAudio.Stop();

        }
    }

    void Update()
    {
        if (agent == null || !agent.enabled || agent.pathPending)
            return;

        // Check if agent started moving
        if (!hasMoved && agent.velocity.sqrMagnitude > 0.01f)
        {
            hasMoved = true;
        }

        // Only allow checking if movement started
        if (hasMoved && !hasReached &&
            agent.remainingDistance <= 1)
        {
            Debug.LogWarning("Panel is triggered");
            hasReached = true;
            onDestinationReached?.Invoke();
        }

    }


    // Sets a navigation route from a start to end point
    public void SetRoute(string startName, string endName)
    {
        hasReached = false;
        hasMoved = false;



        Transform start = GameObject.Find(startName)?.transform;
        Transform end = GameObject.Find(endName)?.transform;

        if (start != null && end != null)
        {
            agent.Warp(start.position);
            agent.SetDestination(end.position);
        }
        else
        {
            Debug.LogWarning("Start or end door not found. Check GameObject names.");
        }

    }

    // Updates the agent's destination mid-route
    public void SetNewDestination(string newTargetName)
    {
        Transform newTarget = GameObject.Find(newTargetName)?.transform;
        if (newTarget != null && agent != null)
        {
            agent.SetDestination(newTarget.position);
        }
    }

    // Switches between first-person and third-person view
    public void SetViewMode(bool thirdPerson)
    {
        isThirdPerson = thirdPerson;
    }

    // Updates the agent's movement speed
    public void SetAgentSpeed(float speed)
    {
        if (agent != null)
        {
            agent.speed = speed;
            // agent.angularSpeed = 50;
        }
    }


    // Mutes and unmutes footstep audio (used when paused)
    public void PauseFootsteps(bool paused)
    {
        if (footstepAudio == null) return;

        footstepAudio.mute = paused;
    }


    // Draws the navigation path using the LineRenderer
    void DrawPath()
    {
        if (agent == null || pathRenderer == null || agent.path == null)
            return;

        Vector3[] corners = agent.path.corners;
        pathRenderer.positionCount = corners.Length;
        pathRenderer.SetPositions(corners);
    }



}
