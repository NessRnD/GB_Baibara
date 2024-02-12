using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseUIControl : MonoBehaviour
{
    [SerializeField] private UnityEvent onPauseMenuEvent;
    [SerializeField] private UnityEvent offPauseMenuEvent;

    private Controls controls;
    private bool isPaused;

    private void Awake()
    {
        isPaused = false;
        controls = new Controls();
        controls.PauseMenu.Pause.performed += OnPause;
    }

    public void EnableControls()
    {
        OnEnable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
    
        if (isPaused)
        {
            Debug.Log("UnPaused");
            isPaused = false;
            PauseOff();
        }
        else
        {
            Debug.Log("Paused");
            isPaused = true;
            PauseOn();
        }
    }

    private void PauseOn()
    {
        onPauseMenuEvent.Invoke();
        controls.Player.Disable();
    }

    private void PauseOff()
    {
        offPauseMenuEvent.Invoke();
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        controls.PauseMenu.Enable();
    }

    private void OnDisable()
    {
        controls.PauseMenu.Disable();
    }
}

