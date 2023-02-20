using UnityEngine;
using UnityEngine.InputSystem;

public class InputHelper : MonoBehaviour {
  private static Controls Controls;

  public static Vector2 MoveInput { get; private set; } = Vector2.zero;
  public static bool MovePressed { get; private set; }

  public static bool FirePressedThisFrame { get; private set; }
  public static bool FirePressed { get; private set; }

  private void OnEnable() {
    Controls = new Controls();

    Controls.Keyboard.Enable();

    Controls.Keyboard.Move.performed += OnMove;
    Controls.Keyboard.Move.canceled += OnMove;
  }

  private void Update() {
    FirePressedThisFrame = Controls.Keyboard.Fire.WasPressedThisFrame();
    FirePressed = Controls.Keyboard.Fire.ReadValue<float>() > 0;
  }

  private void OnMove(InputAction.CallbackContext context) {
    MoveInput = context.ReadValue<Vector2>();
    MovePressed = MoveInput is not { x: 0f, y: 0f };
  }
}
