using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies Data Bundle", menuName = "Data/Enemies Data Bundle", order = 51)]
public class EnemiesDataBundle : ScriptableObject
{
    [SerializeField] private List<EnemyData> _enemies;

    public bool TryGetRobotByLvl(int lvl, out EnemyData enemyData)
    {
        enemyData = _enemies.Find(enemy => enemy.Lvl == lvl);

        return enemyData != null;
    }
}

[Serializable]
public class EnemyData
{
    [SerializeField] private int _lvl;
    [SerializeField] private GameObject _enemyTemplate;

    public int Lvl => _lvl;
    public GameObject EnemyTemplate => _enemyTemplate;
}
