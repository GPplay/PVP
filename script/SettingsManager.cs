using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SettingsManager : MonoBehaviour
{
    //sensibilidad
    [SerializeField] private Slider _mouseSensSlider;
    public Text textoSensibilidad;
    public Slider sliderSensibilidad;

    //volumen
    public Slider volumenSlider;
    public Text textoVolumen;
    public AudioMixer mixerGroup;

    void Start()
    {
        //aqui se le pone el texto a el canvas a el iniciar
        textoSensibilidad.text = "Sensibilidad: " + sliderSensibilidad.value;
        textoVolumen.text = "Volumen: " + volumenSlider.value.ToString("f0");

        //volumen
        volumenSlider.value = PlayerPrefs.GetFloat("volume" , 50f);
        AudioListener.volume = volumenSlider.value;
    }
    
    public void Update()
    {
        //aqui se comprueba si hay cambios
        textoSensibilidad.text = "Sensibilidad: " + sliderSensibilidad.value;
        textoVolumen.text = "Volumen: " + volumenSlider.value.ToString("f0");
    }

    public void CambioVolumen(float valor)
    {
        volumenSlider.value = valor;
        PlayerPrefs.SetFloat("volume", volumenSlider.value);
        AudioListener.volume = volumenSlider.value;
        mixerGroup.SetFloat("MasterVolumen", Mathf.Log10(valor / 100) * 20f); 
    }

    private void Awake()
    {
        //Se modifica la sensibilidad
        _mouseSensSlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    private void OnDestroy(){
        PlayerPrefs.SetFloat("sensitivity", _mouseSensSlider.value);//no borrar
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main menu");
    }
}
/*
public class Audio;Monobehabiour {} supongamos que asi se llama entonces 
private Audio instance;   
public Instance { get => instance; set => instance = value;}  
void Awake() {  
    if(instance = null)
    { instance = this; DontDestroyOnLoad(this.gameObject);} 
    else {Destroy(gameObject;)}
} 
con eso ya lo llamas de cualquier clase con (nombre de la clase.Instancia.Metodo publico que crees ejemplo: Audio.Instance.SubirVolumen(cantidadVolumen);

mixer.SetFloat("masterVolume", Mathf.Log10(getVolumeType("master") / 100) * 20f); 
*/