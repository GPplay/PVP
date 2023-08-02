using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject terreno;
    private bool interruptor = false;
    public static int enemigoMax=1;

    void Start()
    {
        spawnearEnemigoY();
    }

    void FixedUpdate()
    {
          if (this.numeroClones() == true)
        {
            if (this.numeroClonesInt()< enemigoMax && interruptor == false)
            {
                spawnearEnemigoY();
                if (this.numeroClonesInt() == enemigoMax)
                {
                    interruptor = true;
                }
                //te dice cuantos enemigoa aprecieron 
                //Debug.Log(this.numeroClonesInt());
            }
        }

        if (this.numeroClones() == false)
        {
            enemigoMax++;
            interruptor = false;
            spawnearEnemigoY();
        }
    }

    private void spawnearEnemigoY()
    {
        // Obtener las dimensiones del terreno
        Renderer terrainRenderer = terreno.GetComponent<Renderer>();
        Bounds terrainBounds = terrainRenderer.bounds;

        // Generar un punto aleatorio dentro de las dimensiones del terreno
        float x = Random.Range(200, 300);
        float z = Random.Range(300, 380);
        //Debug.Log("min x: " + terrainBounds.min.x);
        //Debug.Log("max x: " + terrainBounds.max.x);
        //Debug.Log("min z: " + terrainBounds.min.z);
        //Debug.Log("max z: " + terrainBounds.max.z);

        // Obtener la altura del terreno en el punto seleccionado
        float y = obtenerYTerreno(x, z);
        if (y > 9.0)
        {
            return;
        }

        // Instanciar el GameObject en la posición determinada
        Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    private float obtenerYTerreno(float x, float z)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, this.gameObject.transform.position.y, z), Vector3.down, out hit))
        {
            return hit.point.y;
        }
        return 0f;
    }

    public bool numeroClones()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag(prefab.tag);
        if (clones.Length == 0)
        {
            return false;
        }

        return true;
    }


    private int numeroClonesInt()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag(prefab.tag);
        return clones.Length;
    }
}