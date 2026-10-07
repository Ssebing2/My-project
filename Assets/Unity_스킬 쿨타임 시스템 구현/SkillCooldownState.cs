/// <summary>
/// 스킬 사용 후 쿨타임이 진행 중인 상태
/// 쿨타임 중에는 스킬 재사용을 제한한다.
/// </summary>
public class SkillCooldownState : ISkillState
{
    private SkillController _controller;

    /// <summary>
    /// Cooldown 상태를 생성하고 SkillController를 참조한다.
    /// </summary>
    /// <param name="controller">스킬 시스템을 관리하는 SkillController</param>
    public SkillCooldownState(SkillController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// 쿨타임 중에는 스킬을 사용할 수 없으므로 동작하지 않는다.
    /// </summary>
    public void UseSkill()
    {
    }

    /// <summary>
    /// 진행 중인 쿨타임을 취소한다.
    /// </summary>
    public void CancelCooldown()
    {
        _controller.CancelCooldown();
    }
}
