using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float CameraMoveSpeed = 0.005f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        float x = 0;
        float y = 0;

        Vector3 totalMovement = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            totalMovement += Camera.main.transform.forward;
           // y += CameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            totalMovement -= Camera.main.transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            totalMovement += (Quaternion.AngleAxis(-90, Vector3.up) * Camera.main.transform.forward);
        }

        if (Input.GetKey(KeyCode.D))
        {
            totalMovement += (Quaternion.AngleAxis(90, Vector3.up) * Camera.main.transform.forward);
        }

        // Vector3 vector = gameObject.transform.localRotation * new Vector3(x, y, 0);
        totalMovement *= CameraMoveSpeed;
        gameObject.transform.Translate(totalMovement);




        //if (Input.GetKey(KeyCode.Q))
        //{
        //    gameObject.transform.Rotate(Vector3.left, 20);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    gameObject.transform.Rotate(Vector3.right, 20);
        //}
        //gameObject.transform.Translate(x, y, 0);
    }
}
