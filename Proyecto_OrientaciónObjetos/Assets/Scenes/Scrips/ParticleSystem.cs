using UnityEngine;
using System.Collections;

public class AreaDeDaño : MonoBehaviour
{
    public float dañoPorSegundos = 10f;
    public float duracion = 5f;  // Duración antes de destruir el área de daño

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable objetivo))
        {
            objetivo.Damage(Mathf.RoundToInt(dañoPorSegundos * Time.deltaTime));
        }
    }

    // Iniciar la destrucción automática después de un tiempo
    private void Start()
    {
        // Llamar a la corutina para destruir el área después de la duración
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        // Esperar el tiempo definido antes de destruir el área
        yield return new WaitForSeconds(duracion);

        // Destruir el área de daño
        Destroy(gameObject);
    }
}

