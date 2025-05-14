using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Habilidad[] habilidades;
    private bool habilidadEnUso = false;

    public HUDManager hud; // Referencia a tu UI

    private void Awake()
    {
        if (habilidades == null || habilidades.Length == 0)
            habilidades = GetComponents<Habilidad>();
    }

    void Update()
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
        if (index < habilidades.Length && habilidades[index].PuedeUsarse())
        {
            habilidadEnUso = true;
            habilidades[index].Ejecutar(gameObject);

            if (hud != null)
                hud.HabilidadEnCooldown(index, habilidades[index].cooldown);

            StartCoroutine(EsperarCooldown(habilidades[index].cooldown));
        }
    }

    private IEnumerator EsperarCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        habilidadEnUso = false;
    }
}

