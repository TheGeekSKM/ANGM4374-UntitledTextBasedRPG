using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public enum PositionType
{
    Screen,
    World
}

public class TouchManager : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;

    public static TouchManager Instance { get; private set; }

    InputAction _touchPositionAction;
    InputAction _touchPressAction;
    InputAction _touchAction;

    public PositionType positionType;

    public UnityEvent<Vector2> TouchPosition;
    public UnityEvent TouchPress;
    public UnityEvent TouchRelease;

    void OnValidate()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];
        _touchAction = _playerInput.actions["Touch"];
    }

    void OnEnable()
    {
        _touchPressAction.started += OnTouchPress;
        _touchPressAction.canceled += OnTouchRelease;

    }

    void OnDisable()
    {
        _touchPressAction.started -= OnTouchPress;
        _touchPressAction.canceled -= OnTouchRelease;
    }

    void OnTouchPress(InputAction.CallbackContext context)
    {
        TouchPress?.Invoke();

        var position = _touchPositionAction.ReadValue<Vector2>();
        if (positionType == PositionType.World) position = Camera.main.ScreenToWorldPoint(position);

        TouchPosition?.Invoke(position);
    }

    void OnTouchRelease(InputAction.CallbackContext context)
    {
        // Debug.Log("Touch Released");
        TouchRelease?.Invoke();
    }
}