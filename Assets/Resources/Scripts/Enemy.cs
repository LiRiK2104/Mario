using System.Collections;
using UnityEngine;

[RequireComponent(
    typeof(EnemyMover), 
    typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerDeathZone _playerDeathZone;
    [SerializeField] private KillEnemyZone _killEnemyZone;
    [SerializeField] private GameObject _eyes;

    private static readonly int Death = Animator.StringToHash(EnemyAnimator.Triggers.Death);
    
    private EnemyMover _enemyMover;
    private Animator _animator;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _animator = GetComponent<Animator>();
    }

    public void StartDie()
    {
        _playerDeathZone.Collider.enabled = false;
        _killEnemyZone.Collider.enabled = false;
        _eyes.SetActive(false);
        
        _animator.SetTrigger(Death);
        _enemyMover.Stop();

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        float timeToDestroy = 2;

        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}

public static class EnemyAnimator
{
    public static class Triggers
    {
        public const string Death = nameof(Death);
    }
}
