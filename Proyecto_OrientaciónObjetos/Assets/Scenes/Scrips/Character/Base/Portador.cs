using UnityEngine;
using UnityEngine.UI;

public abstract class Portador : MonoBehaviour, IDamageable
{
    public Stat Vida { get; protected set; }
    [SerializeField] protected Image barraDeVida;

    protected virtual void Awake()
    {
        Vida = new StatBasico(0, 100);
        Vida.SetCurrentValue(Vida.MaxValue);
        ActualizarBarraDeVida();
    }

    public virtual void Damage(int amount)
    {
        Vida.AffectValue(-amount);
        ActualizarBarraDeVida();

        Debug.Log($"Daño recibido: {amount} | Vida restante: {Vida.CurrentValue}");

        if (Vida.CurrentValue <= 0)
        {
            OnDeath();
        }
    }

    public virtual void Curar(int amount)
    {
        Vida.AffectValue(amount);
        ActualizarBarraDeVida();
    }

    protected virtual void ActualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)Vida.CurrentValue / Vida.MaxValue;
        }
    }

    protected abstract void OnDeath();
}
