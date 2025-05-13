using UnityEngine;
using System.Collections;

public class AumentoEstadisticas : Habilidad
{
    public float multiplicadorVelocidad = 1.5f;
    public float multiplicadorSalto = 2f;
    public float duracion = 5f;

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;

        Jugador jugador = objetivo.GetComponent<Jugador>();
        PlayerMovement movimiento = objetivo.GetComponent<PlayerMovement>();

        if (jugador == null || movimiento == null)
        {
            Debug.LogError("Faltan componentes requeridos en el objetivo.");
            return;
        }

        int energiaActual = jugador.energia.CurrentValue;
        int energiaConsumir = Mathf.FloorToInt(energiaActual * 0.44f);

        if (energiaConsumir <= 0)
        {
            Debug.LogWarning("No hay suficiente energía para usar la habilidad.");
            return;
        }

        jugador.GastarEnergia(energiaConsumir);

        // Multiplicar velocidad con método del PlayerMovement
        movimiento.MultiplicarVelocidad(multiplicadorVelocidad * 5f);

        // Multiplicar salto directamente
        float saltoOriginal = movimiento.jumpHeight;
        movimiento.jumpHeight *= multiplicadorSalto * 5f;

        Debug.Log("Habilidad activada: velocidad y salto aumentados.");

        StartCoroutine(RestaurarEstadisticas(movimiento, saltoOriginal));
        StartCoroutine(IniciarCooldown());
    }

    private IEnumerator RestaurarEstadisticas(PlayerMovement movimiento, float saltoOriginal)
    {
        yield return new WaitForSeconds(duracion);

        movimiento.RestaurarVelocidad();
        movimiento.jumpHeight = saltoOriginal;

        Debug.Log("Estadísticas restauradas.");
    }
}



