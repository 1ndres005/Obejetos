using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HUDManager : MonoBehaviour
{
    public Slider barraVida;
    public Slider barraMana;
    public Slider barraEnergia;  // Asegúrate de que esto esté asignado correctamente

    public Image[] iconosHabilidades;

    // Este método asegura que las barras estén inicializadas antes de su uso
    private void Awake()
    {
        if (barraVida == null || barraMana == null || barraEnergia == null)
        {
            Debug.LogError("Las barras de UI no están asignadas correctamente en el HUDManager.");
        }
    }

    public void ActualizarVida(int actual, int max)
    {
        barraVida.value = (float)actual / max;
    }

    public void ActualizarMana(int actual, int max)
    {
        barraMana.value = (float)actual / max;
    }

    public void ActualizarEnergia(int actual, int max)
    {
        if (barraEnergia != null)
        {
            barraEnergia.value = (float)actual / max;
        }
        else
        {
            Debug.LogError("La barra de energía no está asignada.");
        }
    }

    public void HabilidadEnCooldown(int index, float cooldown)
    {
        StartCoroutine(MarcarCooldown(index, cooldown));
    }

    private IEnumerator MarcarCooldown(int index, float cooldown)
    {
        if (index >= iconosHabilidades.Length) yield break;

        Image icono = iconosHabilidades[index];
        Color original = icono.color;

        icono.color = new Color(original.r, original.g, original.b, 0.4f); // Opacidad reducida

        yield return new WaitForSeconds(cooldown);

        icono.color = new Color(original.r, original.g, original.b, 1f); // Restaurar opacidad
    }
}


