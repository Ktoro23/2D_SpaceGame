using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;


    [SerializeField] private Image energyImage;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image experienceImage;
    [SerializeField] private TMP_Text experienceText;

    [SerializeField] private TMP_Text coinText;

    public GameObject pausePanel;

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
    }
    public void updateEnergySlider(float current, float max)
    {
        float normalizedCurrent =  Mathf.InverseLerp(0, max, current);
        energyImage.fillAmount = normalizedCurrent;      
        energyText.text = current.ToString("F0") + "/" + max.ToString("F0");

    }
    public void updateHealthSlider(float current, float max)
    {
        float normalizedCurrent = Mathf.InverseLerp(0, max, current);
        healthImage.fillAmount = normalizedCurrent;
        healthText.text = current.ToString("F0") + "/" + max.ToString("F0");

    }

    public void updateExperienceSlider(float current, float max)
    {
        float normalizedCurrent = Mathf.InverseLerp(0, max, current);
        experienceImage.fillAmount = normalizedCurrent;
        experienceText.text = current.ToString("F0") + "/" + max.ToString("F0");

    }

    public void UpdateCoinCount(int newCoinCount)
    {
        if (coinText != null)
        {
            coinText.text = newCoinCount.ToString("F0");
        }
    }
}
