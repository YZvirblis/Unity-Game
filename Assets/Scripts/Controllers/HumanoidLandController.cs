using UnityEngine;

public class HumanoidLandController : MonoBehaviour
{
    public Transform cameraFollow;
    [SerializeField] HumanoidLandInput _input;
    Rigidbody _rigidbody = null;

    Vector3 _playerMoveInput = Vector3.zero;
    Vector3 _playerLookInput = Vector3.zero;
    Vector3 _previousPlayerLookInput = Vector3.zero;
    float _cameraPitch = 0.0f;
    [SerializeField] float _playerLookInputLerpTime = 30.0f;

    [Header("Movement")]
    [SerializeField] float _movementMultiplier = 1000.0f;
    [SerializeField] float _rotationSpeedMultiplier = 180.0f;
    [SerializeField] float _pitchSpeedMultiplier = 180.0f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (_input.SprintInput)
        {
            _movementMultiplier = 2000;
        }
        else
        {
            _movementMultiplier = 1000;
        }



        _playerLookInput = getLookInput();
        PlayerLook();
        PitchCamera();

        _playerMoveInput = getMoveInput();
        playerMove();

        _rigidbody.AddRelativeForce(_playerMoveInput, ForceMode.Force);

    }

    private Vector3 getLookInput()
    {
        _previousPlayerLookInput = _playerLookInput;
        _playerLookInput = new Vector3(_input.LookInput.x, (_input.invertMouseY ? -_input.LookInput.y : _input.LookInput.y), 0.0f);
        return Vector3.Lerp(_previousPlayerLookInput, _playerLookInput * Time.deltaTime, _playerLookInputLerpTime);
    }

    private void PlayerLook()
    {
        _rigidbody.rotation = Quaternion.Euler(0.0f, _rigidbody.rotation.eulerAngles.y + (_playerLookInput.x * _rotationSpeedMultiplier), 0.0f);
    }
    private void PitchCamera()
    {
        Vector3 rotationValues = cameraFollow.rotation.eulerAngles;
        _cameraPitch += _playerLookInput.y * _pitchSpeedMultiplier;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -80f, 57.5f);

        cameraFollow.rotation = Quaternion.Euler(_cameraPitch, rotationValues.y, rotationValues.z);
    }

    private Vector3 getMoveInput()
    {
        return new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);
    }

    private void playerMove()
    {
        _playerMoveInput = (new Vector3(_playerMoveInput.x * _movementMultiplier * _rigidbody.mass * Time.deltaTime,
                                            _playerMoveInput.y,
                                            _playerMoveInput.z * _movementMultiplier * _rigidbody.mass * Time.deltaTime));
    }
}
