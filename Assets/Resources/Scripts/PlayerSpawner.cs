using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<CheckPoint> _checkPoints = new List<CheckPoint>();
    
    private int _spawnPriority = 0;
    private Vector2 _spawnPosition;

    private void Start()
    {
        _spawnPosition = _player.transform.position;
    }

    public void StartRespawn()
    {
        StartCoroutine(Respawn());
    }

    public void UpdateRespawnPoint(CheckPoint checkPoint)
    {
        if (_spawnPriority < checkPoint.SpawnPriority)
        {
            _spawnPriority = checkPoint.SpawnPriority;
            _spawnPosition =  checkPoint.transform.position;
        }
    }
    
    private IEnumerator Respawn()
    {
        float respawnTime = 3;
        yield return new WaitForSeconds(respawnTime);

        _player.transform.position = _spawnPosition;
        _player.Recover();
    }
}
