using CodeBase.CameraLogic;
using CodeBase.Coins;
using CodeBase.Enemies;
using CodeBase.PlayerLogic;
using CodeBase.Pool;
using TMPro;
using UnityEngine;

namespace CodeBase.EntryPoint
{
    public class CompositionRoot : MonoBehaviour
    {
       [SerializeField] private TargetTracker _targetTracker;
       [SerializeField] private Player _player;
       [SerializeField] private PlayerMover _playerMover;
       [SerializeField] private ObjectPool _coinPool;
       [SerializeField] private ObjectPool _enemyPool;
       [SerializeField] private Coin _coinPrefab;
       [SerializeField] private Enemy _enemyPrefab;
       [SerializeField] private Spawner _coinSpawner;
       [SerializeField] private Spawner _enemySpawner;
       [SerializeField] private int _sizeCoinPool;
       [SerializeField] private int _sizeEnemyPool;
       [SerializeField] private float _spawnInterval;
       [SerializeField] private TMP_Text _countCoin;
       [SerializeField] private GameObject _startText;
       [SerializeField] private ParticleSystem _particleSystem;

       private int _coin;
       private Vector3 _initPlayerPosition;
       private bool IsGameStarted = false;

       private void Awake()
       {
           Time.timeScale = 0f;
           _initPlayerPosition = _player.transform.position;
       }

       private void Start()
       {
           _targetTracker.Initialize(_player.transform);
           _playerMover.Initialize();
           _coinSpawner.Initialize(_coinPool,_spawnInterval);
           _enemySpawner.Initialize(_enemyPool,_spawnInterval);
           _player.Died += OnPlayerDied;
           _coinSpawner.StartSpawn();
           _enemySpawner.StartSpawn();
           _player.CoinCollecded += AddCoin;
           _playerMover.GameStart += StartGame;
       }

       private void OnDestroy()
       {
           _player.Died -= OnPlayerDied;
           _player.CoinCollecded -= AddCoin;
           _playerMover.GameStart -= StartGame;
       }

       private void AddCoin()
       {
           _coin++;
           _countCoin.text = _coin.ToString();
       }

       private void OnPlayerDied()
       {
           _coinPool.ResetPool();
           _enemyPool.ResetPool();
           RestartGame();
       }

       private void RestartGame()
       {
           Time.timeScale = 0f;
           _startText.SetActive(true);
           _player.transform.position = _initPlayerPosition;
           _particleSystem.gameObject.SetActive(false);
           ResetCoinCount();
       }

       private void ResetCoinCount()
       {
           _coin = 0;
           _countCoin.text = _coin.ToString();
       }

       private void StartGame()
       {
           Time.timeScale = 1f;
           _startText.SetActive(false);
           _coinPool.Initialize(_coinPrefab.gameObject,_sizeCoinPool);
           _enemyPool.Initialize(_enemyPrefab.gameObject,_sizeEnemyPool);
           _particleSystem.gameObject.SetActive(true);
       }
    }
}
