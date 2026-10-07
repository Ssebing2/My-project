using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _rotateSpeed = 10.0f;

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 6.0f;
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody _playerRb;
    private Vector3 _respawnPosition;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();

        _respawnPosition = transform.position;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        JumpPlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }

    private void RotatePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            _playerRb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundMask);
    }

    public void SetRespawnPosition(Vector3 position)
    {
        _respawnPosition = position;
    }

    public void RespawnPlayer()
    {
        transform.position = _respawnPosition;

        _playerRb.velocity = Vector3.zero;
        _playerRb.angularVelocity = Vector3.zero;
    }
}
