using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameGrid : MonoBehaviour
{
    [SerializeField] private GameObject _strawberryPrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private float _minSpawnTime = 5f;
    [SerializeField] private float _maxSpawnTime = 15f;
    [SerializeField]private List<Transform> availableCells = new List<Transform>();
    private Transform activeStrawberry;
    [SerializeField]private List<Transform> usedCoinCells = new List<Transform>();

    private void Start()
    {
        FillAvailableCells();
        StartCoroutine(SpawnStrawberry());
        StartCoroutine(SpawnCoin());
    }

    private void FillAvailableCells()
    {
        foreach (Transform cell in _gridParent)
        {
            availableCells.Add(cell);
        }
    }

    private IEnumerator SpawnStrawberry()
    {
        while (true)
        {
            if (activeStrawberry == null)
            {
                yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
                if (usedCoinCells.Count < availableCells.Count)
                {
                    Transform randomCell = GetRandomAvailableCell();
                    activeStrawberry = Instantiate(_strawberryPrefab, randomCell.position, Quaternion.identity, randomCell).transform;
                    // ������� �������� �������� �������� ������
                    activeStrawberry.SetParent(randomCell);
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
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

            if (availableCells.Count > 0)
            {
                Transform randomCell = GetRandomAvailableCell();
                usedCoinCells.Add(randomCell);
                GameObject coin = Instantiate(_coinPrefab, randomCell.position, Quaternion.identity);
                // ������� ������� �������� �������� ������
                coin.transform.SetParent(randomCell);
            }
        }
    }

    private Transform GetRandomAvailableCell()
    {
        int randomIndex = Random.Range(0, availableCells.Count);
        Transform randomCell = availableCells[randomIndex];
        availableCells.RemoveAt(randomIndex);
        return randomCell;
    }

    public void PlaceStrawberry(Transform strawberryTransform, Transform cellTransform)
    {
        if (availableCells.Contains(cellTransform))
        {
            strawberryTransform.position = cellTransform.position;
            availableCells.Remove(cellTransform);
        }
    }

    public void RemoveOccupiedCell(Transform cell)
    {
        availableCells.Add(cell);
    }
    public void AddOccupiedCell(Transform cell)
    {
        availableCells.Remove(cell);
    }
}