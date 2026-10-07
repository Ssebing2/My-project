/// <summary>
/// 스킬을 사용할 수 있는 준비 상태
/// 스킬 입력 시 SkillController를 통해 스킬을 실행한다.
/// </summary>
public class SkillReadyState : ISkillState
{
    private SkillController _controller;

    /// <summary>
    /// Ready 상태를 생성하고 SkillController를 참조한다.
    /// </summary>
    /// <param name="controller">스킬 시스템을 관리하는 SkillController</param>
    public SkillReadyState(SkillController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// 스킬을 사용하고 쿨타임을 시작한다.
    /// </summary>
    public void UseSkill()
    {
        _controller.StartSkill();
    }

    /// <summary>
    /// Ready 상태에서는 진행 중인 쿨타임이 없으므로 동작하지 않는다.
    /// </summary>
    public void CancelCooldown()
    {
    }
}