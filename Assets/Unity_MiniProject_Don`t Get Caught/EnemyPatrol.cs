using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrol : MonoBehaviour
{
    #region 인스펙터
    [Header("순찰반경")]
    [SerializeField] private float _patrolRadius = 10; // 돌아다닐 반경

    [Header("대기")]
    [SerializeField] private float _time = 0f;
    [SerializeField] private float _delaytime = 2f;

    [Header("플레이어감지")]
    [SerializeField] private float _detectDistance = 8.0f;
    [SerializeField] private Transform _player;
    #endregion

    #region 변수
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
        if (!_agent.isOnNavMesh)
        {
            return;
        }

        SetRemainDistance();
        CheckPlayerDistance();
    }

    private void SetRandomDestination()
    {
        Vector3 randomPosition = Random.insideUnitSphere * _patrolRadius;

        randomPosition += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
    }

    private void SetRemainDistance()
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

    private void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <=_detectDistance)
        {
            Debug.Log("플레이어 발견");
        }
    }
}
