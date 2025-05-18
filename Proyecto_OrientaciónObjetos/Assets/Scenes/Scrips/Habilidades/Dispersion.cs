using UnityEngine;

public class BombaMagica : Habilidad
{
    public GameObject prefabBomba;
    public GameObject prefabCharco; // Prefab del área de daño
    public Transform puntoLanzamiento;
    public float fuerzaLanzamiento = 10f;
    public float areaDeDano = 5f;
    public float danoPorSegundo = 10f;

    protected override void Awake()
    {
        base.Awake();
        usaMana = true;
        costoMana = 20;
        cooldown = 2f;
    }

    public override void Ejecutar(GameObject objetivo)
    {
        if (!PuedeUsarse()) return;
        if (!ConsumirRecurso()) return;

        GameObject bomba = Instantiate(prefabBomba, puntoLanzamiento.position, Quaternion.identity);

        if (bomba.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(puntoLanzamiento.forward * fuerzaLanzamiento, ForceMode.VelocityChange);
        }

        BombaComportamiento comportamiento = bomba.AddComponent<BombaComportamiento>();
        comportamiento.Configurar(areaDeDano, danoPorSegundo, 5f, prefabCharco, gameObject);

        StartCoroutine(IniciarCooldown());
    }
}


