using UnityEngine;

public class PlataformaCurativa : MonoBehaviour
{
    private bool jugadorEnPlaca = false;
    private Jugador jugador;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.GetComponent<Jugador>();

            if (jugador != null)
            {
                jugador.vida.SetCurrentValue(jugador.vida.MaxValue);

                // Si tienes man� y energ�a:

                jugadorEnPlaca = true;
                Debug.Log("Jugador est� sobre la placa.");
                Debug.Log($"[{gameObject.name}] {other.name} ha sido curado completamente.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnPlaca = false;
            jugador = null;
            Debug.Log("Jugador sali� de la placa.");
        }
    }
}
