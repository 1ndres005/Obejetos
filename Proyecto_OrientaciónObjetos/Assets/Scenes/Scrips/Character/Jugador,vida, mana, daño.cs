using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(AbilitySystem))]
public class Jugador : MonoBehaviour, IDamageable
{
    [Header("Estadísticas")]
    public Stat vida;
    public Stat mana;
    public Stat energia;

    [Header("Regeneración")]
    public float tiempoRegeneracion = 2f;
    public int cantidadRegeneracion = 20;

    [Header("Componentes")]
    [SerializeField] private HUDManager hudManager;
    protected AbilitySystem abilitySystem;

    [System.Obsolete]
    protected virtual void Awake()
    {
        // Rango de stats iniciales
        int min = 0;
        int max = 100;

        // Usar clase concreta para evitar error de instanciar clase abstracta
        vida = new StatBasico(min, max);
        mana = new StatBasico(min, max);
        energia = new StatBasico(min, max);

        vida.SetCurrentValue(max);
        mana.SetCurrentValue(max / 2);
        energia.SetCurrentValue(max / 2);

        // Obtener referencias a componentes
        hudManager = hudManager ?? FindObjectOfType<HUDManager>();
        abilitySystem = GetComponent<AbilitySystem>();
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(RegenerarRecursos), 0f, tiempoRegeneracion);
    }

    protected virtual void Update()
    {
        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        if (hudManager == null) return;

        hudManager.ActualizarVida(vida.CurrentValue, vida.MaxValue);
        hudManager.ActualizarMana(mana.CurrentValue, mana.MaxValue);
        hudManager.ActualizarEnergia(energia.CurrentValue, energia.MaxValue);
    }

    private void RegenerarRecursos()
    {
        vida.AffectValue(cantidadRegeneracion);
        mana.AffectValue(cantidadRegeneracion);
        energia.AffectValue(cantidadRegeneracion);
    }

    // Implementación de IDamageable
    public virtual void Damage(int cantidad)
    {
        vida.AffectValue(-cantidad);
        Debug.Log($"Daño recibido: {cantidad} | Vida restante: {vida.CurrentValue}");

        if (vida.CurrentValue <= 0)
        {
            Morir();
        }
    }

    public virtual void Curar(int cantidad)
    {
        vida.AffectValue(cantidad);
        Debug.Log($"Cura recibida: {cantidad}");
    }

    public virtual void GastarMana(int cantidad)
    {
        if (cantidad > mana.CurrentValue)
        {
            Debug.Log("No hay suficiente maná.");
            return;
        }

        mana.AffectValue(-cantidad);
    }

    public virtual void GastarEnergia(int cantidad)
    {
        energia.AffectValue(-cantidad);
    }

    public virtual void RecuperarEnergia(int cantidad)
    {
        energia.AffectValue(cantidad);
    }

    protected virtual void Morir()
    {
        Debug.Log("¡El jugador ha muerto!");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Método público para acceder al sistema de habilidades desde fuera
    public AbilitySystem GetAbilitySystem() => abilitySystem;

    // Métodos opcionales si necesitas ejecutar habilidades desde aquí
    public void UsarHabilidad(int index)
    {
        abilitySystem?.SendMessage("ActivarHabilidad", index);
    }
}

