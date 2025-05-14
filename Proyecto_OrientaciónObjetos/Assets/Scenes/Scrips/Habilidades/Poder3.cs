using UnityEngine;

public class BombaMagica : Habilidad
{
    public GameObject prefabBomba;     // Prefab de la bomba
    public Transform puntoLanzamiento; // Desde dónde se lanza la bomba
    public float fuerzaLanzamiento = 10f;
    public float areaDeDaño = 5f;
    public float dañoPorSegundos = 10f;
    public int costoVida = 10;

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;

        if (prefabBomba == null || puntoLanzamiento == null)
        {
            Debug.LogError("Prefab de bomba o punto de lanzamiento no asignado.");
            return;
        }

        Jugador jugador = objetivo.GetComponent<Jugador>();
        if (jugador == null)
        {
            Debug.LogError("El objetivo no es un jugador.");
            return;
        }

        // Consumir 10 puntos de vida
        jugador.Damage(costoVida);

        // Instanciar la bomba y lanzarla
        GameObject bomba = Instantiate(prefabBomba, puntoLanzamiento.position, Quaternion.identity);

        if (bomba.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(puntoLanzamiento.forward * fuerzaLanzamiento, ForceMode.VelocityChange);
        }

        // Configurar comportamiento y destruir luego de 10 segundos
        BombaComportamiento comportamiento = bomba.AddComponent<BombaComportamiento>();
        comportamiento.Configurar(areaDeDaño, dañoPorSegundos, 10f); // duración de 10 segundos

        Destroy(bomba, 10f); // Destruir el GameObject después de 10 segundos

        StartCoroutine(IniciarCooldown());
    }
}






