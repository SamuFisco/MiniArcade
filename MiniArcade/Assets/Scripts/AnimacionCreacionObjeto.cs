using UnityEngine;  // Importamos UnityEngine para usar GameObjects y LeanTween

public class AnimacionCreacionObjeto : MonoBehaviour
{
    private GameObject circuloVisual;  // Objeto visual que representa el círculo bajo los objetos seleccionados
    private LTDescr tweenAnimacion;  // Variable para almacenar la animación de LeanTween
    private GameObject objetoActual;  // Objeto actualmente seleccionado

    void Start()
    {
        CrearCirculoVisual();  // Crear el círculo cuando inicie el juego
    }

   
    // Crea el círculo visual si no existe.
    // Se usa una esfera con escala reducida en el eje Y para simular un círculo en el suelo.
    
    private void CrearCirculoVisual()
    {
        circuloVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Crear esfera
        circuloVisual.transform.localScale = new Vector3(1.5f, 0.01f, 1.5f);  // Aplanarla en el eje Y
        circuloVisual.name = "CirculoVisual";  // Nombrarla para identificarla
        circuloVisual.SetActive(false);  // Inicialmente oculta

        // Crear un material transparente visible
        Material materialTransparente = new Material(Shader.Find("Standard"));
        materialTransparente.color = new Color(1, 1, 1, 0.5f);  // Semi-transparente
        materialTransparente.SetFloat("_Mode", 3);
        materialTransparente.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        materialTransparente.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        materialTransparente.SetInt("_ZWrite", 0);
        materialTransparente.DisableKeyword("_ALPHATEST_ON");
        materialTransparente.EnableKeyword("_ALPHABLEND_ON");
        materialTransparente.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        materialTransparente.renderQueue = 3000;

        circuloVisual.GetComponent<Renderer>().material = materialTransparente;  // Asignar el material al círculo

        Destroy(circuloVisual.GetComponent<Collider>());  // Eliminar colisión para evitar problemas físicos
    }

    void Update()
    {
        // Si el círculo está activo y hay un objeto seleccionado, actualizar su posición
        if (circuloVisual.activeSelf && objetoActual != null)
        {
            circuloVisual.transform.position = new Vector3(objetoActual.transform.position.x, 0.01f, objetoActual.transform.position.z);
        }
    }

   
    // Muestra el círculo debajo del objeto seleccionado y reinicia la animación.

    // <param name="objeto">El objeto seleccionado</param>
    public void MostrarCirculo(GameObject objeto)
    {
        if (circuloVisual == null)
        {
            CrearCirculoVisual();  // Si el círculo aún no existe, crearlo
        }

        objetoActual = objeto;  // Guardar referencia del objeto seleccionado
        circuloVisual.SetActive(true);  // Activar el círculo
        Debug.Log("Círculo activado debajo del objeto: " + objeto.name);

        ReiniciarAnimacion();  // Iniciar la animación
    }

    
    // Reinicia la animación "ping-pong" del círculo cada vez que se cancela o termina.
    
    private void ReiniciarAnimacion()
    {
        if (tweenAnimacion != null) LeanTween.cancel(circuloVisual);  // Cancelar animación anterior si existe

        // Crear animación "ping-pong" de expansión y contracción
        tweenAnimacion = LeanTween.scale(circuloVisual, new Vector3(2f, 0.01f, 2f), 0.5f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong()
            .setOnComplete(() => ReiniciarAnimacion());  // Si LeanTween cancela la animación, se reinicia automáticamente
    }


    // Oculta el círculo cuando el objeto se deselecciona.
 
    public void OcultarCirculo()
    {
        if (circuloVisual != null)
        {
           Debug.Log("Círculo ocultado.");
           LeanTween.cancel(circuloVisual);  // Detener cualquier animación en ejecución
           circuloVisual.SetActive(false);  // Ocultar el círculo
           objetoActual = null;  // Limpiar referencia del objeto seleccionado
        }
    }
}
