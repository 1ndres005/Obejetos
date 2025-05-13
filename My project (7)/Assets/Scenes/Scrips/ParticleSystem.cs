using UnityEngine;
using System.Collections;

public class AreaDeDa�o : MonoBehaviour
{
    public float da�oPorSegundos = 10f;
    public float duracion = 5f;  // Duraci�n antes de destruir el �rea de da�o

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable objetivo))
        {
            objetivo.Damage(Mathf.RoundToInt(da�oPorSegundos * Time.deltaTime));
        }
    }

    // Iniciar la destrucci�n autom�tica despu�s de un tiempo
    private void Start()
    {
        // Llamar a la corutina para destruir el �rea despu�s de la duraci�n
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        // Esperar el tiempo definido antes de destruir el �rea
        yield return new WaitForSeconds(duracion);

        // Destruir el �rea de da�o
        Destroy(gameObject);
    }
}

