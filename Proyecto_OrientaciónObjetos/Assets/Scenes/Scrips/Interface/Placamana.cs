using UnityEngine;

public class Placamana : MonoBehaviour
{
    public int da�oPorSegundo = 10;
    private bool jugadorEnPlaca = false;
    private Jugador jugador;
    private float tiempoAcumulado = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.GetComponent<Jugador>();
            jugadorEnPlaca = true;
            Debug.Log("Jugador est� sobre la placa.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (jugadorEnPlaca && jugador != null)
        {
            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado >= 1f) // Da�o cada 1 segundo
            {
                jugador.RecuperarEnergia(da�oPorSegundo);
                jugador.Recuperarmana(da�oPorSegundo);
                Debug.Log("Placa Recupero Recurso " + da�oPorSegundo);
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
            Debug.Log("Jugador sali� de la placa.");
        }
    }
}
