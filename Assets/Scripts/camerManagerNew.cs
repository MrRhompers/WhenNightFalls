using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cameraManagerNew : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform cameraTransform;
    public Transform mainCameraPoint;
    public Transform[] otherCameraPoints;

    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("UI Buttons")]
    public CanvasGroup mainButtonsGroup; // assign CanvasGroup on MainButtons
    public CanvasGroup backButtonGroup;  // assign CanvasGroup on BackButton

    private Transform currentPoint;

    void Start()
    {
        MoveCameraInstant(mainCameraPoint);

        // Initial button setup
        mainButtonsGroup.alpha = 1f;
        mainButtonsGroup.interactable = true;
        mainButtonsGroup.blocksRaycasts = true;

        backButtonGroup.alpha = 0f;
        backButtonGroup.interactable = false;
        backButtonGroup.blocksRaycasts = false;
    }

    public void MoveToCameraPoint(int pointIndex)
    {
        if (pointIndex < 0 || pointIndex >= otherCameraPoints.Length) return;

        StartCoroutine(FadeMove(otherCameraPoints[pointIndex], showMainButtons: false));
    }

    public void MoveToMainCamera()
    {
        StartCoroutine(FadeMove(mainCameraPoint, showMainButtons: true));
    }

    IEnumerator FadeMove(Transform newPoint, bool showMainButtons)
    {
        if (currentPoint == newPoint) yield break;

        float t = 0f;

        // Fade out black panel
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float fade = t / fadeDuration;

            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, fade);

            // Fade buttons along with camera
            if (mainButtonsGroup != null)
            {
                float target = showMainButtons ? 1 - fade : fade;
                mainButtonsGroup.alpha = target;
            }

            if (backButtonGroup != null)
            {
                float target = showMainButtons ? fade : 1 - fade;
                backButtonGroup.alpha = target;
            }

            yield return null;
        }

        // Move camera
        MoveCameraInstant(newPoint);

        // Fade in black panel
        t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float fade = t / fadeDuration;

            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, fade);

            if (mainButtonsGroup != null)
            {
                float target = showMainButtons ? 1 - fade : fade;
                mainButtonsGroup.alpha = target;
            }

            if (backButtonGroup != null)
            {
                float target = showMainButtons ? fade : 1 - fade;
                backButtonGroup.alpha = target;
            }

            yield return null;
        }

        // Final setup
        if (mainButtonsGroup != null)
        {
            mainButtonsGroup.alpha = showMainButtons ? 1f : 0f;
            mainButtonsGroup.interactable = showMainButtons;
            mainButtonsGroup.blocksRaycasts = showMainButtons;
        }

        if (backButtonGroup != null)
        {
            backButtonGroup.alpha = showMainButtons ? 0f : 1f;
            backButtonGroup.interactable = !showMainButtons;
            backButtonGroup.blocksRaycasts = !showMainButtons;
        }
    }

    void MoveCameraInstant(Transform newPoint)
    {
        if (newPoint == null || cameraTransform == null) return;

        cameraTransform.position = newPoint.position;
        cameraTransform.rotation = newPoint.rotation;

        currentPoint = newPoint;
    }
}