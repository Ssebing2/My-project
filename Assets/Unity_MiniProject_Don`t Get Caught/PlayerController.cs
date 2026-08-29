using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 인스펙터
    [Header("스피드")]
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _rotateSpeed = 20.0f;
    [SerializeField] private float _runSpeed = 8.0f;

    [Header("카메라")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 1.6f, 0f);

    [Header("중력")]
    [SerializeField] private float _gravity = -9.81f; // 중력

    [Header("물건 감지")]
    [SerializeField] private float _interactionDistance = 3.0f;
    [SerializeField] private LayerMask _interactableLayer;

    [Header("게임 매니저")]
    [SerializeField] private GameManager _gameManager;
    #endregion

    #region 변수
    private CharacterController _controller;
    private float _xRotation;
    private float _verticalVelocity; // 현재 위아래방향 움직임
    #endregion

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (_gameManager.IsGameOver())
        {
            return;
        }

        Move();
        Look();
        Gravity();
        CameraToRay();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void Move() // 플레이어 움직임
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        float currentSpeed = _moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = _runSpeed;
        }

        _controller.Move(moveDirection * currentSpeed * Time.deltaTime);

    }

    private void Look() // 플레이어 시점
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mouseX *= _rotateSpeed * Time.deltaTime;
        mouseY *= _rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        _camera.localRotation = Quaternion.Euler(_xRotation, transform.eulerAngles.y, 0f);
    }

    private void CameraFollow()
    {
        _camera.position = transform.position + _cameraOffset;
    }

    private void Gravity() // 중력
    {
        if (_controller.isGrounded)
        {
            _verticalVelocity = -2f;
        }

        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
    }

    private void CameraToRay() // 특정 Layer 감지
    {
        Vector3 origin = _camera.position;
        Vector3 directionToObject = _camera.forward;

        RaycastHit hit;

        if (Physics.Raycast(origin, directionToObject, out hit, _interactionDistance, _interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(hit.transform.name);

                IInteractable interactable = hit.transform.GetComponentInParent<IInteractable>(); // 부모 Layer에 컴포넌트 연결시켜도 되도록

                if (interactable != null)
                {
                    interactable.Interact();
                }

            }                                   
        }

        Debug.DrawRay(origin, directionToObject * _interactionDistance, Color.green);
  
    }


}
