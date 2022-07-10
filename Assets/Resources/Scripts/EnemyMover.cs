﻿using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Collider2D _findingFloorZone;
    [SerializeField] private Collider2D _findingWallZone;
    
    private int _layerMask = ~((1 << 8) | (1 << 11));
    private float _speed = 2;

    public void Stop()
    {
        _speed = 0;
    }
    
    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        TryRotate();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void TryRotate()
    {
        if (_findingFloorZone.IsTouchingLayers(_layerMask) == false || _findingWallZone.IsTouchingLayers(_layerMask))
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
        }
    }
}
