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

    [Header("이동 속도")]
    [SerializeField] private float _patrolSpeed = 2.0f;
    [SerializeField] private float _chaseSpeed = 4.0f;

    [Header("게임 매니저")]
    [SerializeField] private GameManager _gameManager;

    [Header("발소리")]
    [SerializeField] private AudioSource _footstepAudioSource;
    [SerializeField] private AudioClip[] _footstepClips;

    [Header("좀비 울음소리")]
    [SerializeField] private AudioSource _voiceAudioSource;
    [SerializeField] private AudioClip[] _patrolVoiceClips;
    [SerializeField] private float _voiceMinDelay = 6.0f;
    [SerializeField] private float _voiceMaxDelay = 15.0f;

    [Header("좀비 포효소리")]
    [SerializeField] private AudioSource _roarAudioSource;
    [SerializeField] private AudioClip _chaseroarClip;





    #endregion

    #region 변수
    private EnemyState _currentState = EnemyState.Patrol; // 초기값

    private Vector3 _lastPlayerPosition;

    private NavMeshAgent _agent;    // 실제 Enemy를 움직임
    private Animator _animator; // Enemy 동작
    private float _voiceTimer;
    private float _nextVoiceTime;
    #endregion




    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetRandomDestination();
        SetNextVoiceTime();
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

        _animator.SetFloat("Speed", _agent.velocity.magnitude);

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

        UpdateVoice();

    }

    private void SetRandomDestination() // 랜덤좌표로 이동
    {
        Vector3 randomPosition = Random.insideUnitSphere * _patrolRadius;

        randomPosition += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);

            bool walkVariant = Random.value > 0.5f;
            _animator.SetBool("WalkVariant", walkVariant);
        }
    }

    private void SetRemainDistance() // 목적지 도착 후 2초대기 후 다음 목적지로 이동
    {
        _animator.SetBool("IsChasing", false);
        _agent.speed = _patrolSpeed;

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
                _roarAudioSource.PlayOneShot(_chaseroarClip);
                _currentState = EnemyState.Chase;
            }
        }

        Debug.DrawRay(origin, directionToPlayer * _detectDistance, Color.green);
    }

    private void ChasePlayer() // 플레이어 추적
    {
        _animator.SetBool("IsChasing", true);
        _agent.speed = _chaseSpeed;
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
        _animator.SetBool("IsChasing", false);
        _agent.speed = _patrolSpeed;

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

    public void PlayFootstep()
    {
        int randomIndex = Random.Range(0, _footstepClips.Length);

        _footstepAudioSource.PlayOneShot(_footstepClips[randomIndex]);
    }

    public void StopEnemy()
    {
        _agent.isStopped = true;
        _animator.speed = 0f;
        _footstepAudioSource.Stop();
        _voiceAudioSource.Stop();
        _roarAudioSource.Stop();
    }

    private void SetNextVoiceTime()
    {
        _nextVoiceTime = Random.Range(_voiceMinDelay, _voiceMaxDelay);
       
    }

    private void UpdateVoice()
    {
        _voiceTimer += Time.deltaTime;

        if (_voiceTimer >= _nextVoiceTime)
        {
            int randomIndex = Random.Range(0, _patrolVoiceClips.Length);

            _voiceAudioSource.PlayOneShot(_patrolVoiceClips[randomIndex]);

            _voiceTimer = 0f;

            SetNextVoiceTime();
        }
    }
}
