using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("코인 설정")]
    [SerializeField] private int _score = 1;

    [Header("회전")]
    [SerializeField] private float _rotateSpeed = 100.0f;

    public int Score => _score;

    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}
