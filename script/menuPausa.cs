using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menuPausa : MonoBehaviour
{
   bool paused = false;
    [SerializeField] GameObject mapa;
    [SerializeField] GameObject menupausa;
    [SerializeField] GameObject menumuerte;
    public Text textoMuertos;
    public Text vidas;

    void Start(){
        menupausa.SetActive(false);
        menumuerte.SetActive(false);
        AudioListener.volume=PlayerPrefs.GetFloat("volume");
        ResumeGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                if(Time.timeScale != 0f){
                    PauseGame();
                }  
            }
        }
        textoMuertos.text="MUERTOS: " + Enemigo.numMuertos;
        vidas.text="SALUD: " + Movimiento.vida;
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Detener el tiempo
        paused = true;
        mapa.SetActive(false);
        menupausa.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        // Agrega cualquier otro código necesario para pausar tu juego
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        paused = false;
        mapa.SetActive(true);
        menupausa.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Agrega cualquier otro código necesario para reanudar tu juego
    }

    public void CargarEscena(string nombreNivel){
        SceneManager.LoadScene(nombreNivel);
    }

    public void Salir(){
        Application.Quit();
    }

    public void Menu(){
        SceneManager.LoadScene("Main menu");
        Spawner.enemigoMax = 1;
    }

    public void RestartScene()
    {
        // Obtenemos el índice de la escena actual
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Juego");

        // Reiniciamos la escena
        //SceneManager.LoadScene(currentSceneIndex);
        Enemigo.numMuertos = 0;
        Movimiento.vida = 10f;
        Spawner.enemigoMax = 1;

        ResumeGame();
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main menu");
    }

}