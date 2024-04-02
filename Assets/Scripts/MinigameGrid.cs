using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameGrid : MonoBehaviour
{
    [SerializeField] private GameObject _berryPrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _playableGrid;
    [SerializeField] private float _minimumSpawnTime = 5f;
    [SerializeField] private float _maximumSpawnTime = 15f;
    [SerializeField]private List<Transform> EmptyCells = new List<Transform>();
    private Transform _activeStrawberry;
    [SerializeField]private List<Transform> FilledCells = new List<Transform>();

    private void Start()
    {
        FillEmptyCells();
        StartCoroutine(SpawnStrawberry());
        StartCoroutine(SpawnCoin());
    }

    private void FillEmptyCells()
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
                GameObject coin = Instantiate(_coinPrefab, randomCell.position, Quaternion.identity);
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
        EmptyCells.Add(cell);
    }
    public void FillCell(Transform cell)
    {
        EmptyCells.Remove(cell);
    }
}