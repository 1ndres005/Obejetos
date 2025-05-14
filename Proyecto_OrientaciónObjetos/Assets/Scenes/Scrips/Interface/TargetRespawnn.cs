using UnityEngine;
using System.Collections;

public class TargetRespawner : MonoBehaviour
{
    public GameObject targetPrefab; // Prefab del enemigo
    public Transform spawnPoint;    // Dónde reaparecerá
    public float tiempoDeRespawn = 10f;

    public void IniciarRespawn()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(tiempoDeRespawn);
        GameObject nuevo = Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Nuevo Target reapareció");
    }
}

