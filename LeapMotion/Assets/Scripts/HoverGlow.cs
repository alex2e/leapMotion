using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

//Se indica que es requerido que exista este componente sino te lo indicará en Unity
[RequireComponent(typeof(InteractionBehaviour))]
public class HoverGlow : MonoBehaviour {
    [Header("Activación del Glow")]
    [Tooltip("Si está activado, cambiará de color el objeto en función de lo cerca que esté la distancia de la mano")]
    public bool useHover = true;

    [Tooltip("Si está activado el objeto usará su primaryHoverColor cuando se acerque la mano")]
    public bool userPrimaryHover = true;

    [Header("Colores")]
    //Hacemos una interpolación lineal entre dos colores, para conseguir un color intermedio
    //Gris oscuro
    public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1f);
    //Gris claro
    public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7f);
    //Gris muy claro
    public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8f);
    //Suavizado del cambio de color
    public float smoothColor = 5f;

    //Referencia al material del objeto
    private Material material;
    //Referencia al Scritp InteractionBehaviour
    private InteractionBehaviour intObj;

    // Use this for initialization
    void Start () {
        //recuperamos la referencia al componente InteractionBehaviour
        intObj = GetComponent<InteractionBehaviour>();
        //Intentamos recuperar el componente Renderer del Objeto
        Renderer renderer = GetComponent<Renderer>();

        //Si no existe el rendereren el objeto, lo buscamos en sus hijos
        if(renderer == null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        //Si hemos encontrado el renderer, recuperamos el material
        if(renderer != null)
        {
            material = renderer.material;
        }	
	}
	
	// Update is called once per frame
	void Update () {
        //Verificamos si existe la referencia al material
        if(material != null)
        {
            Color targetColor = defaultColor;

            //Si el elemento detectado es primaryHover, el color objetivo será el definido como primaryHoverColor
            if(intObj.isPrimaryHovered && userPrimaryHover)
            {
                targetColor = primaryHoverColor;
            }
            else
            {
                //Si el elemento detectado es el hover, el color objetivo serña el definido como hoverColor
                if(intObj.isHovered && useHover)
                {
                    //Calsulamos el glow en funcion de la distancia a la palma de la mano
                    //convertimos con un Map el valor de 0 a 0.2 de forma proporcional de 1 a 0
                    float glow = intObj.closestHoveringControllerDistance.Map(0f, 0.2f, 1f, 0.0f);
                    targetColor = Color.Lerp(defaultColor, hoverColor, glow);
                }
            }

            material.color = Color.Lerp(material.color, targetColor, smoothColor * Time.deltaTime);
        }	
	}
}
