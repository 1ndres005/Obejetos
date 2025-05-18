using UnityEngine;
using System.Collections;

public class AumentoEstadisticas : Habilidad
{
    public float multiplicadorVelocidad = 5f;
    public float multiplicadorSalto = 5f;
    public float duracion = 5f;

    private float saltoOriginal;

    protected override void Awake()
    {
        base.Awake();
        if (usaMana == false) costoEnergia = 30; // Gasta energía
        if (usaMana == true) costoMana = 30;

    }

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;
        if (!ConsumirRecurso()) return;

        // Obtener componentes necesarios del objetivo
        Jugador jugador = objetivo.GetComponent<Jugador>();
        PlayerMovement movimiento = objetivo.GetComponent<PlayerMovement>();

        if (jugador == null || movimiento == null) return;

        // Aplicar aumento de estadísticas
        movimiento.MultiplicarVelocidad(multiplicadorVelocidad);

        saltoOriginal = movimiento.jumpHeight;
        movimiento.jumpHeight *= multiplicadorSalto;

        // Iniciar restauración y cooldown
        StartCoroutine(RestaurarEstadisticas(movimiento));
        StartCoroutine(IniciarCooldown());
    }

    private IEnumerator RestaurarEstadisticas(PlayerMovement movimiento)
    {
        yield return new WaitForSeconds(duracion);

        movimiento.RestaurarVelocidad();
        movimiento.jumpHeight = saltoOriginal;
    }
}








