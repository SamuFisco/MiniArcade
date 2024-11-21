using UnityEngine;
using UnityEngine.UI;  // Importar para trabajar con botones
using TMPro;

public class ControladorMenu : MonoBehaviour
{
    [Header("Elementos UI")] //Array Elementos UI
    public RectTransform panelMenu;  // El panel del men� inferior que se mover�
    public RectTransform panelMenuLateral;  // El panel del men� lateral que se mover�
    public Button botonAbrirMenu;     // Bot�n para abrir el men� (subir)
    public Button botonCerrarMenu; // Bot�n para cerrar el men� (bajar)
    public Button botonObjeto1; // Bot�n para crear un objeto en la escena (Objeto 1)
    public Button botonObjeto2; // Bot�n para crear un objeto en la escena (Objeto 2)
    public Button botonObjeto3; // Bot�n para crear un objeto en la escena (Objeto 3)
    public Button botonObjeto4; // Bot�n para crear un objeto en la escena (Objeto 4)
    public Button botonObjeto5; // Bot�n para crear un objeto en la escena (Objeto 5)
    public Button botonObjeto6; // Bot�n para crear un objeto en la escena (Objeto 6)
    public Button botonObjeto7; // Bot�n para crear un objeto en la escena (Objeto 7)
    public Button botonObjeto8; // Bot�n para crear un objeto en la escena (Objeto 8)
    public Button botonAnadirMenuLateral;     // Bot�n para abrir el men� lateral 
    public Button botonRotarObjeto;  // Bot�n para rotar el objeto
    public Button botonEliminarObjeto; // Bot�n para eliminar el objeto seleccionado
    public Button botonMoverObjeto; // Bot�n para activar el modo mover
    

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

    public float duracionAnimacion = 0.5f;  // Duraci�n de la animaci�n
    public float duracionAnimacionCreacion = 0.5f; // Duraci�n de la animaci�n al crear el objeto
    public float duracionAnimacionEliminacion = 0.5f; // Duraci�n de la animaci�n al eliminar el objeto


    private GameObject objetoSeleccionado; // Referencia al objeto seleccionado
    private bool modoMoverActivo = false;  // Controla si el modo de mover est� activo

    // Posiciones del men� inferior
    private Vector2 posicionMenuInferior = new Vector2(-3, -340);   // Posici�n inicial del men� (abajo)
    private Vector2 posicionMenuSuperior = new Vector2(-3, -242);  // Posici�n final del men� (arriba)

    private bool menuArriba = false;  // Controla si el men� inferior est� en la posici�n superior

    // Posiciones del men� lateral
    private Vector2 posicionMenuLateralInicio = new Vector2(810, 38);   // Posici�n inicial del men� lateral (oculto)
    private Vector2 posicionMenuLateralFinal = new Vector2(321, 38);  // Posici�n final del men� lateral (visible)

    private bool menuDentro = false;  // Controla si el men� lateral est� en la posici�n visible


    

    private void Start()
    {

        
        
        // Asegurarse de que el men� inferior empieza en la posici�n oculta
        panelMenu.anchoredPosition = posicionMenuInferior;

        // Asegurarse de que los botones est�n activos al inicio
        botonAbrirMenu.gameObject.SetActive(true);
        botonCerrarMenu.gameObject.SetActive(true);

        // Asegurarse de que el men� lateral empieza en la posici�n oculta
        panelMenuLateral.anchoredPosition = posicionMenuLateralInicio;

        // Asegurarse de que los botones est�n activos al inicio
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
        // Detectar clic izquierdo del rat�n sobre un objeto con la etiqueta "seleccion"
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("seleccion"))
                {
                    // Selecciona el objeto al hacer clic en �l
                    objetoSeleccionado = hit.transform.gameObject;
                    Debug.Log("Objeto seleccionado: " + objetoSeleccionado.name);
                }
            }
        }

        // Movimiento del objeto seleccionado si el modo mover est� activo
        if (modoMoverActivo && objetoSeleccionado != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                objetoSeleccionado.transform.position += Vector3.back * Time.deltaTime; // Mover hacia adelante (eje Z)
            }
            if (Input.GetKey(KeyCode.X))
            {
                objetoSeleccionado.transform.position += Vector3.forward * Time.deltaTime; // Mover hacia atr�s (eje Z)
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

    // Funci�n p�blica que puedes asignar a los botones desde el Inspector
    public void MoverMenu()
    {
        if (!menuArriba)
        {
            // Mover el men� inferior hacia la posici�n superior (arriba)
            LeanTween.move(panelMenu, posicionMenuSuperior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = true;  // Actualizamos el estado a arriba
        }
        else
        {
            // Mover el men� inferior hacia la posici�n inferior (abajo)
            LeanTween.move(panelMenu, posicionMenuInferior, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuArriba = false;  // Actualizamos el estado a abajo
        }
    }

    // Funci�n com�n para mover el men� lateral
    private void MoverMenuLateral()
    {
        if (!menuDentro)
        {
            // Mover el men� lateral hacia la posici�n visible
            LeanTween.move(panelMenuLateral, posicionMenuLateralFinal, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = true;  // Actualizamos el estado a visible
        }
        else
        {
            // Mover el men� lateral hacia la posici�n oculta
            LeanTween.move(panelMenuLateral, posicionMenuLateralInicio, duracionAnimacion).setEase(LeanTweenType.easeInElastic);
            menuDentro = false;  // Actualizamos el estado a oculto
        }
    }

    // Bot�n para a�adir el men� lateral
    public void BotonAnadirMenuLateral()
    {
        MoverMenuLateral();
    }

    // Funci�n para crear un objeto en la posici�n fija (0, 0, 0) y darle una etiqueta de "seleccion"
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

    // Funci�n para rotar el objeto en el eje Y
    private void RotarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            objetoSeleccionado.transform.Rotate(Vector3.up, 90f);  // Rotar 45 grados en el eje Y
        }
    }

    // Funci�n para eliminar el objeto seleccionado
    private void EliminarObjeto()
    {
        if (objetoSeleccionado != null)
        {
            AnimarEliminacion(objetoSeleccionado);  // Llamar a la animaci�n de eliminaci�n
            objetoSeleccionado = null;  // Limpiar la referencia
        }
    }

    public void AnimarEliminacion(GameObject objeto)
    {
        if (objeto != null)
        {
            LeanTween.scale(objeto, Vector3.zero, duracionAnimacionEliminacion)
                     .setEase(LeanTweenType.easeInBack) // Tipo de easing para suavizar la animaci�n
                     .setOnComplete(() => Destroy(objeto));  // Destruir el objeto al terminar la animaci�n
        }
    }


    // Funci�n para activar el modo de mover
    private void ActivarModoMover()
    {
        modoMoverActivo = true;
        Debug.Log("Modo mover activado");
    }
}

