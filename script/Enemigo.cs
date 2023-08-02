using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{   
    //Variables
    public float vida = 2f;
    public NavMeshAgent agent;
    public Transform jugador;
    public LayerMask Suelo, QueJugador;
    public static int numMuertos=0;
  

    //Patruyando
    public Vector3 puntoCamino;//poner que este punto tenga un rango que no se salga del mapa XD 
    bool caminarApunto;
    public float rangoDecaminata;

    //atacando
    bool AtaqueListo;
    public float shotRate = 2f;
    private float shotRateTime = 0;
    public GameObject Bala;
    public Transform spwanPoint;

    //estados
    public float rangoDAlerta, rangoDAtaque;
    public bool jugadorVisto, jugadorAtaque;

    //sonidos
    public AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        jugador = GameObject.Find("player1").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Comprueba la vista y el rango de ataque.
        jugadorVisto = Physics.CheckSphere(transform.position, rangoDAlerta, QueJugador);
        jugadorAtaque = Physics.CheckSphere(transform.position, rangoDAtaque, QueJugador);

        if (!jugadorVisto && !jugadorAtaque) patruyando();
        if (jugadorVisto && !jugadorAtaque) CasandoJugador();
        if (jugadorAtaque && jugadorVisto) atacandoJugador();
    }

    private void patruyando()
    {
        if (!caminarApunto) SearchWalkPoint();

        if (caminarApunto)
            agent.SetDestination(puntoCamino);

        Vector3 distanceToWalkPoint = transform.position - puntoCamino;

        //Punto de acceso alcanzado
        if (distanceToWalkPoint.magnitude < 1f){
            caminarApunto = false;
        }  
    }
    private void SearchWalkPoint()
    {
        //Calcular punto aleatorio en el rango
        float randomZ = Random.Range(-rangoDecaminata, rangoDecaminata);
        float randomX = Random.Range(-rangoDecaminata, rangoDecaminata);

        puntoCamino = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(puntoCamino, -transform.up, 2f, Suelo))
        {
            caminarApunto = true;

        }

    }

    private void CasandoJugador()
    {
        agent.SetDestination(jugador.position);
    }

    private void atacandoJugador()
    {
        //Asegúrate de que el enemigo no se mueva
        agent.SetDestination(transform.position);

        transform.LookAt(jugador);

        if (!AtaqueListo)
        {
            ///Código de ataque aquí
            if (Time.time > shotRateTime)
            {
                GameObject newBala;

                newBala = Instantiate(Bala, spwanPoint.position, spwanPoint.rotation);

                shotRateTime = Time.time + shotRate;

                Destroy(newBala, 0.6f);
                audioSource.Play();
            }
        }
    }
    private void ResetAttack()
    {
        AtaqueListo = false;
    }

    public void DamageReceived(float damageAmount)
    {
        Debug.Log("le estas dando plomo");
        vida -=damageAmount;
        if (vida <= 0f)
        {
            numMuertos++;
            Die();
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDAtaque);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDAlerta);
    }
}