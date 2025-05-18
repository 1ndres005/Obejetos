using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Habilidad[] habilidades;
    [SerializeField] private HUDManager hud;

    private bool habilidadEnUso = false;

    private void Awake()
    {
        if (habilidades == null || habilidades.Length == 0)
            habilidades = GetComponents<Habilidad>();
    }

    private void Update()
    {
        if (!habilidadEnUso)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) ActivarHabilidad(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ActivarHabilidad(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ActivarHabilidad(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) ActivarHabilidad(3);
        }
    }

    private void ActivarHabilidad(int index)
    {
        if (index < 0 || index >= habilidades.Length) return;

        var habilidad = habilidades[index];

        habilidadEnUso = true;

        habilidad.Ejecutar(gameObject);

        hud?.HabilidadEnCooldown(index, habilidad.cooldown);

        StartCoroutine(EsperarCooldown(habilidad.cooldown));
    }

    private IEnumerator EsperarCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        habilidadEnUso = false;
    }
}



