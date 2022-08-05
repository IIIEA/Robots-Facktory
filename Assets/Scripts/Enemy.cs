using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public void Init(Vector2 positionToGo, float time)
    {
        transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }
}
