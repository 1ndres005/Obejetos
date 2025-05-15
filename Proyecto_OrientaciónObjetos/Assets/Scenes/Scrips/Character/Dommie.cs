using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour, IDamageable
{
    public Stat Life { get; private set; }
    public TargetRespawner respawner;
    [Header("Regeneración")]
    public float tasaRegeneracion = 20f;
    public float intervaloRegeneracion = 4f;

    [Header("Respawn")]
    public Transform spawnPoint; // Punto donde el objetivo reaparecerá

    [Header("Barra de vida")]
    public Image barraDeVida;  // Asigna un Image tipo Filled → Horizontal en el Inspector

    private void Awake()
    {
        Life = new Stat(0, 100);
        Life.SetCurrentValue(100);

        if (spawnPoint == null)
            spawnPoint = transform; // Fallback al propio objeto si no se asigna

        ActualizarBarraDeVida();
    }

    private void Start()
    {
        InvokeRepeating(nameof(RegenerarVida), 0f, intervaloRegeneracion);
    }

    private void RegenerarVida()
    {
        if (Life.CurrentValue < Life.MaxValue)
        {
            Life.AffectValue((int)tasaRegeneracion);
            Debug.Log("Target regeneró vida. Vida actual: " + Life.CurrentValue);
            ActualizarBarraDeVida();
        }
    }

    public void Damage(int amount)
    {
        Life.AffectValue(-amount);
        ActualizarBarraDeVida();
        Debug.Log("Jugador dañado por el charco. Vida restante: " + Life.CurrentValue);

        if (Life.CurrentValue <= 0)
        {
            Debug.Log("Target ha muerto.");
            respawner.IniciarRespawn();
            Destroy(gameObject);
        }
    }

    public void Curar(int amount)
    {
        Life.AffectValue(amount);
        Debug.Log("Target se curó. Vida actual: " + Life.CurrentValue);
        ActualizarBarraDeVida();
    }

    private void ActualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)Life.CurrentValue / Life.MaxValue;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(10f);

        Life.SetCurrentValue(Life.MaxValue);
        ActualizarBarraDeVida();

        transform.position = spawnPoint.position;
        gameObject.SetActive(true);

        Debug.Log("Target ha reaparecido.");
    }
}


