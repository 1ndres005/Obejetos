using UnityEngine;

public class Cazador : Jugador
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
        var lanza = gameObject.AddComponent<Lanza>();

        // Configurar para que consuman energía en lugar de maná
        aumento.usaMana = false;
        aumento.costoEnergia = 30;  // Cambia el nombre de costoMana a costoEnergia en la clase si es necesario
        lanza.usaMana = false;
        lanza.costoEnergia = 30;

        // Nuevamente, AbilitySystem recogerá automáticamente las habilidades
    }

    public override void Damage(int cantidad)
    {
        base.Damage(cantidad);
    }
}
