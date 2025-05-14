using UnityEngine;

public class MagicBomb : MonoBehaviour
{
    public GameObject damageZonePrefab;
    public float explosionDelay = 0.1f;

    private bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;

        hasExploded = true;
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        Vector3 position = transform.position;

        // Lanza un raycast hacia abajo para encontrar el suelo
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2f))
        {
            position = hitInfo.point; // Alinea con el punto exacto del suelo
        }

        Instantiate(damageZonePrefab, position, Quaternion.identity);
        Destroy(gameObject);
    }
}
