using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Habilidad : MonoBehaviour
{
    public string nombre;      // Nombre de la habilidad
    public Image habilidadIcono;       // Icono de la habilidad para UI
    public float cooldown = 2f; // Tiempo de cooldown de la habilidad

    protected bool enCooldown = false; // Si est� en cooldown

    public abstract void Ejecutar(GameObject objetivo);

    public bool PuedeUsarse()
    {
        return !enCooldown;
    }

protected IEnumerator IniciarCooldown()
{
    enCooldown = true;

    // Ocultar el icono al iniciar el cooldown
    if (habilidadIcono != null)
        habilidadIcono.enabled = false;

    // Esperar el tiempo de cooldown
    yield return new WaitForSeconds(cooldown);

    enCooldown = false;

    // Mostrar el icono nuevamente al terminar el cooldown
    if (habilidadIcono != null)
        habilidadIcono.enabled = true;
}

}




