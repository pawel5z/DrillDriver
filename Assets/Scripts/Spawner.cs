using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform[] spawnPoints;

    private List<Transform> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Transform>();
        StartCoroutine(Main());
    }

    private IEnumerator Main()
    {
        while (true)
        {
            if (!isAnyLeft)
            {
                foreach (var spawnPoint in spawnPoints)
                    enemies.Add(Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation));
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private bool isAnyLeft
    {
        get
        {
            enemies.RemoveAll(e => e == null);
            return enemies.Exists(e => e != null);
        }
    }
}
