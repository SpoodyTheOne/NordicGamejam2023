using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private RectTransform cursorTransform;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private float cursorSpeed = 1000f;

    public bool menuCursor;
    public GameObject cursorInstant;

    private bool previousMouseState;
    private Mouse virtualMouse;
    private Camera mainCamera;
    private Vector2 deltaValue;
    public bool click;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        if (menuCursor)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

            GameObject instant = Instantiate(cursorInstant, canvasRectTransform);
            cursorTransform = instant.GetComponent<RectTransform>();
        }

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("virtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if(cursorTransform != null )
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;

        cursorTransform.position = new Vector2(0, 0);
    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if(virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        /*if (menuCursor)
        {
            deltaValue = Gamepad.current.leftStick.ReadValue();
        }
        else
        {
            deltaValue = Gamepad.current.rightStick.ReadValue();
        }*/

        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (previousMouseState != aButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }

        AnchorCursor(newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }

    public void Navigate(InputAction.CallbackContext ctx)
    {
        deltaValue = ctx.ReadValue<Vector2>();
    }
}
