using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class BombaComportamiento : MonoBehaviour
{
    public float duracion = 5f;              // Duraci�n de 5 segundos
    public float radio = 5f;                 // Radio de da�o
    public float da�oPorSegundo = 10f;       // Da�o por segundo en el �rea

    private bool haExplotado = false;        // Para verificar si ya ha explotado
    private GameObject areaDeDa�oGO;         // Referencia al �rea de da�o

    // M�todo para configurar la bomba
    public void Configurar(float radio, float da�oPorSegundo, float duracion)
    {
        this.radio = radio;
        this.da�oPorSegundo = da�oPorSegundo;
        this.duracion = duracion;
    }

    // Detecta el impacto con otros objetos
    private void OnCollisionEnter(Collision collision)
    {
        if (!haExplotado)
        {
            haExplotado = true;  // Marca que la bomba ya explot�
            ActivarAreaDeDa�o(); // Activa el �rea de da�o
        }
    }

    // Activa el �rea de da�o cuando la bomba explota
    private void ActivarAreaDeDa�o()
    {
        // Crear un objeto hijo para representar el �rea de da�o
        areaDeDa�oGO = new GameObject("AreaDeDa�o");
        areaDeDa�oGO.transform.parent = transform;  // Hace que el �rea de da�o sea hijo de la bomba
        areaDeDa�oGO.transform.localPosition = Vector3.zero;

        // A�adir el collider de la esfera para el �rea de da�o
        SphereCollider areaCollider = areaDeDa�oGO.AddComponent<SphereCollider>();
        areaCollider.isTrigger = true;
        areaCollider.radius = radio; // Configura el radio de la zona de da�o

        // A�adir el script que maneja el da�o en el �rea
        AreaDeDa�o areaScript = areaDeDa�oGO.AddComponent<AreaDeDa�o>();
        areaScript.da�oPorSegundos = da�oPorSegundo;

        // Iniciar la destrucci�n autom�tica despu�s de la duraci�n especificada
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    // Corutina para destruir la bomba y el �rea de da�o despu�s del tiempo especificado
    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(duracion);  // Espera la duraci�n

        // Destruye el �rea de da�o (si existe)
        if (areaDeDa�oGO != null)
        {
            Destroy(areaDeDa�oGO);  // Destruye el �rea de da�o
        }

        // Destruye la bomba (el objeto principal)
        Destroy(gameObject);  // Destruye la bomba
    }
}


