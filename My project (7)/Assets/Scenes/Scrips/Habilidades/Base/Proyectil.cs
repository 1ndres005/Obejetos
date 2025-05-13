using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float dañoPorcentaje = 0.20f; // Daño por porcentaje

    public void Configurar(float porcentaje)
    {
        dañoPorcentaje = porcentaje;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el proyectil colisiona con un objetivo, inflige el daño y luego se destruye
        if (collision.gameObject.TryGetComponent(out IDamageable objetivo))
        {
            // Infligir daño al objetivo
            objetivo.Damage(Mathf.RoundToInt(dañoPorcentaje * 100));  // Aquí puedes ajustar el valor del daño
        }

        // Destruir el proyectil al impactar
        Destroy(gameObject);
    }
}


