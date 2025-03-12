using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _groundCheck;

        [Header("Movement settings")]
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6.5f;

        [Header("Jump settings")]
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private float _turnSmoothTime = 0.1f;

        private const float _gravity = -9.81f;

        private CharacterController _controller;
        private PlayerInput _input;
        private WeaponController _weapon;
        private Camera _cam;

        private Vector2 _moveDirection;
        private Vector3 _playerVelocity;
        private float _turnSmoothVelocity;
        private float _targetAngle;
        private bool _isGrounded;

        private void Awake()
        {
            _input = new PlayerInput();
            _input.Enable();
            _controller = GetComponent<CharacterController>();
            _weapon = GetComponent<WeaponController>();
        }

        private void Start()
        {
            _cam = Camera.main;
            _input.Player.Move.performed += PlayerInput_Move;
            _input.Player.Move.canceled += PlayerInput_Move;
            _input.Player.Jump.performed += PlayerInput_Jump;
            _input.Player.Attack.performed += PlayerInput_Attack;
        }

        private void PlayerInput_Move(InputAction.CallbackContext obj)
        {
            _moveDirection = obj.ReadValue<Vector2>();
        }

        private void PlayerInput_Jump(InputAction.CallbackContext obj)
        {
            if (_isGrounded)
                Jump();
        }

        private void PlayerInput_Attack(InputAction.CallbackContext obj)
        {
            _weapon.Attack();
        }

        private void Update()
        {
            Vector3 moveDirection = new(_moveDirection.x, 0, _moveDirection.y);

            if (moveDirection.magnitude >= 0.1f)
            {
                MoveCharacter();
                RotateCharacter(moveDirection);
            }

            IsGrounded();
            ApplyGravity();
        }

        private void MoveCharacter()
        {
            Vector3 moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            Vector3 move = moveDir.normalized * _walkSpeed;
            _controller.Move((move + _playerVelocity) * Time.deltaTime);
        }

        private void RotateCharacter(Vector3 direction)
        {
            _targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        private void Jump()
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        private void IsGrounded()
        {
            float distance = 0.04f;
            Debug.DrawLine(_groundCheck.position, _groundCheck.position + (Vector3.down * distance), Color.red);
            _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, distance);
        }

        private void ApplyGravity()
        {
            if (_isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = 0f;

            _playerVelocity.y += _gravity * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
