using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    public InputActions inputActions;

    private void Awake() {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void OnDestroy() {
        inputActions.Disable();
    }
    public bool IsRightActionPressed() {
        return inputActions.Player.PlayerRight.IsPressed();
    }
    // use WasPerformedThisFrame() to check if the action was just pressed
    public bool IsLeftActionPressed() {
        return inputActions.Player.PlayerLeft.IsPressed();
    }

    public bool IsUpActionPressed() {
        return inputActions.Player.PlayerUp.IsPressed();
    }

    public bool IsDownActionPressed() {
        return inputActions.Player.PlayerDown.IsPressed();
    }

    public bool WasShootActionPerformed() {
        return inputActions.Player.PlayerShoot.WasPerformedThisFrame();
    }
}
