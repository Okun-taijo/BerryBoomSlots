using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BerryGrid : MonoBehaviour
{
    [SerializeField] private GameObject _berryPrefab;
    [SerializeField] private Transform _playableGrid;
    [SerializeField] private float _minimumSpawnTime = 5f;
    [SerializeField] private float _maximumSpawnTime = 15f;
    public List<Transform> EmptyCells = new List<Transform>();
    private Transform _activeStrawberry;
    public List<Transform> FilledCells = new List<Transform>();

    private void Start()
    {
        FillEmptyCellsList();
        StartCoroutine(SpawnStrawberry());
        StartCoroutine(SpawnCoin());
    }
    private void Update()
    {
        
    }
    private void FillEmptyCellsList()
    {
        foreach (Transform cell in _playableGrid)
        {
            EmptyCells.Add(cell);
        }
    }

    private IEnumerator SpawnStrawberry()
    {
        
        while (true)
        {
            if (_activeStrawberry == null)
            {
                yield return new WaitForSeconds(Random.Range(_minimumSpawnTime, _maximumSpawnTime));
                if (FilledCells.Count < EmptyCells.Count)
                {
                    Transform randomCell = GetEmptyCell();
                    _activeStrawberry = Instantiate(_berryPrefab, randomCell.position, Quaternion.identity, randomCell).transform;
                    // Сделаем клубнику дочерним объектом ячейки
                    _activeStrawberry.SetParent(randomCell);
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minimumSpawnTime, _maximumSpawnTime));

            if (EmptyCells.Count > 0)
            {
                Transform randomCell = GetEmptyCell();
                FilledCells.Add(randomCell);
                Transform coin = Instantiate(_berryPrefab, randomCell.position, Quaternion.identity, randomCell).transform;
                // Сделаем монетку дочерним объектом ячейки
                coin.transform.SetParent(randomCell);
            }
        }
    }

    private Transform GetEmptyCell()
    {
        int randomIndex = Random.Range(0, EmptyCells.Count);
        Transform randomCell = EmptyCells[randomIndex];
        EmptyCells.RemoveAt(randomIndex);
        return randomCell;
    }

    public void PlaceStrawberry(Transform strawberryTransform, Transform cellTransform)
    {
        if (EmptyCells.Contains(cellTransform))
        {
            strawberryTransform.position = cellTransform.position;
            EmptyCells.Remove(cellTransform);
        }
    }

    public void FreeCell(Transform cell)
    {
        //availableCells.Add(cell);
        for (int i = 0; i <FilledCells.Count; i++)
        {
            if (FilledCells[i].childCount==0) 
            {
                EmptyCells.Add(FilledCells[i]);
                FilledCells.RemoveAt(i);
            }
        }
    }
    public void FillCell(Transform cell)
    {
        EmptyCells.Remove(cell);
    }

    public void SpawnSingleBerry(GameObject gameObject)
    {
        if (EmptyCells.Count > 0)
        {
            Transform randomCell = GetEmptyCell();
            FilledCells.Add(randomCell);
            Transform coin = Instantiate(gameObject, randomCell.position, Quaternion.identity, randomCell).transform;
            // Сделаем монетку дочерним объектом ячейки
            coin.transform.SetParent(randomCell);
        }
    }
}