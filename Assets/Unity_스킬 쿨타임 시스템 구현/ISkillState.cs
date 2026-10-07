/// <summary>
/// 스킬 상태에서 수행할 수 있는 동작을 정의하는 인터페이스
/// </summary>
public interface ISkillState
{
    /// <summary>
    /// 현재 상태에서 스킬 사용을 시도한다.
    /// </summary>
    void UseSkill();

    /// <summary>
    /// 현재 상태에서 진행 중인 쿨타임 취소를 시도한다.
    /// </summary>
    void CancelCooldown();
}
