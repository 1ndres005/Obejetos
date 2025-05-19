using System.Collections.Generic;
using UnityEngine;

public class Curacion : Habilidad
{
    public int cantidadCuracion = 40;
    public float radioCuracion = 10f;

    protected override void Awake()
    {
        base.Awake();
        usaMana = true;     // Esta habilidad consume mana
        costoMana = 30;     // Define el costo en mana
        cooldown = 8f;
    }

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;
        if (!ConsumirRecurso()) return;  // Sin parámetros, usa costoMana o costoEnergia según usaMana

        Jugador jugador = objetivo.GetComponent<Jugador>();
        if (jugador == null) return;

        Collider[] colliders = Physics.OverlapSphere(jugador.transform.position, radioCuracion);
        HashSet<IDamageable> curados = new HashSet<IDamageable>();
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out IDamageable damageable))
            {
                if (!curados.Contains(damageable))
                {
                    damageable.Curar(cantidadCuracion);
                    curados.Add(damageable);
                }
            }
        }

        jugador.Curar(cantidadCuracion);

        StartCoroutine(IniciarCooldown());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioCuracion);
    }
}
