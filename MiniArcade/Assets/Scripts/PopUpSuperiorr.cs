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

    [Header("Animaci�n")]
    public float duracionAnimacion = 0.5f; // Duraci�n de la animaci�n

    private Vector2 posicionOculta = new Vector2(0, 600); // Posici�n fuera de la pantalla
    private Vector2 posicionVisible = new Vector2(1, 300); // Posici�n visible

    private void Start()
    {
        // Inicializar popups en posici�n oculta
        popupCrear.anchoredPosition = posicionOculta;
        popupMover.anchoredPosition = posicionOculta;
        popupRotar.anchoredPosition = posicionOculta;
        popupEliminar.anchoredPosition = posicionOculta;

        // Asignar listeners a los botones de abrir
        botonAbrirCrear.onClick.AddListener(() => MostrarPopup(popupCrear));
        botonAbrirMover.onClick.AddListener(() => MostrarPopup(popupMover));
        botonAbrirRotar.onClick.AddListener(() => MostrarPopup(popupRotar));
        botonAbrirEliminar.onClick.AddListener(() => MostrarPopup(popupEliminar));

        // Asignar listeners a los botones de cerrar
        botonCerrarCrear.onClick.AddListener(() => OcultarPopup(popupCrear));
        botonCerrarMover.onClick.AddListener(() => OcultarPopup(popupMover));
        botonCerrarRotar.onClick.AddListener(() => OcultarPopup(popupRotar));
        botonCerrarEliminar.onClick.AddListener(() => OcultarPopup(popupEliminar));
    }

    // Mostrar popup con animaci�n
    private void MostrarPopup(RectTransform popup)
    {
        LeanTween.moveY(popup, posicionVisible.y, duracionAnimacion).setEase(LeanTweenType.easeOutExpo);
    }

    // Ocultar popup con animaci�n
    private void OcultarPopup(RectTransform popup)
    {
        LeanTween.moveY(popup, posicionOculta.y, duracionAnimacion).setEase(LeanTweenType.easeInExpo);
    }
}
