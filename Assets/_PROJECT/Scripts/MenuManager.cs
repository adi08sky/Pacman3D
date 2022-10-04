using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InitialData _initialData;
    [SerializeField] private Slider _numberOfEnemiesSlider;
    [SerializeField] private Slider _numberOfCoinsSlider;
    [SerializeField] private Slider _enemiesMultiplierSlider;
    [SerializeField] private Text _numberOfEnemiesText;
    [SerializeField] private Text _numberOfCoinsText;
    [SerializeField] private Text _enemiesMultiplierText;

    private void Start()
    {
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("EnemySpeed", 1);
    }
    private void Update()
    {
        _numberOfEnemiesText.text = _numberOfEnemiesSlider.value.ToString();
        _numberOfCoinsText.text = _numberOfCoinsSlider.value.ToString();
        _enemiesMultiplierText.text = _enemiesMultiplierSlider.value.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnStartButton()
    {
        _initialData.SetNumberOfEnemies((int)_numberOfEnemiesSlider.value);
        _initialData.SetNumberOfCoins((int)_numberOfCoinsSlider.value);
        _initialData.SetEnemiesMultiplier((int)_numberOfEnemiesSlider.value);

        SceneManager.LoadScene("Playground");
    }
}
