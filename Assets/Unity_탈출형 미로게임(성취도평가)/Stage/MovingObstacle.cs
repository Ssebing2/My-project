using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveDistance = 3.0f;
    [SerializeField] private float _moveSpeed = 2.0f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _moveToEnd = true;

    private void Start()
    {
        _startPosition = transform.position;
        _endPosition = _startPosition + Vector3.forward * _moveDistance;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        Vector3 targetPosition;

        if (_moveToEnd)
        {
            targetPosition = _endPosition;
        }
        else
        {
            targetPosition = _startPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            _moveToEnd = !_moveToEnd;
        }
    }
}
