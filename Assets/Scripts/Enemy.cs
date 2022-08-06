using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    private int _damage;
    private Vector2 _finishPosition;
    private bool _targetReached;
    private WaitForSeconds _delay;

    private void Update()
    {
        if (transform.position.x <= _finishPosition.x && _targetReached == false)
        {
            _targetReached = true;
            StartCoroutine(AttackWaiter());
        }    
    }

    public void Init(Vector2 positionToGo, float time, int damage, float delay)
    {
        _finishPosition = positionToGo;
        _damage = damage;
        _delay = new WaitForSeconds(delay);

        transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }

    public void TakeDamage(int damage)
    {
        transform.DOShakeScale(0.1f);

        if (damage < _damage)
        {
            Attack();
        }
    }

    private IEnumerator AttackWaiter()
    {
        yield return _delay;

        Attack();
    }

    private void Attack()
    {
        transform.DOShakeScale(0.1f);
    }
}
