using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public float worldSpeed;

   // InputAction pauseAction;
    InputSystem_Actions action;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        action = new InputSystem_Actions();
        action.Player.Pause.performed += Paused;
    }

    private void Start()
    {
        
    }

    private void Paused(InputAction.CallbackContext context)
    {
        
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
        }

    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }


}
