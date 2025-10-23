using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public float worldSpeed;
    public float adjustedworldSpeed;
    public float baseScrollSpeed;

    public int critterCount;
    private GameObject boss1;


    public GameObject levelCompleteUI;
    public GameObject gameplayUI;
    public GameObject bgMusic;

    // InputAction pauseAction;
    InputSystem_Actions action;

    [SerializeField] private int currentCoins;

    public int CurrentCoins { get { return currentCoins; } }

    // Reference to the UI Controller script to update the display.
    [SerializeField] private UIController uiController;
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


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        baseScrollSpeed = worldSpeed;

        LoadCoins();
    }

    private void Start()
    {
        critterCount = 0;
    }

    private void Update()
    {
        adjustedworldSpeed = worldSpeed * Time.deltaTime;

        if (critterCount > 5)
        {
            critterCount = 0;
            GameObject effect = PoolHelper.GetPool(PoolTypes.Boss1).GetPooledObject(transform.position = new Vector2(15f, 0));
            //Instantiate(boss1, new Vector2(15f, 0), Quaternion.Euler(0, 0, -90));
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.bossSpawn, transform, 1f);
        }
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

    public void NextLevel()
    {
        SaveCoins();

        Time.timeScale = 1f; // make sure game unpauses
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
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

    public void SetWorldSpeed(float speed)
    {
        worldSpeed = speed;
    }


    public void ShowLevelComplete()
    {
        gameplayUI.SetActive(false);
        levelCompleteUI.SetActive(true);
        bgMusic.SetActive(false);
        Time.timeScale = 0f;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        Debug.Log("Coins added. Current coins: " + currentCoins);

        if (uiController != null)
        {
            
            uiController.UpdateCoinCount(currentCoins);
        }
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("PlayerCoins", currentCoins);
        PlayerPrefs.Save();
        Debug.Log("Coins saved via PlayerPrefs. Total: " + currentCoins);
    }

    private void LoadCoins()
    {        
        currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        Debug.Log("Coins loaded from PlayerPrefs. Total: " + currentCoins);
    }

}
