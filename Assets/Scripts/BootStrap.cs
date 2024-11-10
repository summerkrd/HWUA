using Cinemachine;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameManager _gameManagerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [Header("Conditions")]
    [SerializeField] private WinConditions _winConditions;
    [SerializeField] private LoseConditions _loseConditions;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = Instantiate(_gameManagerPrefab);
        _gameManager.Init(_camera, _spawnPoints, _loseConditions, _winConditions);
    }
}
