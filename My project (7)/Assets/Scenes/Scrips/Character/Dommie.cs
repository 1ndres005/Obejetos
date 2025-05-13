using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour, IDamageable
{
    public Stat Life { get; private set; }

    [Header("Regeneración")]
    public float tasaRegeneracion = 20f;
    public float intervaloRegeneracion = 4f;

    [Header("Respawn")]
    public Vector3 puntoDeRespawn;
    public GameObject objetivoPrefab;

    [Header("Barra de vida")]
    public Image barraDeVida;  // Asigna el Image del UI tipo Filled → Horizontal

     public Transform spawnPoint;

    private void Awake()
    {
        Life = new Stat(0, 200);
        Life.SetCurrentValue(200);
        puntoDeRespawn = transform.position;

        ActualizarBarraDeVida(); // Mostrar vida inicial

        spawnPoint.position = transform.position;
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
        Debug.Log("Target recibió daño. Vida actual: " + Life.CurrentValue);
        ActualizarBarraDeVida();

        if (Life.CurrentValue <= 0)
        {
            Debug.Log("Target ha muerto.");
            gameObject.SetActive(false);  // Mejor que Destroy para mantener la referencia
            StartCoroutine(Respawn());
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

        if (objetivoPrefab != null)
        {
            GameObject nuevoObjetivo = Instantiate(objetivoPrefab, transform.position, Quaternion.identity);

            // Si el prefab tiene su barra asignada, se actualizará desde su Awake automáticamente
            var target = nuevoObjetivo.GetComponent<Target>();
            target.Life.SetCurrentValue(300);
            Debug.Log("Nuevo Target ha reaparecido.");
        }
    }
}

