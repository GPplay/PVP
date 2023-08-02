using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Movimiento : MonoBehaviour
{
    //movimiento
    public int moveSpeed = 5;
    public static float vida = 10f;
    public int jumpForce = 10;
    public float gravity = 20.0f;
    public Terrain terrain;

    //ataque
    public GameObject Bala;
    public Transform spwanPoint;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;    
    //salto
    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isJumping = false;

    [SerializeField] GameObject menumuerte;

    //Sonidos
    [SerializeField] private static AudioSource audioSource;
    [SerializeField] private AudioClip colectar1;

    //efectos de daños
    public GameObject efectos;
    private Vignette vignetteEffect;
    private PostProcessVolume postProcessVolume;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        postProcessVolume = efectos.GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        isJumping = !controller.isGrounded;

            if(Time.timeScale != 0f){

                if(Input.GetButtonDown("Fire1")){
                
                    if(Time.time>shotRateTime){
                        GameObject newBala;

                        newBala = Instantiate(Bala, spwanPoint.position,spwanPoint.rotation);
                        
                        shotRateTime = Time.time + shotRate;
                        
                        Destroy(newBala, 1.5f);
                        audioSource.PlayOneShot(colectar1);
                    }
                }
            
        }
        
    }
    
    public void DamageReceived(float damageAmount)
    {
        vida -=damageAmount;
        if (postProcessVolume.sharedProfile.TryGetSettings(out vignetteEffect))
        {
            float delayTime = 0.2f;
            vignetteEffect.intensity.value = 0.65f;
            Invoke("Vuelta0", delayTime);
        }
        if (vida <= 0f)
        {
            Die();
        }
    }
    
    void Die()
    {
        Time.timeScale = 0f; // Detener el tiempo
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        menumuerte.SetActive(true);
        audioSource.Play();
        Vuelta0();
    }
    
    void Vuelta0(){
        vignetteEffect.intensity.value = 0f;
    }
}