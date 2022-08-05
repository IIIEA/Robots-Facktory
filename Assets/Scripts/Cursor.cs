using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cursor : MonoBehaviour
{
    [SerializeField] private Sprite _defaultCursor;
    [SerializeField] private Sprite _pressedCursor;

    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 cursorPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;

        if (Input.GetMouseButtonDown(0))
        {
            _spriteRenderer.sprite = _pressedCursor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _spriteRenderer.sprite = _defaultCursor;
        }
    }
}
