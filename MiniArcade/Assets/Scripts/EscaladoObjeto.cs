using UnityEngine;

public class EscaladoObjeto : MonoBehaviour
{
    private bool modoEscalarActivo = false;  // Estado de escalado
    private GameObject objetoSeleccionado;   // Objeto actualmente en escalado
    private float velocidadEscalado = 0.2f;  // Velocidad de escalado con la rueda del ratón

    private Vector3 escalaMinima = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 escalaMaxima = new Vector3(3f, 3f, 3f);

    void Update()
    {
        // Si el modo escalar está activo, el jugador debe hacer clic en un objeto para seleccionarlo
        if (modoEscalarActivo && objetoSeleccionado == null && Input.GetMouseButtonDown(0))
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

        // Si hay un objeto seleccionado, escalar con la rueda del ratón
        if (modoEscalarActivo && objetoSeleccionado != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel"); // Detectar la rueda del ratón
            if (scroll != 0)
            {
                Vector3 nuevaEscala = objetoSeleccionado.transform.localScale + new Vector3(scroll, scroll, scroll) * velocidadEscalado;

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
        objetoSeleccionado = null; // Restablecer la selección
        Debug.Log("Modo escalar activado. Haz clic en un objeto y usa la rueda del ratón.");
    }

    // Método para desactivar el modo escalar
    public void DesactivarModoEscalar()
    {
        modoEscalarActivo = false;
        objetoSeleccionado = null;
        Debug.Log("Modo escalar desactivado.");
    }
}
