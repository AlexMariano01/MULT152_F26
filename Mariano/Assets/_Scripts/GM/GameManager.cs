using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("GM Settings")]
    public static GameManager Instance;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject pauseScreen;
    public Health playerHealth;

    bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(isPaused);
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            winScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }

        if (playerHealth.currentHealth <= 0)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void GameOver()
    {
        loseScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}