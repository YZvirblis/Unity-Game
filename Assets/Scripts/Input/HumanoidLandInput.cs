using UnityEngine;
using UnityEngine.InputSystem;


public class HumanoidLandInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool moveIsPressed = false;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool invertMouseY { get; private set; } = true;
    public float zoomCameraInput { get; private set; } = 0.0f;
    public bool invertScroll { get; private set; } = true;
    public bool SprintInput { get; private set; } = false;

    public bool changeCameraWasPressedThisFrame { get; private set; } = false;
    InputActions _input = null;

    void OnEnable()
    {
        _input = new InputActions();
        _input.HumanoidLand.Enable();

        _input.HumanoidLand.Move.performed += SetMove;
        _input.HumanoidLand.Move.canceled += SetMove;

        _input.HumanoidLand.Look.performed += SetLook;
        _input.HumanoidLand.Look.canceled += SetLook;

        _input.HumanoidLand.ZoomCamera.started += SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled += SetZoomCamera;

        _input.HumanoidLand.Sprint.started += SetSprint;
        _input.HumanoidLand.Sprint.canceled += SetSprint;
    }

    void OnDisable()
    {
        _input.HumanoidLand.Move.performed -= SetMove;
        _input.HumanoidLand.Move.canceled -= SetMove;

        _input.HumanoidLand.Look.performed -= SetLook;
        _input.HumanoidLand.Look.canceled -= SetLook;

        _input.HumanoidLand.ZoomCamera.started -= SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled -= SetZoomCamera;

        _input.HumanoidLand.Sprint.started -= SetSprint;
        _input.HumanoidLand.Sprint.canceled -= SetSprint;

        _input.HumanoidLand.Disable();
    }

    void Update()
    {
        changeCameraWasPressedThisFrame = _input.HumanoidLand.ChangeCamera.WasPressedThisFrame();
    }

    void SetSprint(InputAction.CallbackContext ctx)
    {
        SprintInput = ctx.ReadValue<float>() > 0 ? true : false;
    }

    void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        moveIsPressed = !(MoveInput == Vector2.zero);
    }
    void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }
    void SetZoomCamera(InputAction.CallbackContext ctx)
    {
        zoomCameraInput = ctx.ReadValue<float>();
    }
}
