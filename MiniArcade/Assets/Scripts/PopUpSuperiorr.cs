using UnityEngine;  // Importamos UnityEngine para manipular GameObjects y RectTransform
using UnityEngine.UI;  // Importamos UnityEngine.UI para trabajar con botones e interfaces gráficas

public class PopUpSuperior : MonoBehaviour
{
    [Header("PopUps")]  // Agrupar en el Inspector los elementos de los PopUps
    public RectTransform popupCrear;  // Referencia al popup de "Crear"
    public RectTransform popupMover;  // Referencia al popup de "Mover"
    public RectTransform popupRotar;  // Referencia al popup de "Rotar"
    public RectTransform popupEliminar;  // Referencia al popup de "Eliminar"

    [Header("Botones Abrir")]  // Agrupar los botones que abren los popups
    public Button botonAbrirCrear;  // Botón para abrir el popup "Crear"
    public Button botonAbrirMover;  // Botón para abrir el popup "Mover"
    public Button botonAbrirRotar;  // Botón para abrir el popup "Rotar"
    public Button botonAbrirEliminar;  // Botón para abrir el popup "Eliminar"

    [Header("Botones Cerrar")]  // Agrupar los botones que cierran los popups
    public Button botonCerrarCrear;  // Botón para cerrar el popup "Crear"
    public Button botonCerrarMover;  // Botón para cerrar el popup "Mover"
    public Button botonCerrarRotar;  // Botón para cerrar el popup "Rotar"
    public Button botonCerrarEliminar;  // Botón para cerrar el popup "Eliminar"

    [Header("Animación")]  // Agrupar configuraciones de animación
    public float duracionAnimacion = 0.5f;  // Duración en segundos de la animación de apertura/cierre

    // Posiciones predefinidas para mostrar/ocultar los popups
    private Vector2 posicionOculta = new Vector2(-140, 631);  // Posición fuera de la pantalla (oculto)
    private Vector2 posicionVisible = new Vector2(-140, 384);  // Posición dentro de la pantalla (visible)

    private void Start()
    {
        // Inicializar los popups en la posición oculta
        popupCrear.anchoredPosition = posicionOculta;
        popupMover.anchoredPosition = posicionOculta;
        popupRotar.anchoredPosition = posicionOculta;
        popupEliminar.anchoredPosition = posicionOculta;

        // Asignar eventos a los botones para abrir popups con cierre automático
        botonAbrirCrear.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupCrear));
        botonAbrirMover.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupMover));
        botonAbrirRotar.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupRotar));
        botonAbrirEliminar.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupEliminar));

        // Asignar eventos a los botones para cerrar manualmente los popups
        botonCerrarCrear.onClick.AddListener(() => OcultarPopup(popupCrear));
        botonCerrarMover.onClick.AddListener(() => OcultarPopup(popupMover));
        botonCerrarRotar.onClick.AddListener(() => OcultarPopup(popupRotar));
        botonCerrarEliminar.onClick.AddListener(() => OcultarPopup(popupEliminar));
    }

    /// <summary>
    /// Muestra el popup moviéndolo a la posición visible con animación.
    /// </summary>
    /// <param name="popup">El popup que se va a mostrar.</param>
    private void MostrarPopup(RectTransform popup)
    {
        // Mover el popup a su posición visible con una animación suave
        LeanTween.moveY(popup, posicionVisible.y, duracionAnimacion).setEase(LeanTweenType.easeOutExpo);
    }

    /// <summary>
    /// Muestra el popup y lo cierra automáticamente después de un tiempo.
    /// </summary>
    /// <param name="popup">El popup que se va a mostrar.</param>
    private void MostrarPopupConAutoCerrar(RectTransform popup)
    {
        MostrarPopup(popup);  // Mostrar el popup con animación
        StartCoroutine(CerrarPopupDespuesDeTiempo(popup, 2f));  // Programar el cierre después de 2 segundos
    }

    /// <summary>
    /// Oculta el popup moviéndolo fuera de la pantalla con animación.
    /// </summary>
    /// <param name="popup">El popup que se va a ocultar.</param>
    private void OcultarPopup(RectTransform popup)
    {
        // Mover el popup a su posición oculta con una animación suave
        LeanTween.moveY(popup, posicionOculta.y, duracionAnimacion).setEase(LeanTweenType.easeInExpo);
    }

    /// <summary>
    /// Espera un tiempo determinado antes de ocultar el popup automáticamente.
    /// </summary>
    /// <param name="popup">El popup que se va a ocultar.</param>
    /// <param name="tiempo">Tiempo en segundos antes de que el popup se cierre.</param>
    /// <returns>IEnumerator para la corutina.</returns>
    private System.Collections.IEnumerator CerrarPopupDespuesDeTiempo(RectTransform popup, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);  // Esperar el tiempo especificado antes de continuar
        OcultarPopup(popup);  // Ocultar el popup después de esperar
    }
}
