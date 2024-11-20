using UnityEngine;

public class MoverConRaton : MonoBehaviour
{
    private GameObject objetoSeleccionado; // Objeto actualmente seleccionado
    private Plane planoMovimiento; // Plano de movimiento en el espacio XZ
    private bool moviendoObjeto = false; // Indica si estamos moviendo un objeto

    private void Start()
    {
        // Inicializamos el plano de movimiento en el espacio XZ (Y = 0)
        planoMovimiento = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        // Detectar clic izquierdo para seleccionar o empezar a mover un objeto
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Detectar colisi�n con objetos que tengan la etiqueta "seleccion"
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("seleccion"))
            {
                objetoSeleccionado = hit.transform.gameObject; // Asignar el objeto seleccionado
                moviendoObjeto = true; // Activar el movimiento
            }
        }

        // Si estamos moviendo un objeto, actualizar su posici�n
        if (moviendoObjeto && objetoSeleccionado != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Calcular la intersecci�n del rayo con el plano de movimiento
            if (planoMovimiento.Raycast(ray, out float distancia))
            {
                Vector3 puntoInterseccion = ray.GetPoint(distancia); // Obtener punto de intersecci�n
                objetoSeleccionado.transform.position = puntoInterseccion; // Actualizar posici�n del objeto
            }
        }

        // Detectar cuando se suelta el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonUp(0) && moviendoObjeto)
        {
            moviendoObjeto = false; // Desactivar el movimiento
            objetoSeleccionado = null; // Limpiar la referencia al objeto
        }
    }
}
