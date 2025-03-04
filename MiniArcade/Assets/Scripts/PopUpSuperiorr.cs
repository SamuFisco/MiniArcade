using UnityEngine;  // Importamos UnityEngine para manipular GameObjects y RectTransform
using UnityEngine.UI;  // Importamos UnityEngine.UI para trabajar con botones e interfaces gr�ficas

public class PopUpSuperior : MonoBehaviour
{
    [Header("PopUps")]  // Agrupar en el Inspector los elementos de los PopUps
    public RectTransform popupCrear;  // Referencia al popup de "Crear"
    public RectTransform popupMover;  // Referencia al popup de "Mover"
    public RectTransform popupRotar;  // Referencia al popup de "Rotar"
    public RectTransform popupEliminar;  // Referencia al popup de "Eliminar"

    [Header("Botones Abrir")]  // Agrupar los botones que abren los popups
    public Button botonAbrirCrear;  // Bot�n para abrir el popup "Crear"
    public Button botonAbrirMover;  // Bot�n para abrir el popup "Mover"
    public Button botonAbrirRotar;  // Bot�n para abrir el popup "Rotar"
    public Button botonAbrirEliminar;  // Bot�n para abrir el popup "Eliminar"

    [Header("Botones Cerrar")]  // Agrupar los botones que cierran los popups
    public Button botonCerrarCrear;  // Bot�n para cerrar el popup "Crear"
    public Button botonCerrarMover;  // Bot�n para cerrar el popup "Mover"
    public Button botonCerrarRotar;  // Bot�n para cerrar el popup "Rotar"
    public Button botonCerrarEliminar;  // Bot�n para cerrar el popup "Eliminar"

    [Header("Animaci�n")]  // Agrupar configuraciones de animaci�n
    public float duracionAnimacion = 0.5f;  // Duraci�n en segundos de la animaci�n de apertura/cierre

    // Posiciones predefinidas para mostrar/ocultar los popups
    private Vector2 posicionOculta = new Vector2(-140, 631);  // Posici�n fuera de la pantalla (oculto)
    private Vector2 posicionVisible = new Vector2(-140, 384);  // Posici�n dentro de la pantalla (visible)

    private void Start()
    {
        // Inicializar los popups en la posici�n oculta
        popupCrear.anchoredPosition = posicionOculta;
        popupMover.anchoredPosition = posicionOculta;
        popupRotar.anchoredPosition = posicionOculta;
        popupEliminar.anchoredPosition = posicionOculta;

        // Asignar eventos a los botones para abrir popups con cierre autom�tico
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
    /// Muestra el popup movi�ndolo a la posici�n visible con animaci�n.
    /// </summary>
    /// <param name="popup">El popup que se va a mostrar.</param>
    private void MostrarPopup(RectTransform popup)
    {
        // Mover el popup a su posici�n visible con una animaci�n suave
        LeanTween.moveY(popup, posicionVisible.y, duracionAnimacion).setEase(LeanTweenType.easeOutExpo);
    }

    /// <summary>
    /// Muestra el popup y lo cierra autom�ticamente despu�s de un tiempo.
    /// </summary>
    /// <param name="popup">El popup que se va a mostrar.</param>
    private void MostrarPopupConAutoCerrar(RectTransform popup)
    {
        MostrarPopup(popup);  // Mostrar el popup con animaci�n
        StartCoroutine(CerrarPopupDespuesDeTiempo(popup, 2f));  // Programar el cierre despu�s de 2 segundos
    }

    /// <summary>
    /// Oculta el popup movi�ndolo fuera de la pantalla con animaci�n.
    /// </summary>
    /// <param name="popup">El popup que se va a ocultar.</param>
    private void OcultarPopup(RectTransform popup)
    {
        // Mover el popup a su posici�n oculta con una animaci�n suave
        LeanTween.moveY(popup, posicionOculta.y, duracionAnimacion).setEase(LeanTweenType.easeInExpo);
    }

    /// <summary>
    /// Espera un tiempo determinado antes de ocultar el popup autom�ticamente.
    /// </summary>
    /// <param name="popup">El popup que se va a ocultar.</param>
    /// <param name="tiempo">Tiempo en segundos antes de que el popup se cierre.</param>
    /// <returns>IEnumerator para la corutina.</returns>
    private System.Collections.IEnumerator CerrarPopupDespuesDeTiempo(RectTransform popup, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);  // Esperar el tiempo especificado antes de continuar
        OcultarPopup(popup);  // Ocultar el popup despu�s de esperar
    }
}
