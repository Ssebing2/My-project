using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 인스펙터
    [Header("스피드")]
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private float _mouseSensitivity = 100.0f;
    [SerializeField] private float _runSpeed = 6.0f;

    [Header("카메라")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 1.6f, 0f);

    [Header("중력")]
    [SerializeField] private float _gravity = -9.81f; // 중력

    [Header("물건 감지")]
    [SerializeField] private float _interactionDistance = 3.0f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _interactionRadius = 0.3f;

    [Header("게임 매니저")]
    [SerializeField] private GameManager _gameManager;

    [Header("애니메이터")]
    [SerializeField] private Animator _animator;

    [Header("손전등")]
    [SerializeField] private Light _flashlight;
    [SerializeField] private AudioSource _flashlightAudioSource;
    [SerializeField] private AudioClip _flashlightClickClip;

    [Header("발소리")]
    [SerializeField] private AudioSource _footstepAudioSource;
    [SerializeField] private AudioClip[] _footstepClips;

    [Header("심장박동 소리")]
    [SerializeField] private AudioSource _heartbeatAudioSource;

    [Header("상호작용 UI")]
    [SerializeField] private GameObject _interactText;
    [SerializeField] private Vector2 _interactTextOffset = new Vector2(50f, 0f);

    #endregion

    #region 변수
    private CharacterController _controller;
    private float _xRotation;
    private float _verticalVelocity; // 현재 위아래방향 움직임
    private InteractableOutline _currentOutline; // out라인 표시
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
        if (_gameManager.IsGameOver() || _gameManager.IsGameClear())
        {
            return;
        }

        Move();
        Look();
        Gravity();
        CameraToRay();
        FlashlightOnOff();
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

        float moveAmount = new Vector2(horizontal, vertical).magnitude;
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveAmount > 0.1f;

        float currentSpeed = _moveSpeed;

        if (isRunning)
        {
            currentSpeed = _runSpeed;
        }

        _controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        _animator.SetFloat("Speed", moveAmount);
        _animator.SetBool("IsRunning", isRunning);

    }

    private void Look() // 플레이어 시점
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mouseX *= _mouseSensitivity * Time.deltaTime;
        mouseY *= _mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        _camera.localRotation = Quaternion.Euler(_xRotation, transform.eulerAngles.y, 0f);
    }

    private void CameraFollow()
    {
        _camera.position = transform.position + _cameraOffset + transform.forward * 0.15f;
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

        if (Physics.SphereCast(origin, _interactionRadius, directionToObject, out hit, _interactionDistance, _interactableLayer))
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.transform.position);

            _interactText.transform.position = screenPosition + (Vector3)_interactTextOffset;

            _interactText.SetActive(true);

            InteractableOutline outline = hit.transform.GetComponentInParent<InteractableOutline>();

            if (outline != _currentOutline)
            {
                if (_currentOutline != null)
                {
                    _currentOutline.HideOutline();
                }

                _currentOutline = outline;

                if (_currentOutline != null)
                {
                    _currentOutline.ShowOutline();
                }
            }

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

        else
        {
            _interactText.SetActive(false);

            if (_currentOutline != null)
            {
                _currentOutline.HideOutline();
                _currentOutline = null;
            }
        }

            Debug.DrawRay(origin, directionToObject * _interactionDistance, Color.green);
  
    }

    private void FlashlightOnOff()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _flashlight.enabled = !_flashlight.enabled;

            _flashlightAudioSource.PlayOneShot(_flashlightClickClip);
        }
    }

    public void PlayFootstep1()
    {
        _footstepAudioSource.PlayOneShot(_footstepClips[0]);
    }

    public void PlayFootstep2()
    {
        _footstepAudioSource.PlayOneShot(_footstepClips[1]);
    }

    public void StartHeartbeat()
    {
        if (!_heartbeatAudioSource.isPlaying)
        {
            _heartbeatAudioSource.Play();
        }
    }

    public void StopHeartbeat()
    {
        if (_heartbeatAudioSource.isPlaying)
        {
            _heartbeatAudioSource.Stop();
        }
    }

    public void StopPlayer()
    {
        _animator.speed = 0f;

        _footstepAudioSource.Stop();
        _heartbeatAudioSource.Stop();
    }

    public void SetMouseSensitivity(float value)
    {
        _mouseSensitivity = value;

        Debug.Log($"마우스 감도 변경 : {value}");
    }
}
