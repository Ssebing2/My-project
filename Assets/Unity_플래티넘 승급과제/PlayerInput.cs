using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("이동 / 회전")]
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _rotateSpeed = 120.0f;

    [Header("점프 (리지드 바디 필요")]
    [SerializeField] private float _jumpImpulse = 6.0f;
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private LayerMask _groundMask = ~0;

    [Header("옵션")]
    [SerializeField] private bool _resetRotationOnRMB = true;


    private Rigidbody _playerRb;

    private Vector3 _moveDirection;
    private Animator _animator;
    private bool _isMoving;


    void Start()
    {
        TryGetComponent(out _playerRb);
        TryGetComponent(out _animator);

        if (_playerRb == null)
        {
            CPrint.Warn("점프 → 리지드 바디가 필요합니다.");

            enabled = false;
            return;
        }
    }

    private void Update()
    {
        InputMove();
        InputJump();
        UpdateAnimation();
    }

    private void InputMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(horizontal, 0f, vertical);

        _playerRb.MovePosition(
            _playerRb.position +
            _moveDirection * _moveSpeed * Time.deltaTime
        );
    }

    private void InputIdentity()
    {
        if (!_resetRotationOnRMB)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            transform.localRotation = Quaternion.identity;
        }
    }

    private void InputJump()
    {
        if (_playerRb == null)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (!isGrounded())
        {
            CPrint.Once("점프 로그", "점프 : 바닥이 아닐 때는 점프불가");
            return;
        }

        _playerRb.AddForce(Vector3.up * _jumpImpulse, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, _groundCheckDistance, _groundMask);
    }

    private void UpdateAnimation()
    {
        bool isWalking = _moveDirection.magnitude > 0.1f;
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isJumping = !isGrounded();

        _animator.SetBool("IsWalking", isWalking);
        _animator.SetBool("IsRunning", isRunning);
        _animator.SetBool("IsJumping", isJumping);
    }

}
