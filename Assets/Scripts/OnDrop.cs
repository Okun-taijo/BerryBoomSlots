using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private float _dropRadius = 160f;
    [SerializeField] private MinigameGrid _minigameGrid;
    private Animator _sceneAnimator;
    private BerryGrowing _berryGrowing;
    private string _currentSceneName;
    private BerryGrid _berryGrid;
    void Start()
    {
        
        _currentSceneName = SceneManager.GetActiveScene().name;
        _dropRadius = 200f;
        _berryGrid = GetComponentInParent<BerryGrid>();
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();
        BerryGrowing _berryObject=draggedTransform.GetComponent<BerryGrowing>();
        RectTransform dropTransform = GetComponent<RectTransform>();
        Vector2 dropCenter = dropTransform.position;
        Vector2 draggedPosition = draggedTransform.position;
        _berryGrowing = draggedTransform.GetComponent<BerryGrowing>();
        

        if (Vector2.Distance(draggedPosition, dropCenter) <= _dropRadius)
        {
            if (_currentSceneName == "MinigameScene")
            {
                if (dropTransform.childCount == 0)
                {
                    _minigameGrid.FillCell(dropTransform);
                }
                else
                {
                    GameObject.Destroy(dropTransform.GetChild(0).gameObject);
                    _minigameGrid.FillCell(dropTransform);
                    _berryGrowing.OnCoinTaken();
                }
                draggedTransform.SetParent(dropTransform);
                draggedTransform.anchoredPosition = Vector2.zero;
            }
            if (_currentSceneName == "StrawberryGame")
            {
                _sceneAnimator = draggedTransform.GetComponentInChildren<Animator>();
                if (dropTransform.childCount == 0)
                {
                    _berryGrid.FillCell(dropTransform);
                    draggedTransform.SetParent(dropTransform);
                    draggedTransform.anchoredPosition = Vector2.zero;
                }
                else
                {
                    if(draggedTransform.gameObject.name==dropTransform.GetChild(0).gameObject.name)
                    {
                        GameObject prefab = _berryObject._nextBerry;
                        _sceneAnimator.SetTrigger("Pop");
                        Destroy(draggedTransform.gameObject, 0.5f);
                        Destroy(dropTransform.GetChild(0).gameObject, 0.5f);
                        _berryGrid.FreeCell(dropTransform);
                        _berryGrid.SpawnSingleBerry(prefab);
                       
                    }
                    if(draggedTransform.gameObject.name != dropTransform.GetChild(0).gameObject.name)
                    {
                        return;
                    }
                }


            }
           

        }
       
    }
    
}
