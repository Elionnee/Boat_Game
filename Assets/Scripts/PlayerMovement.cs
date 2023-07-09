using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ---------------------------- VARIABLES ----------------------------

    public Rigidbody boat;

    // Fuerzas para los movimientos del barco
    public float forwardAccel = 12f;
    public float reverseAccel = 7f;
    public float turnStrength = 10f;

    // Limite de velocidad del barco
    public float maxSpeed = 20f;

    // Inputs del jugador
    private float speedInput;
    private float turnInput;

    // ---------------------------- CODIGO ----------------------------

    // Recoge el transform del barco al iniciar la ejecución
    void Start()
    {
        boat.transform.parent = null;
    }

    // Maneja las físicas que se tienen que actualizar frame por frame,
    // en este caso, añade la fuerza de aceleración al barco. 
    private void FixedUpdate()
    {
        if (Mathf.Abs(speedInput) > 0)
        {
            boat.AddForce(transform.forward * speedInput);
        }
    }

    // Se encarga de actualizar las fuerzas que afectan al barco dependiendo de los
    // inputs introcidos por el jugador.
    void Update()
    {
        speedInput = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        } else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        turnInput = Input.GetAxis("Horizontal");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles
            + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));

        transform.position = boat.transform.position;
    }
}