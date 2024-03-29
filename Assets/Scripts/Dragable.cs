using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    private bool _isDragging = false;
    private Vector2 _offset;
    [SerializeField]private RectTransform _panelRect;

    private void Start()
    {
        _panelRect = transform.parent.parent.GetComponent<RectTransform>();
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
                        _isDragging = true;
                        _offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                    }
                    break;

                case TouchPhase.Moved:
                    if (_isDragging)
                    {
                        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 newPosition = touchPosition + _offset;

                        // ќграничиваем новую позицию в пределах родительской панели
                        Vector2 clampedPosition = new Vector2(
                            Mathf.Clamp(newPosition.x, _panelRect.rect.xMin, _panelRect.rect.xMax),
                            Mathf.Clamp(newPosition.y, _panelRect.rect.yMin, _panelRect.rect.yMax)
                        );

                        transform.position = clampedPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    _isDragging = false;
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        // ѕровер€ем, находитс€ ли объект за пределами панели, и возвращаем его в пределы панели
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, _panelRect.rect.xMin, _panelRect.rect.xMax),
            Mathf.Clamp(transform.position.y, _panelRect.rect.yMin, _panelRect.rect.yMax)
        );
        transform.position = clampedPosition;
    }
}

