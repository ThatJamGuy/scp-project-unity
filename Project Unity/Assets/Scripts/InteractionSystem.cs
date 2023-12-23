using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Tag of the target object")]
    public string targetTag = "Item";
    [Tooltip("Radius for interaction with the target object")]
    public int interactRadius;

    [Header("UI Settings")]
    [Tooltip("Canvas for displaying the interactDisplay image")]
    public Canvas canvas;
    [Tooltip("Image shown when within interaction radius of the target")]
    public Image interactDisplay;

    [Header("Interactable Object")]
    [Tooltip("Current interactable object")]
    public GameObject currentInteractible;

    private void Update()
    {
        // Find all objects with the specified tag
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject targetObject in targetObjects)
        {
            // Calculate the distance between this object and the target object
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);

            if (distance <= interactRadius)
            {
                Debug.Log("Within detection radius of " + targetObject.name);
                currentInteractible = targetObject;

                // Convert world position of currentInteractible to screen position
                Vector2 screenPosition = Camera.main.WorldToScreenPoint(currentInteractible.transform.position);

                // Convert screen position to canvas space
                Vector2 canvasPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPosition, null, out canvasPosition);

                // Assign the calculated position to the interactDisplay
                interactDisplay.rectTransform.anchoredPosition = canvasPosition;

                interactDisplay.gameObject.SetActive(true);

                if(Input.GetMouseButton(0))
                    currentInteractible.GetComponent<DoorButton>().UseButton();
            }
            else
            {
                currentInteractible = null;
                interactDisplay.gameObject.SetActive(false);
            }
        }
    }
}