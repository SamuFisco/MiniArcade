using UnityEngine;

public class MoverConRaton : MonoBehaviour
{
    private GameObject objetoSeleccionado; // Objeto actualmente seleccionado
    private Plane planoMovimiento; // Plano de movimiento en el espacio XZ
    private bool moviendoObjeto = false; // Indica si estamos moviendo un objeto
    public GameObject sombraObjeto; // Indica si estamos moviendo la sombra 
    // sombraObjeto = new GameObject(); // Crea el Objeto Sobra

    private void Start()
    {
        // Inicializamos el plano de movimiento en el espacio XZ (Y = 0)
        planoMovimiento = new Plane(Vector3.up, Vector3.zero);
        sombraObjeto = new GameObject("SombraObjeto"); // Crea el Objeto Sombra
        
    }
    
   

    private void Update()
    {
        // Detectar clic izquierdo para seleccionar o empezar a mover un objeto
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Detectar colisión con objetos que tengan la etiqueta "seleccion"
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("seleccion"))
            {
                objetoSeleccionado = hit.transform.gameObject; // Asignar el objeto seleccionado
                moviendoObjeto = true; // Activar el movimiento
                sombraObjeto = hit.transform.gameObject; // Asigna el objeto en el inspector que esta publico
               
                sombraObjeto.SetActive(sombraObjeto);
                
            }
        }

        // Si estamos moviendo un objeto, actualizar su posición
        if (moviendoObjeto && objetoSeleccionado != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Calcular la intersección del rayo con el plano de movimiento
            if (planoMovimiento.Raycast(ray, out float distancia))
            {
                Vector3 puntoInterseccion = ray.GetPoint(distancia); // Obtener punto de intersección
                objetoSeleccionado.transform.position = puntoInterseccion; // Actualizar posición del objeto
                sombraObjeto.transform.position = puntoInterseccion;
            }
        }

        // Detectar cuando se suelta el botón izquierdo del ratón
        if (Input.GetMouseButtonUp(0) && moviendoObjeto)
        {
            moviendoObjeto = false; // Desactivar el movimiento
            objetoSeleccionado = null; // Limpiar la referencia al objeto
            sombraObjeto = null; 
        }
    }
}
