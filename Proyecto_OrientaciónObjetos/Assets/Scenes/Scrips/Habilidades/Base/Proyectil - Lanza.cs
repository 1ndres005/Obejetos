using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float danoPorcentaje = 0.30f;

    public void Configurar(float porcentaje)
    {
        danoPorcentaje = porcentaje;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable objetivo))
        {
            objetivo.Damage(Mathf.RoundToInt(danoPorcentaje * 100));
        }

        Destroy(gameObject);
    }
}


