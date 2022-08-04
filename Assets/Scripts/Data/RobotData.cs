using System;
using UnityEngine;

[Serializable]
public class RobotData
{
    [SerializeField] private int _lvl;
    [SerializeField] private Sprite _sprite;

    public int Lvl => _lvl;
    public Sprite Sprite => _sprite;
}
