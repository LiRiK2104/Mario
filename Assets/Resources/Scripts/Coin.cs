using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : PlayerTriggerZone
{
    [SerializeField] private ParticleSystem _collectEffect;
    [SerializeField] private int _points = 1;

    private PointsCollector _pointsCollector;
    private SpriteRenderer _spriteRenderer;
    private int _minValue = 0;
    
    public int Points => _points;
    
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pointsCollector = FindObjectOfType<PointsCollector>();
    }

    private void OnValidate()
    {
        _points = Math.Max(_points, _minValue);
    }

    protected override void Action(Player player)
    {
        Collider.enabled = false;
        _spriteRenderer.enabled = false;
        _collectEffect.Play();
        _pointsCollector.AddPoints(this);

        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        float waitingTime = 1;

        yield return new WaitForSeconds(waitingTime);
        Destroy(gameObject);
    }
}
