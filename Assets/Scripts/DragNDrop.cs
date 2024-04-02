using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]private MinigameGrid _minigameField;
    [SerializeField]private RectTransform _localRectRansform;
    private CanvasGroup _localCanvasGroup;
    private RectTransform _parentObjectRectTransform;
    
    public void Awake()
    {
        _minigameField = GetComponentInParent<MinigameGrid>();
        _localRectRansform=GetComponent<RectTransform>();
        _localCanvasGroup=GetComponent<CanvasGroup>();
        _parentObjectRectTransform = GetComponentInParent<RectTransform>();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
            _localCanvasGroup.blocksRaycasts = false;
            _localCanvasGroup.alpha = 0.7f;
        _minigameField.FreeCell(transform.parent);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsInsidePanel(eventData))
        {
            _localRectRansform.anchoredPosition += eventData.delta/_parentObjectRectTransform.localScale.x;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        _localCanvasGroup.blocksRaycasts = true;
        _localCanvasGroup.alpha = 1f;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Works");
    }
    private bool IsInsidePanel(PointerEventData eventData   )
    {
        Vector3[] corners = new Vector3[4];
        _parentObjectRectTransform.GetWorldCorners(corners);

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentObjectRectTransform, eventData.position, null, out localMousePosition);

        return RectTransformUtility.RectangleContainsScreenPoint(_parentObjectRectTransform, eventData.position);
    }

}
