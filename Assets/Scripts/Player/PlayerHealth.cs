using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private float _health;
    private float _lerpTimer;

    [Header("Health Bar")]
    public float _maxHealth = 100f;
    public float _chipSpeed = 2f;
    public Image _frontHealthBar;
    public Image _backHealthBar;

    [Header("Damage Overlay")]
    public Image _overlay;
    public float _duration;
    public float _fadeSpeed;

    private float _durationTimer;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public string[] scenesToReload = { "MAINGAME", "Game" }; // Replace with your scene names
    private bool isDead = false;

    void Start()
    {
        _health = _maxHealth;
        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, 0);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        UpdateHealthUI();

        if (_overlay.color.a > 0)
        {
            if (_health < 30)
                return;

            _durationTimer += Time.deltaTime;

            if (_durationTimer > _duration)
            {
                float _tempAlpha = _overlay.color.a;
                _tempAlpha -= Time.deltaTime * _fadeSpeed;
                _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, _tempAlpha);
            }
        }

        // Check if player died
        if (_health <= 0 && !isDead)
        {
            isDead = true;
            OnPlayerDeath();
        }
    }

    public void UpdateHealthUI()
    {
        float _fillFront = _frontHealthBar.fillAmount;
        float _fillBack = _backHealthBar.fillAmount;
        float _healthFraction = _health / _maxHealth;

        if (_fillBack > _healthFraction) // Taking damage
        {
            _frontHealthBar.fillAmount = _healthFraction;
            _backHealthBar.color = Color.red;
            _lerpTimer += Time.deltaTime;
            float _percentComplete = _lerpTimer / _chipSpeed;
            _backHealthBar.fillAmount = Mathf.Lerp(_fillBack, _healthFraction, _percentComplete);
        }
        else if (_fillFront < _healthFraction) // Healing
        {
            _backHealthBar.fillAmount = _healthFraction;
            _backHealthBar.color = Color.green;
            _lerpTimer += Time.deltaTime;
            float _percentComplete = _lerpTimer / _chipSpeed;
            _frontHealthBar.fillAmount = Mathf.Lerp(_fillFront, _healthFraction, _percentComplete);
        }
        else
        {
            _lerpTimer = 0f;
        }
    }

    public void TakeDamage(float _damage)
    {
        _health -= _damage;
        _lerpTimer = 0f;
        _durationTimer = 0;
        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, 1);
        if(_health <=0f)
            OnPlayerDeath();
    }

    public void RestoreHealth(float _healAmount)
    {
        _health += _healAmount;
        _lerpTimer = 0f;
    }

    void OnPlayerDeath()
    {
        Time.timeScale = 0f; // Pause the game
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    //  This gets called by the restart button
    public void RestartGame()
    {
        // Time.timeScale = 1f; // Resume time before restarting
        //StartCoroutine(ReloadScenes());
        SceneManager.LoadScene("menu");
    }
    public GameObject _gameWinPnel;
    public void OnPlayerWin()
    {
        _gameWinPnel.SetActive(true);
        Debug.Log("hello");
        Time.timeScale=0f;
    }

    IEnumerator ReloadScenes()
    {
        foreach (string sceneName in scenesToReload)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                yield return SceneManager.UnloadSceneAsync(sceneName);
        }

        yield return new WaitForSecondsRealtime(0.2f);

        foreach (string sceneName in scenesToReload)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}
