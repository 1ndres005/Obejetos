using UnityEngine;

public class Lanza : Habilidad
{
    public GameObject prefabLanza; // Prefab de la lanza
    public Transform puntoDisparo; // Punto desde el cual se lanza

    public float fuerza = 20f; // Fuerza con la que se lanza

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;

        if (prefabLanza == null || puntoDisparo == null)
        {
            Debug.LogError("Prefab de lanza o punto de disparo no asignado.");
            return;
        }

        Jugador jugador = objetivo.GetComponent<Jugador>();
        if (jugador == null)
        {
            Debug.LogError("El objetivo no es un jugador.");
            return;
        }

        // Consumir 30 puntos de maná
        jugador.GastarMana(30);

        // Instanciar la lanza y lanzarla
        GameObject lanza = Instantiate(prefabLanza, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = lanza.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el prefab de la lanza.");
            return;
        }

        rb.linearVelocity = puntoDisparo.forward * fuerza;

        // Configurar daño del proyectil
        Proyectil proyectil = lanza.GetComponent<Proyectil>();
        if (proyectil != null)
        {
            proyectil.Configurar(0.20f); // 20% de daño
        }

        StartCoroutine(IniciarCooldown());
    }
}



