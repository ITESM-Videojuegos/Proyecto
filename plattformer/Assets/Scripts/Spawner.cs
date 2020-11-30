using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject treePrefab;
    public float spxMin;
    public float spxMax;
    public float spyPos;
    public float spawnTime = 10f;
    public float respawnTime;
    public float respawnTimeMin;
    public float respawnTimeMax;
    public bool stop;

    void Start()
    {
        StartCoroutine(SpawnerWait());
    }

    // Update is called once per frame
    void Update()
    {
        respawnTime = Random.Range(respawnTimeMin, respawnTimeMax);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    IEnumerator SpawnerWait()
    {
        yield return new WaitForSeconds(spawnTime);

        while (!stop)
        {
            Vector2 spawnPos = new Vector2(Random.Range((float)spxMin, (float)spxMax), spyPos);
            Instantiate(treePrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(respawnTime);

        }
    }
}
