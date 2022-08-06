using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _positionToGo;
    [SerializeField] private EnemiesDataBundle _enemiesDataBundle;
    [SerializeField] private BotHealth _bot;
    [Space]
    [SerializeField] private float _awaitDelay;
    [SerializeField] private float _timeToReachPoint;
    [SerializeField] private float _spawnDelay;

    private List<Enemy> _enemies = new List<Enemy>();
    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _spawnDelay)
        {
            _timer = 0;

            if(_enemiesDataBundle.TryGetRobotByLvl(Random.Range(1,10), out EnemyData enemyData))
            {
                try
                {
                    var enemy = Instantiate(enemyData.EnemyTemplate, _spawnPoint.position, Quaternion.identity);
                    enemy.Init(_positionToGo.position, _timeToReachPoint, enemyData.Lvl, _awaitDelay, _bot);
                    enemy.transform.localScale = Vector3.zero;
                    enemy.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack, 5f);

                    _enemies.Add(enemy);
                    enemy.Died += OnEnemyDied;
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                enemy.Died -= OnEnemyDied;
        }
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        if (_enemies.Count > 0)
        {
            enemy = _enemies[0];

            return _enemies[0] != null && enemy.TargetReached == true;
        }

        enemy = null;

        return false;
    }

    public void OnEnemyDied(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
