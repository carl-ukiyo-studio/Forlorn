using System;
using UnityEngine;
using UnityEngine.UI;

public class TinyBoatController : MonoBehaviour
{
    public Button btnEnter;
    private State _state;
    public float speedX = 3.0f;
    public float speedY = 3.0f;
    public float rotationSpeed = 100.0f;
    public event EventHandler OnControlVehicle;
    
    

    private enum State
    {
        Docked,
        PlayerReady,
        Flying
    }

    // Start is called before the first frame update
    void Start()
    {
        _state = State.Docked;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.PlayerReady:
                WaitForPlayerInput();
                break;
            case State.Flying:
                HandleControl();
                break;
        }
    }

    private void WaitForPlayerInput()
    {
        if (Input.GetButtonDown("Submit"))
        {
            EnterVehicle();
        }
    }

    private void EnterVehicle()
    {
        _state = State.Flying;
        btnEnter.gameObject.SetActive(false);
        OnControlVehicle?.Invoke(this, EventArgs.Empty);
    }

    private void ExitVehicle()
    {
        btnEnter.gameObject.SetActive(false);
        _state = State.Docked;
        OnControlVehicle?.Invoke(this, EventArgs.Empty);
    }

    private void HandleControl()
    {
        var translation = Input.GetAxis("Vertical") * speedX;
        var rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
        
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(Vector3.up * (speedY * Time.deltaTime));
        }
        
        if (Input.GetButton("Fire2"))
        {
            transform.Translate(Vector3.down * (speedY * Time.deltaTime));
        }

        if (Input.GetButtonDown("Submit"))
        {
            ExitVehicle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_state == State.Flying)
        {
            return;
        }
        btnEnter.gameObject.SetActive(true);
        _state = State.PlayerReady;
        Debug.Log("Show button!");
    }

    private void OnTriggerExit(Collider other)
    {
        // ExitVehicle();
    }
}