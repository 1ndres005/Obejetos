using UnityEngine;

public class Cazador : Jugador
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
        var lanza = gameObject.AddComponent<Lanza>();

        // Configurar para que consuman energ�a en lugar de man�
        aumento.usaMana = false;
        aumento.costoEnergia = 30;  // Cambia el nombre de costoMana a costoEnergia en la clase si es necesario
        lanza.usaMana = false;
        lanza.costoEnergia = 30;

        // Nuevamente, AbilitySystem recoger� autom�ticamente las habilidades
    }

    public override void Damage(int cantidad)
    {
        base.Damage(cantidad);
    }
}
