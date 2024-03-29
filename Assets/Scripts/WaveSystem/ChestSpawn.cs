using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    public GameObject Chest;

    private bool _hasSpawned = false;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GameWon == true && _hasSpawned == false)
        {
            Instantiate(Chest);
            _hasSpawned = true;
        }
    }
}
