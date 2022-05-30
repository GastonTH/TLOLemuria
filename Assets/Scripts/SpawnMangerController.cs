using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMangerController : MonoBehaviour
{
    public List<SpawnPoint> _spawnPoints;

    private void Awake()
    {
        // new Vector3(4,-4,0), new Vector3(8,-4,0), new Vector3(12,-4,0),new Vector3(16,-4,0)
        _spawnPoints = new List<SpawnPoint>();
        
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(8,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(12,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(16,-4,0), "Bandits/HeavyBandit")
            );
        /*_spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );
        _spawnPoints.Add(
            new SpawnPoint(new Vector3(4,-4,0), "Bandits/HeavyBandit")
            );*/
        
    }

    public void ResuciteAll()
    {
        
    }

    public void Respawn()
    {
        for (int ii = 0; ii < _spawnPoints.Count; ii++)
        {
            if (!_spawnPoints[ii].isAlive)
            {
                Instantiate(Resources.Load("Characters/" + _spawnPoints[ii].name), _spawnPoints[ii].position, Quaternion.identity);
            }
            
        }
    }
}
