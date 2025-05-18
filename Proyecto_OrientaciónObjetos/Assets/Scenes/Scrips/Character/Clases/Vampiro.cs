using UnityEngine;

public class Vampiro : Jugador
{
    

    private void Awake()
    {
        base.Awake();
        // Obtener o añadir el AbilitySystem
        abilitySystem = GetComponent<AbilitySystem>();
        if (abilitySystem == null)
            abilitySystem = gameObject.AddComponent<AbilitySystem>();

        // Agregar habilidades como componentes
        var aumento = gameObject.AddComponent<AumentoEstadisticas>();
        var lanza = gameObject.AddComponent<Lanza>();

        // Configurar las habilidades para usar mana
        aumento.usaMana = true;
        lanza.usaMana = true;
    }

    public override void Damage(int cantidad)
    {
        base.Damage(cantidad);
    }
}
