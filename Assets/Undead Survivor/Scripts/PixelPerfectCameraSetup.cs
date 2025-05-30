using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineCamera))]
public class PixelPerfectCameraSetup : MonoBehaviour
{
    [Header("Pixel Perfect Settings")]
    public float pixelsPerUnit = 18f;
    public bool snapToPixelGrid = true;

    private CinemachineCamera cinemachineCamera;
    private Camera mainCamera;
    private float UnitsPerPixel => 1f / pixelsPerUnit;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        mainCamera = Camera.main;

        if (mainCamera == null)
            mainCamera = FindFirstObjectByType<Camera>();
    }

    void Start()
    {
        SetupPixelPerfectCamera();
    }

    void LateUpdate()
    {
        if (snapToPixelGrid)
        {
            SnapCameraToPixelGrid();
        }
    }

    void SetupPixelPerfectCamera()
    {
        if (cinemachineCamera != null)
        {
            // Set up the camera for pixel perfect rendering
            var brain = cinemachineCamera.gameObject.GetComponent<CinemachineBrain>();
            if (brain == null)
            {
                brain = cinemachineCamera.gameObject.AddComponent<CinemachineBrain>();
            }

            // Disable default blend for pixel perfect movement
            brain.DefaultBlend.Style = CinemachineBlendDefinition.Styles.Cut;

            // Configure the camera lens for pixel perfect
            if (mainCamera != null)
            {
                mainCamera.orthographic = true;

                // Calculate orthographic size based on screen resolution and pixels per unit
                float screenHeight = Screen.height;
                float targetOrthoSize = (screenHeight / 2f) / pixelsPerUnit;
                cinemachineCamera.Lens.OrthographicSize = targetOrthoSize;
            }
        }
    }

    void SnapCameraToPixelGrid()
    {
        if (mainCamera != null)
        {
            Vector3 cameraPos = mainCamera.transform.position;

            // Snap camera position to pixel grid
            cameraPos.x = Mathf.Round(cameraPos.x / UnitsPerPixel) * UnitsPerPixel;
            cameraPos.y = Mathf.Round(cameraPos.y / UnitsPerPixel) * UnitsPerPixel;

            mainCamera.transform.position = cameraPos;
        }
    }

    // Call this when screen resolution changes
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            SetupPixelPerfectCamera();
        }
    }
}