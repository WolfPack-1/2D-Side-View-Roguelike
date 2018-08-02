using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUIController : MonoBehaviour
{

    [SerializeField] Transform iconTransform;
    [SerializeField] Camera playerCamera;
    [SerializeField] Canvas canvas;
    
    public void SetIcon(Vector2 worldPosition)
    {
        iconTransform.gameObject.SetActive(true);
        worldPosition.y += 0.5f;
        Vector3 screenPoint = playerCamera.WorldToScreenPoint(worldPosition);
        Vector2 resultPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPoint, canvas.worldCamera, out resultPoint);
        iconTransform.localPosition = resultPoint;
    }

    public void DisableIcon()
    {
        iconTransform.gameObject.SetActive(false);
    }
    
}
