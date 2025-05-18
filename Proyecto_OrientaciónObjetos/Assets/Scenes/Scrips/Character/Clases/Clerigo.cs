using UnityEngine;

public class Clerigo : Jugador
{
    
    private void Awake()
    {
        base.Awake();
        // Obtener o a�adir AbilitySystem
        abilitySystem = GetComponent<AbilitySystem>();
        if (abilitySystem == null)
            abilitySystem = gameObject.AddComponent<AbilitySystem>();

        // A�adir habilidades
        var aumento = gameObject.AddComponent<AumentoEstadisticas>();
        var curacion = gameObject.AddComponent<Curacion>();
        var dispersion = gameObject.AddComponent<BombaMagica>();

        // Configurar costos y recursos si es necesario
        aumento.usaMana = true;       // Usa man�
        curacion.usaMana = true;      // Usa man�
        dispersion.usaMana = true;    // Usa man�

        // Ya no hace falta pasar las habilidades al AbilitySystem manualmente,
        // se recoge autom�ticamente en Awake de AbilitySystem
    }

    public override void Damage(int cantidad)
    {
        base.Damage(cantidad);
    }
}