using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform Spawnpoint1;
    public Transform Spawnpoint2;
    public Transform Spawnpoint3;
    public Transform Spawnpoint4;
    public Transform Spawnpoint5;
    public Transform Spawnpoint6;
    public Transform Spawnpoint7;
    public Transform Spawnpoint8;
    public Transform Spawnpoint9;

    public GameObject Goblin;
    public GameObject Skeleton;

    private bool _hasSpawned = false;

    // Update is called once per frame
    void Update()
    {
        Waves();
    }

    private void Waves()
    {
        switch(GameManager.Instance.WaveCount)
        {
            case 1:
                Wave1();
                break;
            case 2:
                Wave2();
                break;
            case 3:
                Wave3();
                break;
            case 4:
                Wave4();
                break;
            case 5:
                LastWave();
                break;
            default:
                break;
        }
    }

    private void Wave1()
    {
        if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == false)
        {
            Instantiate(Goblin, Spawnpoint3);
            _hasSpawned = true;
        }
        else if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == true)
        {
            GameManager.Instance.WaveCount ++;
            _hasSpawned = false;
        }
    }

    private void Wave2()
    {
        if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == false)
        {
            Instantiate(Goblin, Spawnpoint5);
            Instantiate(Goblin, Spawnpoint8);
            _hasSpawned = true;
        }
        else if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == true)
        {
            GameManager.Instance.WaveCount ++;
            _hasSpawned = false;
        }
    }

    private void Wave3()
    {
        if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == false)
        {
            Instantiate(Goblin, Spawnpoint1);
            Instantiate(Goblin, Spawnpoint2);
            Instantiate(Skeleton, Spawnpoint3);
            _hasSpawned = true;
        }
        else if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == true)
        {
            GameManager.Instance.WaveCount ++;
            _hasSpawned = false;
        }
    }

    private void Wave4()
    {
        if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == false)
        {
            Instantiate(Skeleton, Spawnpoint6);
            Instantiate(Skeleton, Spawnpoint7);
            Instantiate(Goblin, Spawnpoint4);
            Instantiate(Goblin, Spawnpoint9);
            _hasSpawned = true;
        }
        else if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == true)
        {
            GameManager.Instance.WaveCount ++;
            _hasSpawned = false;
        }
    }

    private void LastWave()
    {
        if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == false)
        {
            Instantiate(Skeleton, Spawnpoint6);
            Instantiate(Skeleton, Spawnpoint7);
            Instantiate(Skeleton, Spawnpoint9);
            Instantiate(Goblin, Spawnpoint1);
            Instantiate(Goblin, Spawnpoint2);
            _hasSpawned = true;
        }
        else if(GameManager.Instance.Enemies.Count == 0 && _hasSpawned == true)
        {
            GameManager.Instance.GameWon = true;
        }
    }
}
