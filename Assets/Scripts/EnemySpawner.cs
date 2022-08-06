using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _positionToGo;
    [SerializeField] private EnemiesDataBundle _enemiesDataBundle;
    [Space]
    [SerializeField] private float _awaitDelay;
    [SerializeField] private float _timeToReachPoint;
    [SerializeField] private float _spawnDelay;

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
                    enemy.Init(_positionToGo.position, _timeToReachPoint, enemyData.Lvl, _awaitDelay);
                    enemy.transform.localScale = Vector3.zero;
                    enemy.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack, 5f);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}
