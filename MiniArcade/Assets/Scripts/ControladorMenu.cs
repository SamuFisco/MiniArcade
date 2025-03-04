using UnityEngine;  // Importamos UnityEngine para manejar objetos en la escena
using UnityEngine.UI;  // Importamos UnityEngine.UI para manejar botones y elementos UI

public class ControladorMenu : MonoBehaviour
{
    [Header("Elementos UI")] // Sección de elementos de la interfaz de usuario
    public RectTransform panelMenu;  // Referencia al panel del menú inferior
    public RectTransform panelMenuLateral;  // Referencia al panel del menú lateral
    public Button botonAbrirMenu;  // Botón para abrir el menú inferior
    public Button botonCerrarMenu;  // Botón para cerrar el menú inferior
    public Button botonObjeto1, botonObjeto2, botonObjeto3, botonObjeto4;  // Botones para crear objetos
    public Button botonObjeto5, botonObjeto6, botonObjeto7, botonObjeto8;  // Más botones para crear objetos
    public Button botonAnadirMenuLateral;  // Botón para abrir el menú lateral
    public Button botonRotarObjeto;  // Botón para rotar el objeto seleccionado
    public Button botonEliminarObjeto;  // Botón para eliminar el objeto seleccionado
    public Button botonMoverObjeto;  // Botón para activar el modo mover

    [Header("Prefabs")] // Sección de prefabs de objetos en la escena
    public GameObject prefab1, prefab2, prefab3, prefab4;  // Prefabs de los objetos 1-4
    public GameObject prefab5, prefab6, prefab7, prefab8;  // Prefabs de los objetos 5-8

    public float duracionAnimacion = 0.5f;  // Duración de las animaciones de UI
    public float duracionAnimacionEliminacion = 0.5f;  // Duración de la animación al eliminar un objeto

    private GameObject objetoSeleccionado;  // Referencia al objeto actualmente seleccionado
    private bool modoMoverActivo = false;  // Estado para activar el modo mover

    [Header("Animaciones de Creación")]
    public AnimacionCreacionObjeto animador;  // Referencia al script de animación del círculo

    private Vector2 posicionMenuInferior = new Vector2(-3, -622);  // Posición inicial del menú inferior
    private Vector2 posicionMenuSuperior = new Vector2(-3, -242);  // Posición cuando el menú está arriba
    private bool menuArriba = false;  // Estado del menú inferior

    private Vector2 posicionMenuLateralInicio = new Vector2(1101, 38);  // Posición inicial del menú lateral (oculto)
    private Vector2 posicionMenuLateralFinal = new Vector2(771, 38);  // Posición cuando el menú lateral está visible
    private bool menuDentro = false;  // Estado del menú lateral

    private void Start()
    {
        // Inicializar el menú inferior en su posición oculta
        panelMenu.anchoredPosition = posicionMenuInferior;
        botonAbrirMenu.gameObject.SetActive(true);
        botonCerrarMenu.gameObject.SetActive(true);

        // Inicializar el menú lateral en su posición oculta
        panelMenuLateral.anchoredPosition = posicionMenuLateralInicio;
        botonAnadirMenuLateral.gameObject.SetActive(true);

        // Asignar eventos a botones para crear objetos
        botonObjeto1.onClick.AddListener(() => CrearObjetoEnEscena(prefab1, 1));
        botonObjeto2.onClick.AddListener(() => CrearObjetoEnEscena(prefab2, 2));
        botonObjeto3.onClick.AddListener(() => CrearObjetoEnEscena(prefab3, 3));
        botonObjeto4.onClick.AddListener(() => CrearObjetoEnEscena(prefab4, 4));
        botonObjeto5.onClick.AddListener(() => CrearObjetoEnEscena(prefab5, 5));
        botonObjeto6.onClick.AddListener(() => CrearObjetoEnEscena(prefab6, 6));
        botonObjeto7.onClick.AddListener(() => CrearObjetoEnEscena(prefab7, 7));
        botonObjeto8.onClick.AddListener(() => CrearObjetoEnEscena(prefab8, 8));

        // Asignar eventos a botones de acciones
        botonRotarObjeto.onClick.AddListener(RotarObjeto);
        botonEliminarObjeto.onClick.AddListener(EliminarObjeto);
        botonMoverObjeto.onClick.AddListener(ActivarModoMover);
    }

    private void Update()
    {
        // Detectar clic izquierdo para seleccionar un objeto
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("seleccion"))  // Solo objetos con la etiqueta "seleccion"
                {
                    // Si ya había un objeto seleccionado, ocultar el círculo
                    if (objetoSeleccionado != null)
                    {
                        animador.OcultarCirculo();
                    }

                    // Asignar el nuevo objeto seleccionado
                    objetoSeleccionado = hit.transform.gameObject;
                    animador.MostrarCirculo(objetoSeleccionado);  // Mostrar el círculo debajo del objeto
                    Debug.Log("Objeto seleccionado: " + objetoSeleccionado.name);
                }
            }
        }
    }

    // Alternar la posición del menú inferior
    public void MoverMenu()
    {
        if (!menuArriba)
        {
            LeanTween.move(panelMenu, posicionMenuSuperior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = true;
        }
        else
        {
            LeanTween.move(panelMenu, posicionMenuInferior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = false;
        }
    }

    // Alternar la visibilidad del menú lateral
    private void MoverMenuLateral()
    {
        if (!menuDentro)
        {
            LeanTween.move(panelMenuLateral, posicionMenuLateralFinal, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = true;
        }
        else
        {
            LeanTween.move(panelMenuLateral, posicionMenuLateralInicio, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = false;
        }
    }

    // Activar el menú lateral con botón
    public void BotonAnadirMenuLateral()
    {
        MoverMenuLateral();
    }

    // Crear un objeto en la escena
    private void CrearObjetoEnEscena(GameObject prefab, int objetoId)
    {
        if (prefab != null)
        {
            Vector3 posicionInicial = new Vector3(0, 1, 0);  // Posición fija donde se crean los objetos
            GameObject nuevoObjeto = Instantiate(prefab, posicionInicial, Quaternion.identity);
            nuevoObjeto.tag = "seleccion";  // Asignar la etiqueta de selección
            nuevoObjeto.name = "Objeto" + objetoId;

            if (animador != null)
            {
                animador.MostrarCirculo(nuevoObjeto);  // Mostrar el círculo debajo del objeto
            }
            else
            {
                Debug.LogWarning("Animador no asignado en el Inspector.");
            }

            Debug.Log("Objeto creado: " + nuevoObjeto.name);
        }
    }

    // Rotar el objeto seleccionado
    private void RotarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            objetoSeleccionado.transform.Rotate(Vector3.up, 90f);  // Rotar 90 grados en el eje Y
            Debug.Log("Objeto rotado: " + objetoSeleccionado.name);
        }
    }

    // Eliminar el objeto seleccionado
    private void EliminarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            animador.OcultarCirculo();
            LeanTween.scale(objetoSeleccionado, Vector3.zero, duracionAnimacionEliminacion)
                .setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => Destroy(objetoSeleccionado));

            Debug.Log("Objeto eliminado: " + objetoSeleccionado.name);
            objetoSeleccionado = null;
        }
    }

    // Activar el modo de mover
    private void ActivarModoMover()
    {
        modoMoverActivo = true;
        Debug.Log("Modo mover activado");
    }
}

