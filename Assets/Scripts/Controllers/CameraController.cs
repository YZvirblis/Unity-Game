using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] float _cameraZoomModifier = .5f;
    float _minCameraZoomDistance = 1f;
    float _maxCameraZoomDistance = 5f;
    CinemachineVirtualCamera _activeCamera;
    int _activeCameraPriorityModifier = 31337;

    public Camera mainCamera;
    public CinemachineVirtualCamera cinemachine1stPerson;
    public CinemachineVirtualCamera cinemachine3rdPerson;
    CinemachineFramingTransposer _cinemachineFramingTransporter;

    void Awake()
    {
        _cinemachineFramingTransporter = cinemachine3rdPerson.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Start()
    {
        ChangeCamera();
    }
    void Update()
    {
        if (!(_input.zoomCameraInput == 0.0f)) { ZoomCamera(); };
        if (_input.changeCameraWasPressedThisFrame) { ChangeCamera(); };
    }
    void ZoomCamera()
    {
        if (_activeCamera == cinemachine3rdPerson)
        {
            _cinemachineFramingTransporter.m_CameraDistance = Mathf.Clamp(_cinemachineFramingTransporter.m_CameraDistance +
                (_input.invertScroll ? -_input.zoomCameraInput : _input.zoomCameraInput / _cameraZoomModifier), _minCameraZoomDistance,
                _maxCameraZoomDistance);
        }
    }

    private void ChangeCamera()
    {
        if (cinemachine3rdPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine3rdPerson, cinemachine1stPerson);
        }
        else if (cinemachine1stPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine1stPerson, cinemachine3rdPerson);
        }
        else
        {
            cinemachine3rdPerson.Priority += _activeCameraPriorityModifier;
            _activeCamera = cinemachine3rdPerson;
        }
    }
    private void SetCameraPriorities(CinemachineVirtualCamera currentCameraMode, CinemachineVirtualCamera newCameraMode)
    {
        currentCameraMode.Priority -= _activeCameraPriorityModifier;
        newCameraMode.Priority += _activeCameraPriorityModifier;
        _activeCamera = newCameraMode;
    }
}
