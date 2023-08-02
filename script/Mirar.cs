using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mirar : MonoBehaviour
{
    public float sensitivity = 0f;
    public Transform player1;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 75f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player1.Rotate(Vector3.up * mouseX);

    }
}
