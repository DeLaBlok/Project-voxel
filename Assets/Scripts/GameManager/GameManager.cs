using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesInLockOnRange = new List<GameObject>();

    public int WaveCount = 1;

    public bool GameWon = false;

    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ResetGameManager()
    {
        Enemies.Clear();
        EnemiesInLockOnRange.Clear();
        WaveCount = 1;
        GameWon = false;
    }
}
