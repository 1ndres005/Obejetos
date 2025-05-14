using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float da�oPorcentaje = 0.20f; // Da�o por porcentaje

    public void Configurar(float porcentaje)
    {
        da�oPorcentaje = porcentaje;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el proyectil colisiona con un objetivo, inflige el da�o y luego se destruye
        if (collision.gameObject.TryGetComponent(out IDamageable objetivo))
        {
            // Infligir da�o al objetivo
            objetivo.Damage(Mathf.RoundToInt(da�oPorcentaje * 100));  // Aqu� puedes ajustar el valor del da�o
        }

        // Destruir el proyectil al impactar
        Destroy(gameObject);
    }
}


