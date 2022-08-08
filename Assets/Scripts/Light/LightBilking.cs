using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class LightBilking : MonoBehaviour
{
    [SerializeField] private List<Light2D> _lights;
    [SerializeField] private CraftScreen _screen;
    [SerializeField] private float _delay;
    [SerializeField] private int _blinkCountTimes;

    private Dictionary<Light2D, float> _intensities = new Dictionary<Light2D, float>();
    private WaitForSeconds _waiter;

    private void Start()
    {
        _waiter = new WaitForSeconds(_delay * 2);

        foreach (var light in _lights)
        {
            _intensities.Add(light, light.intensity);
        }
    }

    private void OnEnable()
    {
        _screen.CraftComplited += OnTextChanged;
    }

    private void OnDisable()
    {
        _screen.CraftComplited -= OnTextChanged;
    }

    private void OnTextChanged()
    {
        StartCoroutine(PlayBlinkRoutine());
    }

    private IEnumerator PlayBlinkRoutine()
    {
        for (int i = 0; i < _blinkCountTimes; i++)
        {
            foreach (var light in _lights)
            {
                _intensities.TryGetValue(light, out float intensity);

                DOTween.To(() => intensity, x => intensity = x, intensity * 2, _delay).OnUpdate(() => { light.intensity = intensity; });
                DOTween.To(() => intensity, x => intensity = x, intensity, _delay).OnUpdate(() => { light.intensity = intensity; }).SetDelay(_delay);
            }

            yield return _waiter;
        }
    }
}
