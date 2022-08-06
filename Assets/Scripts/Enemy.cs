using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip _punchAudio;

    private BotHealth _bot;
    private AudioSource _audio;
    private int _damage;
    private Vector2 _finishPosition;
    private bool _targetReached;
    private WaitForSeconds _delay;
    private bool _canAttack = true;

    public bool TargetReached => _targetReached;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transform.position.x <= _finishPosition.x && _targetReached == false)
        {
            _targetReached = true;
            StartCoroutine(AttackWaiter());
        }    
    }

    public void Init(Vector2 positionToGo, float time, int damage, float delay, BotHealth bot)
    {
        _bot = bot;
        _finishPosition = positionToGo;
        _damage = damage;
        _delay = new WaitForSeconds(delay);

        transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(TakeDamageRoutine(damage));
    }

    private void SetDamage(int damage)
    {
        _bot.TakeDamage(damage);
    }


    private IEnumerator AttackWaiter()
    {
        yield return _delay;

        if (_canAttack)
            StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        var tween = transform.DOShakeScale(0.1f);
        _audio.PlayOneShot(_punchAudio);
        _bot.TakeDamage(_damage);

        yield return tween.WaitForCompletion();

        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private IEnumerator TakeDamageRoutine(int damage)
    {
        var tween = transform.DOShakeScale(0.1f);
        _canAttack = false;

        if (damage < _damage)
        {
            SetDamage(damage);
        }

        yield return tween.WaitForCompletion();

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
