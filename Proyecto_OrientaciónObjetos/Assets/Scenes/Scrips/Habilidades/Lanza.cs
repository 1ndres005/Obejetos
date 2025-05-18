using UnityEngine;

public class Lanza : Habilidad
{
    public GameObject prefabLanza;
    public Transform puntoDisparo;
    public float fuerza = 20f;

    public int costo = 30;

    protected override void Awake()
    {
        base.Awake();
        if (usaMana == false) costoEnergia = 30; // Gasta energía
        if (usaMana == true) costoMana = 30; // Confirmamos que usa maná
        cooldown = 3f;
    }

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;
        if (prefabLanza == null || puntoDisparo == null) return;

        // Configuramos el costo de maná
        costoMana = costo;

        if (!ConsumirRecurso()) return;

        GameObject lanza = Instantiate(prefabLanza, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = lanza.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = puntoDisparo.forward * fuerza;

        if (lanza.TryGetComponent(out Proyectil proyectil))
        {
            proyectil.Configurar(0.30f); // 30% daño
        }

        StartCoroutine(IniciarCooldown());
    }
}





