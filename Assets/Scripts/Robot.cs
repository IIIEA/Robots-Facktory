using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Robot : MonoBehaviour
{
    [SerializeField] private AudioClip _punchSound;

    private AudioSource _audio;
    private EnemySpawner _enemySpawner;
    private Robot _previousBotPosition;
    private Tween _moveTween;
    private Vector2 _positionToGo;
    private bool _finishPositionReached;
    private bool _needStopTween = true;
    private bool _attacked;
    private int _damage;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(_positionToGo.x)) <= 0.1f && _attacked == false)
        {
            Attack();
        }

        if (_previousBotPosition != null && Mathf.Abs(transform.position.x - _previousBotPosition.transform.position.x) < 1f)
        {
            if(_needStopTween == true)
            {
                _needStopTween = false;
                _moveTween.Pause();
            }
        }
        else
        {
            if(_needStopTween == false)
            {
                _needStopTween = true;
                _moveTween.Play();
            }
        }
    }

    public void Init(Robot previousBot, Vector2 positionToGo, float time, int damage, EnemySpawner enemySpawner)
    {
        _damage = damage;
        _enemySpawner = enemySpawner;
        _previousBotPosition = previousBot;
        _positionToGo = positionToGo;
        _moveTween = transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }

    private void Attack()
    {
        if(_enemySpawner.TryGetEnemy(out Enemy enemy))
        {
            _attacked = true;
            StartCoroutine(DestroyAwaiter(enemy));
        }
    }

    private IEnumerator DestroyAwaiter(Enemy enemy)
    {
        var tween = transform.DOShakeScale(0.1f);
        enemy.TakeDamage(_damage);
        _audio.PlayOneShot(_punchSound);

        yield return tween.WaitForCompletion();

        transform.DOScale(Vector3.zero, 0.1f);
        Destroy(gameObject, 0.11f);
    }
}
