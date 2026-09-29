using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("추적 대상")]
    [SerializeField] private Transform _target;

    [Header("카메라 설정")]
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _mouseSensitivity = 3.0f;

    [Header("상하 회전 제한")]
    [SerializeField] private float _minPitch = -20.0f;
    [SerializeField] private float _maxPitch = 60.0f;

    private float _yaw;
    private float _pitch;

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        CameraRotate();
        CameraFollow();
    }

    private void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _yaw += mouseX * _mouseSensitivity;
        _pitch -= mouseY * _mouseSensitivity;

        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
    }

    private void CameraFollow()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);

        Vector3 targetPosition =
            _target.position + Vector3.up * _height;

        Vector3 cameraPosition =
            targetPosition - rotation * Vector3.forward * _distance;

        transform.position = cameraPosition;
        transform.rotation = rotation;
    }
}
