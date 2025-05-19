using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Curacion : Habilidad
{
    public int cantidadCuracion = 20;
    public int costoMana = 30;
    public float radioCuracion = 10f; // Radio del área de efecto

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;

        Jugador jugador = objetivo.GetComponent<Jugador>();
        if (jugador == null) return;

        if (jugador.mana.CurrentValue < costoMana)
        {
            Debug.LogWarning("No hay suficiente maná para usar la habilidad.");
            return;
        }

        jugador.mana.AffectValue(-costoMana); // Resta maná

        // Curación en área: buscar todos los colliders en un radio
        Collider[] colliders = Physics.OverlapSphere(jugador.transform.position, radioCuracion);
        HashSet<IDamageable> curados = new HashSet<IDamageable>();
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out IDamageable damageable))
            {
<<<<<<< Updated upstream
                // Curar solo una vez por objeto
                if (!curados.Contains(damageable))
=======
                if(!curados.Contains(damageable))
>>>>>>> Stashed changes
                {
                    damageable.Curar(cantidadCuracion);
                    curados.Add(damageable);
                }
<<<<<<< Updated upstream
=======
                
>>>>>>> Stashed changes
            }
        }

        Debug.Log($"Curación realizada en un radio de {radioCuracion} metros.");

        StartCoroutine(IniciarCooldown());
    }

    // Para visualizar el radio en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioCuracion);
    }
}

