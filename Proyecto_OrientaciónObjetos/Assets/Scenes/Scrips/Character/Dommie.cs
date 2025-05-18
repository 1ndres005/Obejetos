using UnityEngine;

public class NPC : Portador
{
    [Header("Regeneración")]
    [SerializeField] private float tasaRegeneracion = 20f;
    [SerializeField] private float intervaloRegeneracion = 4f;

    [Header("Respawn")]
    [SerializeField] private Transform spawnPoint;
    public TargetRespawner respawner;

    protected override void Awake()
    {
        base.Awake();

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    private void Start()
    {
        InvokeRepeating(nameof(RegenerarVida), 0f, intervaloRegeneracion);
    }

    private void RegenerarVida()
    {
        if (Vida.CurrentValue < Vida.MaxValue)
        {
            Vida.AffectValue((int)tasaRegeneracion);
            ActualizarBarraDeVida();
        }
    }

    protected override void OnDeath()
    {
        Debug.Log("NPC ha muerto.");
        respawner?.IniciarRespawn(spawnPoint.position);
        Destroy(gameObject);
    }
}


