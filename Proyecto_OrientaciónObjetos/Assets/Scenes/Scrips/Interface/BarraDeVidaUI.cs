using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaUI : MonoBehaviour
{
    public Image barraVida;
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 2, 0);

    private void LateUpdate()
    {
        if (objetivo == null) return;

        // Seguir al objetivo
        transform.position = objetivo.position + offset;

        // Mirar hacia la cámara
        transform.forward = Camera.main.transform.forward;
    }

    public void ActualizarVida(float actual, float max)
    {
        barraVida.fillAmount = actual / max;
    }
}

