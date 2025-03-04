using UnityEngine;

public class EscaladoObjeto2 : MonoBehaviour
{
    private bool modoEscalarActivo = false;  // Estado de escalado (solo activable con el botón de la UI)
    private GameObject objetoSeleccionado;   // Objeto actualmente en escalado
    private float velocidadEscalado = 2f;    // Velocidad de escalado con el movimiento del ratón
    private float sensibilidadRaton = 0.1f;  // Sensibilidad al mover el ratón

    private Vector3 escalaMinima = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 escalaMaxima = new Vector3(3f, 3f, 3f);

    void Update()
    {
        // SOLO FUNCIONA EL ESCALADO SI SE ACTIVÓ CON EL BOTÓN DE LA UI
        if (!modoEscalarActivo)
            return; // No hace nada si el modo escalar no ha sido activado desde la UI

        // Permitir seleccionar un objeto con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("seleccion"))
                {
                    objetoSeleccionado = hit.transform.gameObject;
                    Debug.Log("Objeto seleccionado para escalar: " + objetoSeleccionado.name);
                }
            }
        }

        // Si hay un objeto seleccionado, escalar con el movimiento del ratón mientras se mantiene presionado el botón derecho
        if (modoEscalarActivo && objetoSeleccionado != null && Input.GetMouseButton(1)) // MouseButton(1) → Botón derecho
        {
            float deltaEscala = Input.GetAxis("Mouse Y") * velocidadEscalado * sensibilidadRaton;
            if (Mathf.Abs(deltaEscala) > 0) // Solo escalar si el ratón se mueve
            {
                Vector3 nuevaEscala = objetoSeleccionado.transform.localScale + new Vector3(deltaEscala, deltaEscala, deltaEscala);

                // Aplicar límites de escala
                nuevaEscala = new Vector3(
                    Mathf.Clamp(nuevaEscala.x, escalaMinima.x, escalaMaxima.x),
                    Mathf.Clamp(nuevaEscala.y, escalaMinima.y, escalaMaxima.y),
                    Mathf.Clamp(nuevaEscala.z, escalaMinima.z, escalaMaxima.z)
                );

                objetoSeleccionado.transform.localScale = nuevaEscala;
            }
        }
    }

    // Método para activar el modo escalar desde el botón de la UI
    public void ActivarModoEscalar()
    {
        modoEscalarActivo = true;
        Debug.Log("Modo escalar activado. Haz clic en un objeto y mantén presionado el botón derecho mientras mueves el ratón arriba/abajo.");
    }

    // Método para desactivar el modo escalar manualmente
    public void DesactivarModoEscalar()
    {
        modoEscalarActivo = false;
        objetoSeleccionado = null;
        Debug.Log("Modo escalar desactivado.");
    }
}
