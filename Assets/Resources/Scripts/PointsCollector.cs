using UnityEngine;
using System;

public class PointsCollector : MonoBehaviour
{
    [SerializeField] private PointsDisplayer _pointsDisplayer;
    
    private int _points = 0;
    public event Action PointsCollected;

    public int Points => _points;

    public void AddPoints(Coin coin)
    {
        _points += coin.Points;
        PointsCollected?.Invoke();
    }
}
