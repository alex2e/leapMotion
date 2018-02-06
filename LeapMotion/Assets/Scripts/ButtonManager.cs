using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    /// <summary>
    /// Referencia al aocmponente AudioSource
    /// </summary>
    private AudioSource audiosource;

	// Use this for initialization
	void Start () {
        //Recupermaos la referencia al componente audiosource
        audiosource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Llama al play del AudioSource
    /// </summary>
    public void click()
    {
        audiosource.Play();
    }
}
