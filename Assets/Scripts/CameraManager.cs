using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera[] otherCameras;

    private Camera currentCamera;

    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        SwitchCameraInstant(mainCamera);
    }

    public void SwitchToCamera(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= otherCameras.Length)
            return;

        StartCoroutine(FadeSwitch(otherCameras[cameraIndex]));
    }

    public void SwitchToMainCamera()
    {
        StartCoroutine(FadeSwitch(mainCamera));
    }

    IEnumerator FadeSwitch(Camera newCam)
    {
        if (currentCamera == newCam) yield break;

        float t = 0;

        // Fade Out
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }

        SwitchCameraInstant(newCam);

        // Fade In
        t = fadeDuration;

        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
    }

    void SwitchCameraInstant(Camera newCam)
    {
        mainCamera.enabled = false;

        foreach (Camera cam in otherCameras)
            cam.enabled = false;

        newCam.enabled = true;
        currentCamera = newCam;
    }
}