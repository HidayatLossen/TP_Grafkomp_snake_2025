using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject pausePanel;
    public Slider speedSlider;
    public Slider volumeSlider;
    public Button resumeButton;

    [Header("Game References")]
    [SerializeField] private Snake snakeScript;
    public AudioSource backgroundMusic;

    private bool isPaused = false;

    // Batas delay minimum agar snake tidak nembus obstacle
    private float safeMinDelay;
    private float maxDelay = 0.2f;

    void Start()
    {
        pausePanel.SetActive(false);

        // === Setup Slider Kecepatan ===
        speedSlider.minValue = 0f;   // kiri = lambat
        speedSlider.maxValue = 1f;   // kanan = cepat

        if (snakeScript != null)
{
    safeMinDelay = snakeScript.minDelay;
    float t = Mathf.InverseLerp(maxDelay, safeMinDelay, snakeScript.moveDelay);
    speedSlider.value = t;
    speedSlider.onValueChanged.AddListener(UpdateSnakeSpeed);
}


        // === Setup Slider Volume ===
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        if (backgroundMusic != null)
        {
            volumeSlider.value = backgroundMusic.volume;
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }

        // === Tombol Resume ===
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(TogglePause);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void UpdateSnakeSpeed(float value)
    {
        if (snakeScript != null)
        {
            // Konversi slider (0~1) ke delay (maxDelay ~ safeMinDelay)
            float newDelay = Mathf.Lerp(maxDelay, safeMinDelay, value);
            snakeScript.moveDelay = newDelay;
        }
    }

    void UpdateVolume(float value)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = value;
        }
    }
}
