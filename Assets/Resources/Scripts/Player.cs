using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Collider2D _floorChecker;
   
    private static readonly int JumpFlag = Animator.StringToHash(PlayerAnimator.Triggers.Jump);
    private static readonly int IsWalking = Animator.StringToHash(PlayerAnimator.Flags.IsWalking);
    private static readonly int IsFalling = Animator.StringToHash(PlayerAnimator.Flags.IsFalling);
    
    private PlayerSpawner _playerSpawner;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private float _speed = 0;

    private void Start()
    {
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateAnimationStates();
    }

    public void SetMovingRight()
    {
        _spriteRenderer.flipX = false;
        _speed = 6;
    }

    public void SetMovingLeft()
    {
        _spriteRenderer.flipX = true;
        _speed = -6;
    }

    public void StopMove()
    {
        _speed = 0;
    }

    public void Jump(bool force = false)
    {
        if (IsGroundStanding() || force)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            _animator.SetTrigger(JumpFlag);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _playerSpawner.StartRespawn();
    }
    
    public void Recover()
    {
        gameObject.SetActive(true);
    }
    
    private void Move()
    {
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }
    
    private void UpdateAnimationStates()
    {
        bool isFalling = IsGroundStanding() == false && _rigidbody.velocity.y < 0;
        bool isWalking = IsGroundStanding() && isFalling == false && Mathf.Abs(_speed) > 0;

        _animator.SetBool(IsWalking, isWalking);
        _animator.SetBool(IsFalling, isFalling);
    }

    private bool IsGroundStanding()
    {
        int layerMask = ~(1 << 8);
        return _floorChecker.IsTouchingLayers(layerMask);
    }
}

public static class PlayerAnimator
{
    public static class Flags
    {
        public const string IsWalking = nameof(IsWalking);
        public const string IsFalling = nameof(IsFalling);
    }
    
    public static class Triggers
    {
        public const string Jump = nameof(Jump);
    }
} 

