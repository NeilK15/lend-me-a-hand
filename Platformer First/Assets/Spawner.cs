using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    #region Singleton

    public static Spawner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("WTF I have more than one spanwer????");
            return;
        }
        instance = this;
    }


    #endregion


    public GameObject enemy;

    public Transform left;
    public Transform right;

    public void Spawn()
    {
        float spawnPoint = Random.Range(left.transform.position.x, right.transform.position.x);
        Instantiate(enemy, new Vector3(spawnPoint, transform.position.y), Quaternion.identity);
        

    }

}
