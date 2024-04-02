using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    private bool _isDraggingNow = false;
    private Vector2 _offsetBorder;
    [SerializeField]private RectTransform _fieldRectTransform;

    private void Start()
    {
        _fieldRectTransform = transform.parent.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
                    {
                        _isDraggingNow = true;
                        _offsetBorder = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                    }
                    break;

                case TouchPhase.Moved:
                    if (_isDraggingNow)
                    {
                        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 newPosition = touchPosition + _offsetBorder;

                        // Ограничиваем новую позицию в пределах родительской панели
                        Vector2 clampedPosition = new Vector2(
                            Mathf.Clamp(newPosition.x, _fieldRectTransform.rect.xMin, _fieldRectTransform.rect.xMax),
                            Mathf.Clamp(newPosition.y, _fieldRectTransform.rect.yMin, _fieldRectTransform.rect.yMax)
                        );

                        transform.position = clampedPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    _isDraggingNow = false;
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, _fieldRectTransform.rect.xMin, _fieldRectTransform.rect.xMax),
            Mathf.Clamp(transform.position.y, _fieldRectTransform.rect.yMin, _fieldRectTransform.rect.yMax)
        );
        transform.position = clampedPosition;
    }
}

