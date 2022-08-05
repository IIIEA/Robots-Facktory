using System;
using UnityEngine;

[Serializable]
public class RobotData
{
    [SerializeField] private int _lvl;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Robot _robotTemplate;

    public int Lvl => _lvl;
    public Sprite Sprite => _sprite;
    public Robot RobotTemplate => _robotTemplate;
}
