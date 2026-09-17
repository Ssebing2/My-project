using UnityEngine;

public class CTooth : MonoBehaviour
{
    private bool _alreadyPressed; // 눌린 상태
    private bool _dangerTooth; // 위험 이빨


    public enum EToothResult
    {
        None,
        Safe,
        Danger,
        AlreadyPressed
    }

    public void ResetTooth()
    {
        _alreadyPressed = false;
        _dangerTooth = false;
    }

    public void SetDanger(bool danger)
    {
        _dangerTooth = danger;
    }

    public EToothResult Press()
    {
        if (_alreadyPressed)
        {
            return EToothResult.AlreadyPressed;
        }

        _alreadyPressed = true;

        if (_dangerTooth)
        {
            return EToothResult.Danger;
        }

        return EToothResult.Safe;
    }

    public bool IsPressed()
    {
        return _alreadyPressed;
    }
}
