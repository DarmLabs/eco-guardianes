using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<DraggingObject> residuos;
    int residuosInitialCount;
    public int ResiduosInitialCount => residuosInitialCount;
    bool finishedSpawn;
    public bool FinishedSpawn => finishedSpawn;
    [SerializeField] float minX, maxX;
    [SerializeField] float delay;
    void Start()
    {
        residuosInitialCount = residuos.Count;
        StartCoroutine(DelayObjectSpawn());
    }
    void SpawnObject()
    {
        float posX = Random.Range(minX, maxX);
        Vector3 spawnerPosition = new Vector3(posX, transform.position.y, transform.position.z);
        int residuosIndex = Random.Range(0, residuos.Count);

        residuos[residuosIndex].transform.parent.transform.position = spawnerPosition;
        residuos[residuosIndex].Spawned = true;
        residuos[residuosIndex].RecordInitialY();
        residuos.RemoveAt(residuosIndex);
    }
    IEnumerator DelayObjectSpawn()
    {
        for (int i = 0; i < residuosInitialCount; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(delay);
        }
        finishedSpawn = true;
    }
}
