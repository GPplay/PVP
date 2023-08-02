
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject Bala;
    public Transform spwanPoint;

    public float shotRate = 0.5f;
    private float shotRateTime = 0;    


    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0f){

            if(Input.GetButtonDown("Fire1")){
            
                if(Time.time>shotRateTime){
                    GameObject newBala;

                    newBala = Instantiate(Bala, spwanPoint.position,spwanPoint.rotation);
                    
                    shotRateTime = Time.time + shotRate;
                    
                    Destroy(newBala, 1.5f);
                }
            }
            
        }
        
    }
}
