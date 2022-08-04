using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    [Range(1,4)]
    [SerializeField] private int _lvl;
    [Min(0)]
    [SerializeField] private int _cost;
}
