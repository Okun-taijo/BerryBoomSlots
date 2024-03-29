using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]private MinigameGrid _grid;
    [SerializeField]private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private RectTransform _parentRectTransform;
    
    public void Awake()
    {
        _grid = GetComponentInParent<MinigameGrid>();
        _rectTransform=GetComponent<RectTransform>();
        _canvasGroup=GetComponent<CanvasGroup>();
        _parentRectTransform = GetComponentInParent<RectTransform>();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.7f;
        _grid.RemoveOccupiedCell(transform.parent);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsInsidePanel(eventData))
        {
            _rectTransform.anchoredPosition += eventData.delta/_parentRectTransform.localScale.x;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
       
        // throw new System.NotImplementedException();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Works");
    }
    private bool IsInsidePanel(PointerEventData eventData   )
    {
        Vector3[] corners = new Vector3[4];
        _parentRectTransform.GetWorldCorners(corners);

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, eventData.position, null, out localMousePosition);

        return RectTransformUtility.RectangleContainsScreenPoint(_parentRectTransform, eventData.position);
    }

}
