using UnityEngine;
using DG.Tweening;

public class RobotPartsSpawner : MonoBehaviour
{
    private RobotPart[] _parts;
    private Vector2[] _positions;

    private void Start()
    {
        _parts = GetComponentsInChildren<RobotPart>();
        _positions = new Vector2[_parts.Length];

        for (int i = 0; i < _parts.Length; i++)
        {
            _positions[i] = _parts[i].transform.position;
        }
    }

    //Call with UnityEvent
    public void OnCraftComplited()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if(_parts[i] != null && _parts[i].transform.parent == null)
            {
                var scale = _parts[i].transform.localScale;
                var oldPart = _parts[i].gameObject;
                var newPart = Instantiate(_parts[i], _positions[i], _parts[i].transform.rotation);
                Destroy(oldPart);
                newPart.transform.parent = transform;
                newPart.transform.localScale = Vector3.zero;
                newPart.transform.DOScale(scale, 0.2f).SetEase(Ease.OutBack, 10);

                _parts[i] = newPart;
            }
            else if (_parts[i].transform.parent != transform)
            {
                var scale = _parts[i].transform.localScale;
                var newPart = Instantiate(_parts[i], _positions[i], _parts[i].transform.rotation);
                newPart.transform.parent = transform;
                newPart.transform.localScale = Vector3.zero;
                newPart.transform.DOScale(scale, 0.2f).SetEase(Ease.OutBack, 10);

                _parts[i] = newPart;
            }
        }
    }
}
