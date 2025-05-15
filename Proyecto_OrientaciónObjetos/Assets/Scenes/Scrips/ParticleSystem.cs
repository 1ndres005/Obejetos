using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaDeDaño : MonoBehaviour
{
    public float dañoPorSegundos = 10f;
    public float intervaloDaño = 1f; // Tiempo entre cada "tick" de daño
    public float duracion = 5f;      // Duración total del área de daño

    private HashSet<IDamageable> objetivosDentro = new HashSet<IDamageable>();
    private float temporizador = 0f;

    private void Start()
    {
        StartCoroutine(DestruirDespuésDeTiempo(duracion));
    }

    private void Update()
    {
        if (objetivosDentro.Count == 0) return;

        temporizador += Time.deltaTime;

        if (temporizador >= intervaloDaño)
        {
            // Crear una lista temporal para eliminar referencias inválidas
            List<IDamageable> objetivosValidos = new List<IDamageable>();

            foreach (var objetivo in objetivosDentro)
            {
                if (objetivo == null) continue;

                // Verificar si el MonoBehaviour fue destruido
                MonoBehaviour mb = objetivo as MonoBehaviour;
                if (mb == null || mb.gameObject == null)
                    continue;

                objetivo.Damage(Mathf.RoundToInt(dañoPorSegundos));
                objetivosValidos.Add(objetivo); // Solo agregar los válidos
            }

            objetivosDentro = new HashSet<IDamageable>(objetivosValidos);
            temporizador = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable objetivo = ObtenerIDamageable(other);
        if (objetivo != null)
        {
            objetivosDentro.Add(objetivo);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageable objetivo = ObtenerIDamageable(other);
        if (objetivo != null)
        {
            objetivosDentro.Add(objetivo); // Asegura que se mantenga si vuelve a entrar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable objetivo = ObtenerIDamageable(other);
        if (objetivo != null && objetivosDentro.Contains(objetivo))
        {
            objetivosDentro.Remove(objetivo);
        }
    }

    private IDamageable ObtenerIDamageable(Collider other)
    {
        return other.GetComponent<IDamageable>() ??
               other.GetComponentInParent<IDamageable>() ??
               other.GetComponentInChildren<IDamageable>();
    }

    private IEnumerator DestruirDespuésDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject); // Destruye el área de daño
    }
}

