using UnityEngine;

public class CameraWASD : MonoBehaviour
{
    public float moveSpeed = 20f;         // Speed of movement
    public float rotationSpeed = 3f;      // Sensitivity for mouse look

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // WASD movement
        float h = Input.GetAxis("Horizontal"); // A/D
        float v = Input.GetAxis("Vertical");   // W/S
        transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime, Space.Self);

        // Right-click + drag to rotate the camera
        if (Input.GetMouseButton(1))
        {
            yaw += rotationSpeed * Input.GetAxis("Mouse X");
            pitch -= rotationSpeed * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -80f, 80f); // Prevent flipping

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
