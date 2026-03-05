using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject labelText; // assign the Text child of this button

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (labelText != null)
            labelText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (labelText != null)
            labelText.SetActive(false);
    }
}