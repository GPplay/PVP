using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float damage = 1f; // Daño que inflige la bala
    public string enemyTag = "enemy";
    public string jugadorTag = "jugador1";
    public float velocidad = 1f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
    
   void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag(enemyTag))
        {
            // Si la bala colisiona con un objeto que tenga el tag de enemigo, inflige daño
            collision.gameObject.GetComponent<Enemigo>().DamageReceived(damage);

            // Destruye la bala
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag(jugadorTag)){
            collision.gameObject.GetComponent<Movimiento>().DamageReceived(damage);

            Destroy(gameObject);
        }
    }
    
}

