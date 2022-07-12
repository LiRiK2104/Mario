using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int _spawnPriority;
    [Space] 
    [SerializeField] private GameObject _flag;
    [SerializeField] private Transform _flagTargetPoint;
    [Space] 
    [SerializeField] private Sprite _activeStateSprite;

    private bool _isActive;
    private PlayerSpawner _playerSpawner;
    
    private SpriteRenderer _spriteRenderer;
    
    public int SpawnPriority => _spawnPriority;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _flag.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive == false && 
            collision.TryGetComponent(out Player player))
        {
            _playerSpawner.UpdateRespawnPoint(this);
            Activate();
        }
    }

    public void Init(PlayerSpawner playerSpawner)
    {
        _playerSpawner = playerSpawner;
    }

    private void Activate()
    {
        if (_isActive)
            return;
        
        _isActive = true;
        
        _flag.SetActive(true);
        _spriteRenderer.sprite = _activeStateSprite;
        StartCoroutine(RaiseFlag());
    }

    private IEnumerator RaiseFlag()
    {
        float speed = 0.5f;
        float endAnimationDistance = 0.05f;

        do
        {
            _flag.transform.position = Vector3.MoveTowards(_flag.transform.position, _flagTargetPoint.position,
                Time.deltaTime * speed);
            yield return null;
        } 
        while (GetFlagLeftDistance() > endAnimationDistance);
    }

    private float GetFlagLeftDistance()
    {
        return Vector3.Distance(_flag.transform.position, _flagTargetPoint.position);
    }
}
