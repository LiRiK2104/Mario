using System;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private const int MinProgress = 0;
    private const int MaxProgress = 1;
    
    [SerializeField] private Platform _platform;
    [Space]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] [Range(MinProgress, MaxProgress)] private float _startProgress;
    
    [SerializeField] private float _speed = 1;


    private float _minSpeed = 0.1f;
    private float _maxSpeed = 2;
    private float _progress = 0;
    private Direction _direction = Direction.Up;


    private void Start()
    {
        _progress = _startProgress;
    }

    private void Update()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        _progress += Time.deltaTime * _speed * (int)_direction;
        _progress = Mathf.Clamp(_progress, MinProgress, MaxProgress);
        
        switch (_progress)
        {
            case MinProgress:
                _direction = Direction.Up;
                break;

            case MaxProgress:
                _direction = Direction.Down;
                break;
        }
        
        MovePlatform(_progress);
    }

    private void MovePlatform(float progress)
    {
        if (_platform == null || _pointA == null || _pointB == null)
            return;
        
        _platform.transform.position = Vector2.Lerp(_pointA.position, _pointB.position, progress);
    }

    private void OnValidate()
    {
        MovePlatform(_startProgress);
        _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        if (_pointA == null || _pointB == null)
            return;
                
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_pointA.position, _pointB.position);
    }

    private enum Direction
    {
        Up = 1,
        Down = -1
    }
}
