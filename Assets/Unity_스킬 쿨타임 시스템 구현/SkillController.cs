using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 스킬 사용, 쿨타임, 상태 전환 및 UI를 관리하는 컨트롤러
/// </summary>
public class SkillController : MonoBehaviour
{
    [Header("Skill")]
    [SerializeField] private float _cooldownTime = 3.0f;

    [Header("Player")]
    [SerializeField] private Renderer _playerRenderer;

    [Header("UI")]
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private TMP_Text _cooldownText;

    private ISkillState _currentState;
    private SkillReadyState _readyState;
    private SkillCooldownState _cooldownState;

    private PlayerInputActions _inputActions;
    private Coroutine _cooldownCoroutine;

    /// <summary>
    /// 입력 시스템과 스킬 상태 객체를 초기화한다.
    /// 최초 상태는 Ready 상태로 설정한다.
    /// </summary>
    private void Awake()
    {
        _inputActions = new PlayerInputActions();

        _readyState = new SkillReadyState(this);
        _cooldownState = new SkillCooldownState(this);

        _currentState = _readyState;
    }

    /// <summary>
    /// 오브젝트 활성화 시 New Input의 performed 이벤트를 등록하고
    /// Player Action Map을 활성화한다.
    /// </summary>
    private void OnEnable()
    {
        _inputActions.Player.Skill.performed += OnSkillPerformed;
        _inputActions.Player.Cancel.performed += OnCancelPerformed;

        _inputActions.Player.Enable();
    }

    /// <summary>
    /// 게임 시작 시 플레이어와 쿨타임 UI를 기본 상태로 초기화한다.
    /// </summary>
    private void Start()
    {
        ResetSkill();
    }

    /// <summary>
    /// 오브젝트 비활성화 시 입력 이벤트 등록을 해제하고
    /// Player Action Map을 비활성화한다.
    /// </summary>
    private void OnDisable()
    {
        _inputActions.Player.Skill.performed -= OnSkillPerformed;
        _inputActions.Player.Cancel.performed -= OnCancelPerformed;

        _inputActions.Player.Disable();
    }

    /// <summary>
    /// 스킬 입력이 수행되었을 때 현재 상태에 스킬 사용을 요청한다.
    /// </summary>
    /// <param name="context">Input System 입력 이벤트 정보</param>
    private void OnSkillPerformed(InputAction.CallbackContext context)
    {
        _currentState.UseSkill();
    }

    /// <summary>
    /// 쿨타임 취소 입력이 수행되었을 때 현재 상태에 취소를 요청한다.
    /// </summary>
    /// <param name="context">Input System 입력 이벤트 정보</param>
    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        _currentState.CancelCooldown();
    }

    /// <summary>
    /// 스킬을 사용하고 플레이어 색상을 변경한 뒤
    /// Cooldown 상태로 전환하여 쿨타임을 시작한다.
    /// </summary>
    public void StartSkill()
    {
        _playerRenderer.material.color = Color.red;

        _currentState = _cooldownState;

        _cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    /// <summary>
    /// 진행 중인 쿨타임 코루틴을 중지하고
    /// 즉시 Ready 상태로 복귀한다.
    /// </summary>
    public void CancelCooldown()
    {
        if (_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
            _cooldownCoroutine = null;
        }

        ResetSkill();
    }

    /// <summary>
    /// 설정된 시간 동안 쿨타임을 진행하며
    /// 남은 시간에 따라 게이지와 텍스트를 갱신한다.
    /// </summary>
    /// <returns>프레임 단위 쿨타임 처리를 위한 IEnumerator</returns>
    private IEnumerator CooldownCoroutine()
    {
        float currentTime = _cooldownTime;

        _cooldownSlider.value = 1.0f;

        while (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0.0f)
            {
                currentTime = 0.0f;
            }

            _cooldownSlider.value = currentTime / _cooldownTime;
            _cooldownText.text = currentTime.ToString("F1");

            yield return null;
        }

        _cooldownCoroutine = null;

        ResetSkill();
    }

    /// <summary>
    /// 플레이어 색상과 쿨타임 UI를 초기화하고
    /// 스킬 상태를 Ready 상태로 변경한다.
    /// </summary>
    private void ResetSkill()
    {
        _playerRenderer.material.color = Color.white;

        _cooldownSlider.value = 0.0f;
        _cooldownText.text = "0.0";

        _currentState = _readyState;
    }
}
