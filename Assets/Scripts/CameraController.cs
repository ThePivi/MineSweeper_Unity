using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    private float screenWidth, screenHeight;
    public float zoom = 1f;
    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenHeight = Screen.height-1;
        screenWidth = Screen.width-1;
        mainCamera.transform.position = CalcualateOrthoSize().center;
        mainCamera.orthographicSize = CalcualateOrthoSize().size;
    }

    private (Vector3 center, float size) CalcualateOrthoSize() {
        var bounds = new Bounds();
        
        foreach (var col in FindObjectsOfType<Collider2D>()) bounds.Encapsulate(col.bounds);
        
        bounds.Expand(0f);
        
        var horizontal = bounds.size.x * Camera.main.pixelHeight / Camera.main.pixelWidth;
        var vertical = bounds.size.y;

        var size = (Mathf.Max(horizontal, vertical) * 0.5f)/zoom;
        var center = bounds.center + new Vector3(0, 0, -10);
        center.x = center.x - (screenWidth/zoom - GetMouseCoordinates().x)/(horizontal/zoom);
        center.y = center.y - (screenHeight/zoom - GetMouseCoordinates().y)/(vertical/zoom);

        return (center, size);
    }

    private Vector3 GetMouseCoordinates() {
        Vector3 mouseCoordinates = Input.mousePosition;
        mouseCoordinates.z = -10;
        return mouseCoordinates;
    }
}
