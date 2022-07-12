using System.Collections.Generic;
using UnityEngine;

public class CoinContainer : MonoBehaviour
{
    [SerializeField] private PointsCollector _pointsCollector;
    [SerializeField] private List<Coin> _coins = new List<Coin>();

    private void Awake()
    {
        foreach (var coin in _coins)
        {
            coin.Init(_pointsCollector);
        }
    }
}
