using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float camMoveT = 0.008f;
    public Vector2 cameraOffset = Vector2.zero;

    public float zoomSpeed = 3;
    public float minZoom = 1;
    public float maxZoom = 5;
    public float zoomT = 5;
    public float zoom;
    // Start is called before the first frame update
    void Start()
    {
        zoom = Camera.main.orthographicSize; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = new Vector3(player.position.x + cameraOffset.x, player.position.y + cameraOffset.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, pos, camMoveT);

        

        zoom -= zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom, zoomT);
    }


}
