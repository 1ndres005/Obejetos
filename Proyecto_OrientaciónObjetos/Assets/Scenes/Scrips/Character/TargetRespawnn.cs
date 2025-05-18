using UnityEngine;
using System.Collections;

public class TargetRespawner : MonoBehaviour
{
    public GameObject prefabNPC;

    public void IniciarRespawn(Vector3 spawnPoint)
    {
        StartCoroutine(RespawnRoutine(spawnPoint));
    }

    private IEnumerator RespawnRoutine(Vector3 spawnPoint)
    {
        yield return new WaitForSeconds(10f);

        GameObject nuevoNPC = Instantiate(prefabNPC, spawnPoint, Quaternion.identity);
        nuevoNPC.SetActive(true);
        Debug.Log("NPC ha reaparecido.");
    }
}

