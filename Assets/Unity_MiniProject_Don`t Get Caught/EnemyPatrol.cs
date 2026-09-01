using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Chase,
    Search
}

public class EnemyPatrol : MonoBehaviour
{
    #region 인스펙터
    [Header("순찰반경")]
    [SerializeField] private float _patrolRadius = 10; // 돌아다닐 반경

    [Header("대기")]
    [SerializeField] private float _time = 0f;
    [SerializeField] private float _delaytime = 2f;

    [Header("플레이어 감지")]
    [SerializeField] private float _detectDistance = 8.0f;
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewAngle = 90.0f;

    [Header("게임 매니저")]
    [SerializeField] private GameManager _gameManager;    
    #endregion

    #region 변수
    private EnemyState _currentState = EnemyState.Patrol; // 초기값

    private Vector3 _lastPlayerPosition;
    #endregion


    private NavMeshAgent _agent;    // 실제 Enemy를 움직임

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetRandomDestination();
    }

    private void Update()
    {
        if (_gameManager.IsGameOver() || _gameManager.IsGameClear())
        {
            return;
        }

        if (!_agent.isOnNavMesh)
        {
            return;
        }

        switch (_currentState)
        {
            case EnemyState.Patrol:
                SetRemainDistance();
                CheckPlayerDistance();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Search:
                SearchPlayer();
                break;
        }

       
    }

    private void SetRandomDestination() // 랜덤좌표로 이동
    {
        Vector3 randomPosition = Random.insideUnitSphere * _patrolRadius;

        randomPosition += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
    }

    private void SetRemainDistance() // 목적지 도착 후 2초대기 후 다음 목적지로 이동
    {
        if (!_agent.pathPending && _agent.remainingDistance <= 0.5f)
        {
            if (DelayTime())
            {
                Debug.Log("목적지에 도착했다.");

                SetRandomDestination();
                Debug.Log("다음 목적지로 이동.");
            }

           
        }
    }

    private bool DelayTime()
    {
        _time += Time.deltaTime;
        if (_time >= _delaytime)
        {
            _time = 0.0f;
            return true;
        }

        else
        {
            return false;
        }

    }

    private void CheckPlayerDistance() // 플레이어 감지
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <=_detectDistance)
        {
            Vector3 directionToPlayer = _player.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle <= _viewAngle * 0.5f)
            {
                RayToPlayer();

            }

        }
    }

    private void RayToPlayer()
    {
        Vector3 origin = transform.position + Vector3.up * 0.7f;
        Vector3 playerTarget = _player.position + Vector3.up * 1.0f;
        Vector3 directionToPlayer = (playerTarget - origin).normalized;

        RaycastHit hit;

        if (Physics.Raycast(origin, directionToPlayer, out hit, _detectDistance))
        {

            if (hit.transform == _player)
            {
                Debug.Log("플레이어 발견");
                _currentState = EnemyState.Chase;
            }
        }

        Debug.DrawRay(origin, directionToPlayer * _detectDistance, Color.green);
    }

    private void ChasePlayer() // 플레이어 추적
    {
        _agent.SetDestination(_player.position);

        float distance = Vector3.Distance(transform.position,_player.position);

        if (distance > _detectDistance)
        {
            _lastPlayerPosition = _player.position;
            _currentState = EnemyState.Search;

            Debug.Log("플레이어를 놓쳤다");
        }
    }

    private void SearchPlayer()
    {
        _agent.SetDestination(_lastPlayerPosition);

        CheckPlayerDistance();

        if (!_agent.pathPending && _agent.remainingDistance <= 0.5f)
        {
            if (DelayTime())
            {
                _currentState = EnemyState.Patrol;

                SetRandomDestination();
            }
        }
    }
}
