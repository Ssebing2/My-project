using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;

    [Header("Camera Setting")]
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 10.0f, -8.0f);

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = _target.position + _offset;
    }
}
