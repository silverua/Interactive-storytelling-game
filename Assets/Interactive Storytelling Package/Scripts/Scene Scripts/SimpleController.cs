using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float Speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var translation = Input.GetAxis("Vertical") * Speed;
        var strafe = Input.GetAxis("Horizontal") * Speed;

        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;
        
        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }
}
