using UnityEngine;
using UnityEngine.UI;

public class Difference : MonoBehaviour
{
    #region 인스펙터
    [Header("찾기 여부")]
    [SerializeField] private bool _isFind = false;
    [SerializeField] private Image _displayAnswer; // 정답 표시

    [SerializeField] private StageManager _stageManager;
 
    #endregion


    public void OnClick()
    {
        if (_isFind == true)
        {
            return;
        }

        _isFind = true; 
        _displayAnswer.gameObject.SetActive(true);

        _stageManager.FindAnswer();
    }

    public void ResetDifference()
    {
        _isFind = false;
        _displayAnswer.gameObject.SetActive(false);
    }
}
