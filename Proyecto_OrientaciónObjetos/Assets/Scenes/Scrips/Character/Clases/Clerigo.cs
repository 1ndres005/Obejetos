using UnityEngine;

public class Clerigo : Jugador
{
    
    private void Awake()
    {
        base.Awake();
        // Obtener o añadir AbilitySystem
        abilitySystem = GetComponent<AbilitySystem>();
        if (abilitySystem == null)
            abilitySystem = gameObject.AddComponent<AbilitySystem>();

        // Añadir habilidades
        var aumento = gameObject.AddComponent<AumentoEstadisticas>();
        var curacion = gameObject.AddComponent<Curacion>();
        var dispersion = gameObject.AddComponent<BombaMagica>();

        // Configurar costos y recursos si es necesario
        aumento.usaMana = true;       // Usa maná
        curacion.usaMana = true;      // Usa maná
        dispersion.usaMana = true;    // Usa maná

        // Ya no hace falta pasar las habilidades al AbilitySystem manualmente,
        // se recoge automáticamente en Awake de AbilitySystem
    }

    public override void Damage(int cantidad)
    {
        base.Damage(cantidad);
    }
}