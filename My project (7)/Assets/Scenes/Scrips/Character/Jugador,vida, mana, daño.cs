using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour, IDamageable
{
    public float velocidad = 5f;
    public float salto = 5f;

    public Stat vida;
    public Stat mana;
    public Stat energia;

    public List<Habilidad> habilidades;

    public HUDManager hudManager;

    // Par�metros de regeneraci�n autom�tica
    public float tiempoRegeneracion = 2f;
    public int cantidadRegeneracion = 20;

    private void Awake()
    {
        int min = 0;
        int max = 100;

        // Inicializaci�n de vida, man� y energ�a
        vida = new Stat(min, max);
        mana = new Stat(min, max);
        energia = new Stat(min, max);

        // Establecer a la mitad
        vida.SetCurrentValue(max / 2);
        mana.SetCurrentValue(max / 2);
        energia.SetCurrentValue(max / 2);

        // Asignar HUDManager si no est� asignado
        if (hudManager == null)
        {
            hudManager = FindObjectOfType<HUDManager>();
        }
    }

    private void Start()
    {
        // Iniciar regeneraci�n peri�dica desde el inicio del juego
        InvokeRepeating(nameof(RegenerarRecursos), 0f, tiempoRegeneracion);
    }

    private void Update()
    {
        hudManager?.ActualizarVida(vida.CurrentValue, vida.MaxValue);
        hudManager?.ActualizarMana(mana.CurrentValue, mana.MaxValue);
        hudManager?.ActualizarEnergia(energia.CurrentValue, energia.MaxValue);

        if (Input.GetKeyDown(KeyCode.Alpha1) && habilidades.Count > 0) habilidades[0]?.Ejecutar(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2) && habilidades.Count > 1) habilidades[1]?.Ejecutar(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3) && habilidades.Count > 2) habilidades[2]?.Ejecutar(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha4) && habilidades.Count > 3) habilidades[3]?.Ejecutar(gameObject);
    }

    public void Damage(int cantidad)
    {
        vida.AffectValue(-cantidad);
    }

    public void Curar(int cantidad)
    {
        vida.AffectValue(cantidad);
    }

    public void GastarEnergia(int cantidad)
    {
        energia.AffectValue(-cantidad);
    }

    public void RecuperarEnergia(int cantidad)
    {
        energia.AffectValue(cantidad);
    }

    // M�todo para gastar man�
    public void GastarMana(int cantidad)
    {
        if (cantidad > mana.CurrentValue)
        {
            Debug.Log("No hay suficiente man�.");
            return;
        }

        mana.AffectValue(-cantidad); // Resta el man�
        Debug.Log($"Man� restante: {mana.CurrentValue}");
    }

    private void RegenerarRecursos()
    {
        if (vida.CurrentValue < vida.MaxValue)
            vida.AffectValue(cantidadRegeneracion);

        if (mana.CurrentValue < mana.MaxValue)
            mana.AffectValue(cantidadRegeneracion);

        if (energia.CurrentValue < energia.MaxValue)
            energia.AffectValue(cantidadRegeneracion);
    }
}






