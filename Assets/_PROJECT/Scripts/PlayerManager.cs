using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int _playerHealth;
    private int _collectedCoins;
    private int _numberOfCoins;
    private int _level;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Text _collectedCoinsTxt, _playerHealthTxt;

    [HideInInspector] public bool playerHasBeenCaught = false;

    private CharacterController _characterController;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _winlevelText;
    [SerializeField] private GameObject _loselevelText;
    [SerializeField] private GameObject _winlevelBtn;

    private void Start()
    {
        _winlevelText.SetActive(false);
        _endPanel.SetActive(false);
        _loselevelText.SetActive(false);
        _winlevelBtn.SetActive(false);

        _collectedCoins = 0;
        _playerHealth = 3;

        _collectedCoinsTxt.text = "Coins: " + _collectedCoins;
        _playerHealthTxt.text = "Health: " + _playerHealth;

        _characterController = GetComponent<CharacterController>();

        List<Coin> coins = FindObjectsOfType<Coin>().ToList();
        _numberOfCoins = coins.Count;

        _level = PlayerPrefs.GetInt("Level");
    }
    private void FixedUpdate()
    {
        if (playerHasBeenCaught)
        {
            ReturnToSpawn();
            _playerHealth--;
            playerHasBeenCaught = false;

            _collectedCoinsTxt.text = "Coins: " + _collectedCoins;
            _playerHealthTxt.text = "Health: " + _playerHealth;

            if (_playerHealth < 1)
            {
                LoseTheGame();
            }
        }

        if (_collectedCoins == _numberOfCoins)
        {
            Winlevel();
        }
    }

    private void Winlevel()
    {
        _winlevelText.SetActive(true);
        _characterController.enabled = false;

        if (_level > 1)
        {
            EndGame();
        }
        else
        {
            _winlevelBtn.SetActive(true);
        }
    }

    public void ContinueLevel()
    {
        PlayerPrefs.SetInt("Level", 2);
        SceneManager.LoadScene("Playground");
    }

    private void EndGame()
    {
        _characterController.enabled = false;
        _endPanel.SetActive(true);
    }

    private void LoseTheGame()
    {
        _characterController.enabled = false;
        _endPanel.SetActive(true);
        _loselevelText.SetActive(true);
    }

    void ReturnToSpawn()
    {
        transform.position = new Vector3(_spawnPoint.position.x, transform.position.y, _spawnPoint.position.z);
    }

    public void CoinCollected()
    {
        _collectedCoins++;
        _collectedCoinsTxt.text = "Coins: " + _collectedCoins;
    }
}
