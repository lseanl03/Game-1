using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public bool followPlayer = true;
    public float zoom = 5f;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        Zoom();
        FollowPlayer();
    }
    void Zoom()
    {
        if (cam.orthographic)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoom;
        }
        else
        {
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom;
        }
    }
    void FollowPlayer()
    {
        if (followPlayer)
        {
            transform.position = new Vector3(player.position.x, player.position.y + 6, transform.position.z);
        }

    }
}
