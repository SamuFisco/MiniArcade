using UnityEngine;
using UnityEngine.UI;  // Importar para trabajar con botones
using TMPro;

public class ControladorMenu : MonoBehaviour
{
    [Header("Elementos UI")] //Array Elementos UI
    public RectTransform panelMenu;  // El panel del menú inferior que se moverá
    public RectTransform panelMenuLateral;  // El panel del menú lateral que se moverá
    public Button botonAbrirMenu;     // Botón para abrir el menú (subir)
    public Button botonCerrarMenu; // Botón para cerrar el menú (bajar)
    public Button botonObjeto1; // Botón para crear un objeto en la escena (Objeto 1)
    public Button botonObjeto2; // Botón para crear un objeto en la escena (Objeto 2)
    public Button botonObjeto3; // Botón para crear un objeto en la escena (Objeto 3)
    public Button botonObjeto4; // Botón para crear un objeto en la escena (Objeto 4)
    public Button botonObjeto5; // Botón para crear un objeto en la escena (Objeto 5)
    public Button botonObjeto6; // Botón para crear un objeto en la escena (Objeto 6)
    public Button botonObjeto7; // Botón para crear un objeto en la escena (Objeto 7)
    public Button botonObjeto8; // Botón para crear un objeto en la escena (Objeto 8)
    public Button botonAnadirMenuLateral;     // Botón para abrir el menú lateral 
    public Button botonRotarObjeto;  // Botón para rotar el objeto
    public Button botonEliminarObjeto; // Botón para eliminar el objeto seleccionado
    public Button botonMoverObjeto; // Botón para activar el modo mover
    

    [Header("Prefabs")] //Array prefabs
    public GameObject sombraObjeto; // Sombra del Objeto en escena
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;
    public GameObject prefab7;
    public GameObject prefab8;

    public float duracionAnimacion = 0.5f;  // Duración de la animación
    public float duracionAnimacionCreacion = 0.5f; // Duración de la animación al crear el objeto
    public float duracionAnimacionEliminacion = 0.5f; // Duración de la animación al eliminar el objeto


    private GameObject objetoSeleccionado; // Referencia al objeto seleccionado
    private bool modoMoverActivo = false;  // Controla si el modo de mover está activo

    // Posiciones del menú inferior
    private Vector2 posicionMenuInferior = new Vector2(-3, -340);   // Posición inicial del menú (abajo)
    private Vector2 posicionMenuSuperior = new Vector2(-3, -242);  // Posición final del menú (arriba)

    private bool menuArriba = false;  // Controla si el menú inferior está en la posición superior

    // Posiciones del menú lateral
    private Vector2 posicionMenuLateralInicio = new Vector2(810, 38);   // Posición inicial del menú lateral (oculto)
    private Vector2 posicionMenuLateralFinal = new Vector2(321, 38);  // Posición final del menú lateral (visible)

    private bool menuDentro = false;  // Controla si el menú lateral está en la posición visible


    

    private void Start()
    {

        
        
        // Asegurarse de que el menú inferior empieza en la posición oculta
        panelMenu.anchoredPosition = posicionMenuInferior;

        // Asegurarse de que los botones están activos al inicio
        botonAbrirMenu.gameObject.SetActive(true);
        botonCerrarMenu.gameObject.SetActive(true);

        // Asegurarse de que el menú lateral empieza en la posición oculta
        panelMenuLateral.anchoredPosition = posicionMenuLateralInicio;

        // Asegurarse de que los botones están activos al inicio
        botonAnadirMenuLateral.gameObject.SetActive(true);

        // Agregar los listeners a los botones
       
        botonRotarObjeto.onClick.AddListener(RotarObjeto);
        botonEliminarObjeto.onClick.AddListener(EliminarObjeto);
        botonMoverObjeto.onClick.AddListener(ActivarModoMover);
        botonObjeto1.onClick.AddListener(() => CrearObjetoEnEscena(prefab1, 1));
        botonObjeto2.onClick.AddListener(() => CrearObjetoEnEscena(prefab2, 2));
        botonObjeto3.onClick.AddListener(() => CrearObjetoEnEscena(prefab3, 3));
        botonObjeto4.onClick.AddListener(() => CrearObjetoEnEscena(prefab4, 4));
        botonObjeto5.onClick.AddListener(() => CrearObjetoEnEscena(prefab5, 5));
        botonObjeto6.onClick.AddListener(() => CrearObjetoEnEscena(prefab6, 6));
        botonObjeto7.onClick.AddListener(() => CrearObjetoEnEscena(prefab7, 7));
        botonObjeto8.onClick.AddListener(() => CrearObjetoEnEscena(prefab8, 8));
    }

    private void Update()
    {
        // Detectar clic izquierdo del ratón sobre un objeto con la etiqueta "seleccion"
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("seleccion"))
                {
                    // Selecciona el objeto al hacer clic en él
                    objetoSeleccionado = hit.transform.gameObject;
                    Debug.Log("Objeto seleccionado: " + objetoSeleccionado.name);
                }
            }
        }

        // Movimiento del objeto seleccionado si el modo mover está activo
        if (modoMoverActivo && objetoSeleccionado != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                objetoSeleccionado.transform.position += Vector3.back * Time.deltaTime; // Mover hacia adelante (eje Z)
            }
            if (Input.GetKey(KeyCode.X))
            {
                objetoSeleccionado.transform.position += Vector3.forward * Time.deltaTime; // Mover hacia atrás (eje Z)
            }
            if (Input.GetKey(KeyCode.A))
            {
                objetoSeleccionado.transform.position += Vector3.right * Time.deltaTime; // Mover hacia la izquierda (eje X)
            }
            if (Input.GetKey(KeyCode.D))
            {
                objetoSeleccionado.transform.position += Vector3.left * Time.deltaTime; // Mover hacia la derecha (eje X)
            }
        }
    }

    // Función pública que puedes asignar a los botones desde el Inspector
    public void MoverMenu()
    {
        if (!menuArriba)
        {
            // Mover el menú inferior hacia la posición superior (arriba)
            LeanTween.move(panelMenu, posicionMenuSuperior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = true;  // Actualizamos el estado a arriba
        }
        else
        {
            // Mover el menú inferior hacia la posición inferior (abajo)
            LeanTween.move(panelMenu, posicionMenuInferior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = false;  // Actualizamos el estado a abajo
        }
    }

    // Función común para mover el menú lateral
    private void MoverMenuLateral()
    {
        if (!menuDentro)
        {
            // Mover el menú lateral hacia la posición visible
            LeanTween.move(panelMenuLateral, posicionMenuLateralFinal, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = true;  // Actualizamos el estado a visible
        }
        else
        {
            // Mover el menú lateral hacia la posición oculta
            LeanTween.move(panelMenuLateral, posicionMenuLateralInicio, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = false;  // Actualizamos el estado a oculto
        }
    }

    // Botón para añadir el menú lateral
    public void BotonAnadirMenuLateral()
    {
        MoverMenuLateral();
    }

    // Función para crear un objeto en la posición fija (0, 0, 0) y darle una etiqueta de "seleccion"
    private void CrearObjetoEnEscena(GameObject prefab, int objetoId)
    {
        if (prefab != null)
        {
            Vector3 posicionInicial = new Vector3(0, 1, 0);
            GameObject nuevoObjeto = Instantiate(prefab, posicionInicial, Quaternion.identity);
            nuevoObjeto.tag = "seleccion";
            nuevoObjeto.name = "Objeto" + objetoId;
         

            AnimarCreacion(nuevoObjeto);
            Debug.Log("Objeto creado: " + nuevoObjeto.name);
        }
        else
        {
            Debug.LogWarning("Prefab no asignado para el objeto " + objetoId);
        }

    }
   
    public void AnimarCreacion(GameObject objeto)
    {
        if (objeto != null)
        {
            objeto.transform.localScale = Vector3.zero;
            LeanTween.scale(objeto, Vector3.one, duracionAnimacionCreacion).setEase(LeanTweenType.easeOutBounce);
        }
    }

    // Función para rotar el objeto en el eje Y
    private void RotarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            objetoSeleccionado.transform.Rotate(Vector3.up, 90f);  // Rotar 45 grados en el eje Y
        }
    }

    // Función para eliminar el objeto seleccionado
    private void EliminarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            AnimarEliminacion(objetoSeleccionado);  // Llamar a la animación de eliminación
            objetoSeleccionado = null;  // Limpiar la referencia
        }
    }

    public void AnimarEliminacion(GameObject objeto)
    {
        if (objeto != null)
        {
            LeanTween.scale(objeto, Vector3.zero, duracionAnimacionEliminacion)
                     .setEase(LeanTweenType.easeInBack) // Tipo de easing para suavizar la animación
                     .setOnComplete(() => Destroy(objeto));  // Destruir el objeto al terminar la animación
        }
    }


    // Función para activar el modo de mover
    private void ActivarModoMover()
    {
        modoMoverActivo = true;
        Debug.Log("Modo mover activado");
    }
}

