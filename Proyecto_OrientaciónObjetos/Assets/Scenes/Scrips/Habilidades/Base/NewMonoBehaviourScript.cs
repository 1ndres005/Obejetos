using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class BombaComportamiento : MonoBehaviour
{
    public float duracion = 5f;              // Duración de 5 segundos
    public float radio = 5f;                 // Radio de daño
    public float dañoPorSegundo = 10f;       // Daño por segundo en el área

    private bool haExplotado = false;        // Para verificar si ya ha explotado
    private GameObject areaDeDañoGO;         // Referencia al área de daño

    // Método para configurar la bomba
    public void Configurar(float radio, float dañoPorSegundo, float duracion)
    {
        this.radio = radio;
        this.dañoPorSegundo = dañoPorSegundo;
        this.duracion = duracion;
    }

    // Detecta el impacto con otros objetos
    private void OnCollisionEnter(Collision collision)
    {
        if (!haExplotado)
        {
            haExplotado = true;  // Marca que la bomba ya explotó
            ActivarAreaDeDaño(); // Activa el área de daño
        }
    }

    // Activa el área de daño cuando la bomba explota
    private void ActivarAreaDeDaño()
    {
        // Crear un objeto hijo para representar el área de daño
        areaDeDañoGO = new GameObject("AreaDeDaño");
        areaDeDañoGO.transform.parent = transform;  // Hace que el área de daño sea hijo de la bomba
        areaDeDañoGO.transform.localPosition = Vector3.zero;

        // Añadir el collider de la esfera para el área de daño
        SphereCollider areaCollider = areaDeDañoGO.AddComponent<SphereCollider>();
        areaCollider.isTrigger = true;
        areaCollider.radius = radio; // Configura el radio de la zona de daño

        // Añadir el script que maneja el daño en el área
        AreaDeDaño areaScript = areaDeDañoGO.AddComponent<AreaDeDaño>();
        areaScript.dañoPorSegundos = dañoPorSegundo;

        // Iniciar la destrucción automática después de la duración especificada
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    // Corutina para destruir la bomba y el área de daño después del tiempo especificado
    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(duracion);  // Espera la duración

        // Destruye el área de daño (si existe)
        if (areaDeDañoGO != null)
        {
            Destroy(areaDeDañoGO);  // Destruye el área de daño
        }

        // Destruye la bomba (el objeto principal)
        Destroy(gameObject);  // Destruye la bomba
    }
}


