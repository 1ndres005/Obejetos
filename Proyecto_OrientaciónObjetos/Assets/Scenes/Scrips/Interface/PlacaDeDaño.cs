using UnityEngine;

public class TargetDamageZone : MonoBehaviour
{
    public int dañoPorSegundo = 10;
    private bool jugadorEnPlaca = false;
    private Jugador jugador;
    private float tiempoAcumulado = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.GetComponent<Jugador>();
            jugadorEnPlaca = true;
            Debug.Log("Jugador está sobre la placa.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (jugadorEnPlaca && jugador != null)
        {
            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado >= 1f) // Daño cada 1 segundo
            {
                jugador.Damage(dañoPorSegundo);
                Debug.Log("Placa infligió daño: " + dañoPorSegundo);
                tiempoAcumulado = 0f; // Reiniciar contador
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnPlaca = false;
            jugador = null;
            tiempoAcumulado = 0f;
            Debug.Log("Jugador salió de la placa.");
        }
    }
}



