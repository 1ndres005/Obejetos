using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaDeDano : MonoBehaviour
{
    public float danoPorSegundo = 10f;
    public float duracion = 5f;
    public GameObject origen; // El lanzador de la bomba

    private List<IDamageable> objetivosDentro = new List<IDamageable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable objetivo))
        {
            if (!objetivosDentro.Contains(objetivo))
                objetivosDentro.Add(objetivo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable objetivo))
        {
            if (objetivosDentro.Contains(objetivo))
                objetivosDentro.Remove(objetivo);
        }
    }

    private void Start()
    {
        StartCoroutine(AplicarDanoPeriodicamente());
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    private IEnumerator AplicarDanoPeriodicamente()
    {
        while (true)
        {
            // Lista temporal para eliminar referencias nulas
            List<IDamageable> objetivosADestruir = new List<IDamageable>();

            foreach (var objetivo in objetivosDentro)
            {
                // Verificar si el objetivo fue destruido
                if (objetivo == null || ((MonoBehaviour)objetivo) == null)
                {
                    objetivosADestruir.Add(objetivo);
                    continue;
                }

                objetivo.Damage((int)danoPorSegundo);

                string nombreOrigen = origen != null ? origen.name : "Desconocido";
                Debug.Log($"[AreaDeDano] {((MonoBehaviour)objetivo).name} recibe {danoPorSegundo} de daño de {nombreOrigen}");
            }

            // Limpiar referencias inválidas
            foreach (var eliminado in objetivosADestruir)
            {
                objetivosDentro.Remove(eliminado);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
    }
}






