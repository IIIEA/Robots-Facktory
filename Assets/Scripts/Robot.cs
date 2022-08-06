using UnityEngine;
using DG.Tweening;

public class Robot : MonoBehaviour
{
    private Tween _moveTween;
    private Robot _previousBotPosition;
    private bool _needStopTween = true;

    private void Update()
    {
        if(_previousBotPosition != null && Mathf.Abs(transform.position.x - _previousBotPosition.transform.position.x) < 1f)
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

    public void Init(Robot previousBot, Vector2 positionToGo, float time)
    {
        Debug.Log(previousBot);
        _previousBotPosition = previousBot;
        _moveTween = transform.DOMoveX(positionToGo.x, time).SetEase(Ease.Linear);
    }
}
