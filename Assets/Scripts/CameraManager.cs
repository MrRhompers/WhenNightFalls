using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera[] otherCameras;

    private Camera currentCamera;

    void Start()
    {
        SwitchCamera(mainCamera);
    }

    public void SwitchToCamera(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= otherCameras.Length)
            return;

        SwitchCamera(otherCameras[cameraIndex]);
    }

    public void SwitchToMainCamera()
    {
        SwitchCamera(mainCamera);
    }

    void SwitchCamera(Camera newCam)
    {
        if (currentCamera == newCam) return;

        mainCamera.enabled = false;

        foreach (Camera cam in otherCameras)
            cam.enabled = false;

        newCam.enabled = true;
        currentCamera = newCam;
    }
}