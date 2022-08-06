using UnityEngine;
using DG.Tweening;

public class Robot : MonoBehaviour
{
    public void Init(Vector2 positionToGo, float time)
    {
        var a = transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }
}
