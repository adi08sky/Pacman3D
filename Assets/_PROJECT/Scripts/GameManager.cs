using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InitialData _initialData;

    private int _numberOfEnemies;
    private int _numberOfCoins;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject CoinPrefab;
    private GameObject[] _enemies;
    private GameObject[] _coins;

    [SerializeField] private Text numberOfCoinsTxt, playerHealthTxt;

    private NavMeshTriangulation triangulation;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.SetInt("Level", 1);

        if (!PlayerPrefs.HasKey("EnemySpeed"))
            PlayerPrefs.SetInt("EnemySpeed", 1);

        int level = PlayerPrefs.GetInt("Level");

        _numberOfEnemies = _initialData.GetNumberOfEnemies();

        if (level == 2)
        {
            _numberOfEnemies = _numberOfEnemies * _initialData.GetEnemiesMultiplier();
            PlayerPrefs.SetInt("EnemySpeed", 2);
        }
    }

    private void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();
        SpawnEnemy();
        SpawnCoins();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    void SpawnEnemy()
    {
        _enemies = new GameObject[_numberOfEnemies];

        for (int i = 0; i < _numberOfEnemies; i++)
        {
            int VertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;

            if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out hit, 2f, 1))
            {
                _enemies[i] = Instantiate(EnemyPrefab, hit.position, Quaternion.identity) as GameObject;
            }
        }
    }

    void SpawnCoins()
    {
        _numberOfCoins = _initialData.GetNumberOfCoins();
        _coins = new GameObject[_numberOfCoins];

        for (int i = 0; i < _numberOfCoins; i++)
        {
            int VertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;

            if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out hit, 2f, 1))
            {
                Vector3 position = new Vector3(hit.position.x, 1, hit.position.z);
                _coins[i] = Instantiate(CoinPrefab, position, Quaternion.identity) as GameObject;
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Reload()
    {
        SceneManager.LoadScene("Playground");
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("EnemySpeed", 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
