using Unity.VisualScripting;
using UnityEngine;

public class CCrocodileGame : MonoBehaviour
{
    [SerializeField] private CTooth[] _teeth;
    [SerializeField] private EGameState _gameState;
    [SerializeField] private int _dangerToothIndex;
    [SerializeField] private int _successCount;
    [SerializeField] private CCrocodileMouth _crocodileMouth;
    [SerializeField] private CTooth _dangerTooth;

    private enum EGameState
    {
        Playing,
        GameOver,
        Clear
    }

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        // 팀장

        /*
        ㆍ 전체 흐름

        - 게임 상태 확인
        - Restart 입력 확인
        - 클릭한 이빨 찾기
        - 이빨 Press()
        - Press 결과 확인
        - Safe / Danger / AlreadyPressed 처리
        - Clear 확인
        */
    }

    // 팀원 1 - 입력
    private bool TryGetClickedTooth(out CTooth tooth)
    {
        tooth = null;

        return false;
    }

    // 팀원 2 - 안전 처리
    private void ProcessSafeTooth(CTooth tooth)
    {
    }

    // 팀원 3 - 위험 처리
    private void ProcessDangerTooth(CTooth tooth)
    {
    }

    // 팀원 4 - 게임 규칙
    private void InitializeGame()
    {
        for (int i = 0; i < _teeth.Length; i++)
        {
            _teeth[i].ResetTooth();
        }

        _gameState = EGameState.Playing;
        _successCount = 0;
        _crocodileMouth.ResetMouth();

        if (_dangerTooth == null)
        {
            _dangerTooth = SelectDangerTooth();
        }

        else
        {
            _dangerTooth = SelectDangerTooth();

            for (int i = 0; i < _teeth.Length; i++)
            {
                if (_teeth[i] == _dangerTooth)
                {
                    _dangerToothIndex = i;
                    break;
                }
            } 
        }
    }

    private CTooth SelectDangerTooth()
    {
        _dangerToothIndex = Random.Range(0, _teeth.Length);

        _teeth[_dangerToothIndex].SetDanger(true);

        return _teeth[_dangerToothIndex];
    }

    private bool CheckClear()
    {
        _successCount = 0;

        for ( int i = 0; i < _teeth.Length; i++)
        {
            if (i == _dangerToothIndex)
            {
                continue;
            }

            if (_teeth[i].IsPressed())
            {
                _successCount++;
            }
        }

        if (_successCount == _teeth.Length - 1)
        {
            return true;
        }

        return false;
    }

    // 팀장 - 상태 / 병합
    private void ChangeGameState(EGameState nextState)
    {
    }

    private void RestartGame()
    {
    }

    private void OnGUI()
    {
    }
}
