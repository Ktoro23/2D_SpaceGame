using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public void Paused(InputAction.CallbackContext context)
    {
        
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.Pause, 1f);

            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.unPause, 1f);

            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerMovement.Instance.ExitBoost();
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

    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
           
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
