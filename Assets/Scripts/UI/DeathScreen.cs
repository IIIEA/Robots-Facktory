using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private BotHealth _bot;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private List<GameObject> _objectsToDisable;

    private void Start()
    {
        _endScreen.SetActive(false);
    }

    private void OnEnable()
    {
        _bot.Dead.AddListener(OnDead);
    }

    private void OnDisable()
    {
        _bot.Dead.RemoveListener(OnDead);
    }

    private void OnDead()
    {
        UnityEngine.Cursor.visible = true;

        _endScreen.SetActive(true);

        foreach (var objectToDisable in _objectsToDisable)
        {
            objectToDisable.SetActive(false);
        }
    }
}
