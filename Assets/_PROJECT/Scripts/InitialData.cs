using UnityEngine;

[CreateAssetMenu(fileName = "InitialData", menuName = "InitialData")]
public class InitialData: ScriptableObject
{
    private int _NumberOfEnemies = 1;
    private int _EnemiesMultiplier = 2;
    private int _NumberOfCoins = 0;

    public int GetNumberOfEnemies()
    {
        return _NumberOfEnemies;
    }
    public void SetNumberOfEnemies(int value)
    {
        _NumberOfEnemies = value;
    }

    public int GetEnemiesMultiplier()
    {
        return _EnemiesMultiplier;
    }

    public void SetEnemiesMultiplier(int value)
    {
        _EnemiesMultiplier = value;
    }

    public int GetNumberOfCoins()
    {
        return _NumberOfCoins;
    }
    public void SetNumberOfCoins(int value)
    {
        _NumberOfCoins = value;
    }
}
