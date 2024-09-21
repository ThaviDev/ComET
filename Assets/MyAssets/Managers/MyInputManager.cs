using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInputManager : MonoBehaviour
{
    static PlayerInput _input;
    [SerializeField] PlayerInput _inputRef;

    bool _moveRight;
    bool _moveLeft;
    bool _moveUp;
    bool _moveDown;
    bool _interact;
    private void Awake()
    {
        _input = _inputRef;
    }
    public bool MoveRightButton { get { return _moveRight; } }
    public bool MoveLeftButton { get { return _moveLeft; } }
    public bool MoveUpButton { get { return _moveUp; } }
    public bool MoveDownButton { get { return _moveDown; } }
    public bool InteractButton { get { return _interact; } }
    private void Update()
    {
        if (OnMoveRight()) { _moveRight = true; } else { _moveRight = false; }
        if (OnMoveLeft()) { _moveLeft = true; } else { _moveLeft = false; }
        if (OnMoveUp()) { _moveUp = true; } else { _moveUp = false; }
        if (OnMoveDown()) { _moveDown = true; } else { _moveDown = false; }
        if (OnInteract()) { _interact = true; } else { _interact = false; }
    }

    public static bool OnMoveRight()
    {
        return _input.actions.FindAction("MoveRight").IsPressed();
    }
    public static bool OnMoveLeft()
    {
        return _input.actions.FindAction("MoveLeft").IsPressed();
    }
    public static bool OnMoveUp()
    {
        return _input.actions.FindAction("MoveUp").IsPressed();
    }
    public static bool OnMoveDown()
    {
        return _input.actions.FindAction("MoveDown").IsPressed();
    }
    public static bool OnInteract()
    {
        return _input.actions.FindAction("Interact").IsPressed();
    }
}
