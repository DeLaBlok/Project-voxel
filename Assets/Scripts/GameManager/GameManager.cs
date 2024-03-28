using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesInLockOnRange = new List<GameObject>();

    public float PlayerHealth;
    public float PlayerStamina;

    public int WaveCount = 1;

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
}
