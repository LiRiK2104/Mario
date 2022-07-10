using UnityEngine;
using System;

public class PointsCollector : MonoBehaviour
{
    private int _points = 0;

    public event Action<PointsCollector> PointsCollected;

    public int Points => _points;
    
    public void AddPoints(Coin coin)
    {
        _points += coin.Points;
        PointsCollected?.Invoke(this);
    }
}
