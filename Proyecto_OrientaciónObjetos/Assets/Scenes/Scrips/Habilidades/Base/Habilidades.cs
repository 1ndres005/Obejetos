using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
public abstract class Habilidad : MonoBehaviour
{
    public string nombre;
    public float cooldown = 2f;
    public bool usaMana = true;
    public Image habilidadIcono;
    public int costoMana = 20;
    public int costoEnergia = 20;

    protected bool enCooldown = false;

    protected Jugador jugador;

    protected virtual void Awake()
    {
        jugador = GetComponent<Jugador>();
        if (jugador == null)
            Debug.LogError("No se encontró componente Jugador en " + gameObject.name);
    }

    public abstract void Ejecutar(GameObject objetivo);

    public bool PuedeUsarse()
    {
        if (enCooldown) return false;

        if (usaMana)
            return jugador.mana.CurrentValue >= costoMana;
        else
            return jugador.energia.CurrentValue >= costoEnergia;
    }

    protected bool ConsumirRecurso()
    {
        if (usaMana)
        {
            if (jugador.mana.CurrentValue >= costoMana)
            {
                jugador.GastarMana(costoMana);
                return true;
            }
        }
        else
        {
            if (jugador.energia.CurrentValue >= costoEnergia)
            {
                jugador.GastarEnergia(costoEnergia);
                return true;
            }
        }
        return false;
    }

    protected IEnumerator IniciarCooldown()
    {
        enCooldown = true;
        if (habilidadIcono != null)
            habilidadIcono.enabled = false;
        yield return new WaitForSeconds(cooldown);
        enCooldown = false;
        if (habilidadIcono != null)
            habilidadIcono.enabled = true;
    }
}




