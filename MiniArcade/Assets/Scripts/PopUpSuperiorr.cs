using UnityEngine;
using UnityEngine.UI;

public class PopUpSuperior : MonoBehaviour
{
    [Header("PopUps")]
    public RectTransform popupCrear;
    public RectTransform popupMover;
    public RectTransform popupRotar;
    public RectTransform popupEliminar;

    [Header("Botones Abrir")]
    public Button botonAbrirCrear;
    public Button botonAbrirMover;
    public Button botonAbrirRotar;
    public Button botonAbrirEliminar;

    [Header("Botones Cerrar")]
    public Button botonCerrarCrear;
    public Button botonCerrarMover;
    public Button botonCerrarRotar;
    public Button botonCerrarEliminar;

    [Header("Animación")]
    public float duracionAnimacion = 0.5f; // Duración de la animación

    private Vector2 posicionOculta = new Vector2(-140, 346); // Posición fuera de la pantalla
    private Vector2 posicionVisible = new Vector2(-140, 178); // Posición visible

    private void Start()
    {
        // Inicializar popups en posición oculta
        popupCrear.anchoredPosition = posicionOculta;
        popupMover.anchoredPosition = posicionOculta;
        popupRotar.anchoredPosition = posicionOculta;
        popupEliminar.anchoredPosition = posicionOculta;

        // Asignar listeners a los botones de abrir
        botonAbrirCrear.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupCrear));
        botonAbrirMover.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupMover));
        botonAbrirRotar.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupRotar));
        botonAbrirEliminar.onClick.AddListener(() => MostrarPopupConAutoCerrar(popupEliminar));

        // Asignar listeners a los botones de cerrar
        botonCerrarCrear.onClick.AddListener(() => OcultarPopup(popupCrear));
        botonCerrarMover.onClick.AddListener(() => OcultarPopup(popupMover));
        botonCerrarRotar.onClick.AddListener(() => OcultarPopup(popupRotar));
        botonCerrarEliminar.onClick.AddListener(() => OcultarPopup(popupEliminar));
    }

    // Mostrar popup con animación
    private void MostrarPopup(RectTransform popup)
    {
        LeanTween.moveY(popup, posicionVisible.y, duracionAnimacion).setEase(LeanTweenType.easeOutExpo);
    }

    // Mostrar popup con cierre automático
    private void MostrarPopupConAutoCerrar(RectTransform popup)
    {
        MostrarPopup(popup);
        StartCoroutine(CerrarPopupDespuesDeTiempo(popup, 2f)); // 2 segundos
    }

    // Ocultar popup con animación
    private void OcultarPopup(RectTransform popup)
    {
        LeanTween.moveY(popup, posicionOculta.y, duracionAnimacion).setEase(LeanTweenType.easeInExpo);
    }

    // Corutina para cerrar el popup después de un tiempo
    private System.Collections.IEnumerator CerrarPopupDespuesDeTiempo(RectTransform popup, float tiempo)
    {
        yield return new WaitForSeconds(tiempo); // Esperar el tiempo especificado
        OcultarPopup(popup); // Ocultar el popup
    }
}
