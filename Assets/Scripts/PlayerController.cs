using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonAutoNavigator : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cameraTransform;

    public Vector3 thirdPersonOffset = new Vector3(0, 150, -10);
    public Vector3 firstPersonOffset = new Vector3(0, 1.8f, 0.2f);

    [HideInInspector] public bool isThirdPerson = true;

    void Start()
    {
        if (agent != null)
        {
            agent.speed = 100f;
            agent.acceleration = 40f;
            agent.angularSpeed = 10f;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 offset = isThirdPerson ? thirdPersonOffset : firstPersonOffset;
            Vector3 desiredPosition = transform.position + offset;

            cameraTransform.position = desiredPosition;

            if (isThirdPerson)
                cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
            else
                cameraTransform.rotation = transform.rotation;
        }
    }

    public void SetRoute(string startName, string endName)
    {
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

    public void SetNewDestination(string newTargetName)
    {
        Transform newTarget = GameObject.Find(newTargetName)?.transform;
        if (newTarget != null && agent != null)
        {
            agent.SetDestination(newTarget.position);
        }
    }

    //  NEW: Set View Mode from UI Toggle
    public void SetViewMode(bool thirdPerson)
    {
        isThirdPerson = thirdPerson;
    }
    public void SetAgentSpeed(float speed)
    {
        if (agent != null)
        {
            agent.speed = speed;
        }
    }

}
