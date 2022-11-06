using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject tinyBoat;
    [SerializeField] private GameObject player;
    private TinyBoatController _tinyBoatControllerScript;
    private ThirdPersonController _playerThirdPersonControllerScript;
    private Animator _animator;
    private bool _playerFollowCamera = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerThirdPersonControllerScript = player.GetComponent<ThirdPersonController>();
        _tinyBoatControllerScript = tinyBoat.GetComponent<TinyBoatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _tinyBoatControllerScript.OnControlVehicle += SwitchState;
    }

    private void SwitchState(object sender, EventArgs eventArgs)
    {
        if (_playerFollowCamera)
        {
            _animator.Play("TinyBoatFollowCamera");
            player.transform.SetParent(tinyBoat.transform);
        }
        else
        {
            _animator.Play("PlayerFollowCamera");
            _playerThirdPersonControllerScript.enabled = true;
            player.transform.SetParent(null);
        }

        _playerFollowCamera = !_playerFollowCamera;
    }
}