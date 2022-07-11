using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PlayerTriggerZone : MonoBehaviour
{
    public Collider2D Collider { get; private set; }

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Action(player);
        }
    }
    
    protected abstract void Action(Player player);
}
