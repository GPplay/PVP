using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    void Start(){
        AudioListener.volume=PlayerPrefs.GetFloat("volume");   
    }

    public void cargarNivel(string nombreNivel){
        //StartCoroutine(CargarEscena(nombreNivel));
        SceneManager.LoadScene(nombreNivel);
    }
    
    /*
    IEnumerator CargarEscena(string nombreNivel){
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreNivel);

        while(!operacion.isDone){
            float progreso = Mathf.Clamp01(operacion.progress/0.9f);
            Debug.Log(progreso);
          
            yield return null;
        }
    }*/

    public void Jugar(){
        SceneManager.LoadScene("Juego");
    }


    public void salir(){
        Application.Quit();
    }

    public void Tutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
