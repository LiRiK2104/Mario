using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _playerInput;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerInput = new PlayerInputActions();

        _playerInput.Keyboard.MoveLeft.started += context => _player.SetMovingLeft();
        _playerInput.Keyboard.MoveRight.started += context => _player.SetMovingRight();
        _playerInput.Keyboard.MoveLeft.canceled += context => _player.StopMove();
        _playerInput.Keyboard.MoveRight.canceled += context => _player.StopMove();
        _playerInput.Keyboard.Jump.started += context => _player.Jump();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
