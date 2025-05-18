using UnityEngine;

public class BombaComportamiento : MonoBehaviour
{
    public float duracion = 10f;
    public float radio = 5f;
    public float danoPorSegundo = 10f;
    public GameObject prefabAreaDeDano;
    public GameObject origen;

    private bool haExplotado = false;

    public void Configurar(float radio, float danoPorSegundo, float duracion, GameObject prefabAreaDeDano, GameObject origen)
    {
        this.radio = radio;
        this.danoPorSegundo = danoPorSegundo;
        this.duracion = duracion;
        this.prefabAreaDeDano = prefabAreaDeDano;
        this.origen = origen;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!haExplotado)
        {
            haExplotado = true;
            ActivarAreaDeDano();
        }
    }

    private void ActivarAreaDeDano()
    {
        GameObject charco = Instantiate(prefabAreaDeDano, transform.position, Quaternion.identity);
        charco.transform.localScale = Vector3.one * radio;

        AreaDeDano area = charco.GetComponent<AreaDeDano>();
        if (area != null)
        {
            area.danoPorSegundo = danoPorSegundo;
            area.duracion = duracion;
            area.origen = origen;
        }

        Destroy(gameObject, 0.1f); // Destruir bomba después de activar charco
    }
}





