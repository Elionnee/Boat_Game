using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReactToWater : MonoBehaviour
{
    // ---------------------------- VARIABLES ----------------------------

    // Transform de los puntos de referencia del barco
    public Transform[] Floaters;

    // Caracteristicas ambientales del oceano
    public float UnderWaterDrag = 3f;
    public float UnderWaterAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;
    public float FloatingPower = 15f;

    // Referencia al ocean manager responsable del oleaje
    OceanManager oceanManager;

    // Referencia al barco
    Rigidbody Rb;
    // Variables para saber si esta bajo el agua
    bool Underwater;
    int FloatersUnderWater;

    // ---------------------------- CODIGO ----------------------------

    // Recoge la referencia al barco y al ocean manager al iniciar la ejecución
    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
        oceanManager = FindObjectOfType<OceanManager>();
    }

    // Una vez por frame, revisa la posición actual del barco y aplica las fuerzas
    // correspondientes para hacerlo flotar o hundirse
    void FixedUpdate()
    {
        FloatersUnderWater = 0;
        for (int i = 0; i < Floaters.Length; i++)
        {
            float diff = Floaters[i].position.y - oceanManager.WaterHeightAtPosition(Floaters[i].position);
            if (diff < 0)
            {
                Rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(diff), Floaters[i].position, ForceMode.Force);
                FloatersUnderWater += 1;
                if (!Underwater)
                {
                    Underwater = true;
                    SwitchState(true);
                }
            }
        }
        if (Underwater && FloatersUnderWater == 0)
        {
            Underwater = false;
            SwitchState(false);
        }
    }

    // Revisa el estado actual del barco y lo cambia, actualizando las fuerzas que
    // actuarán sobre él en la función fixedUpdate.
    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            Rb.drag = UnderWaterDrag;
            Rb.angularDrag = UnderWaterAngularDrag;
        }
        else
        {
            Rb.drag = AirDrag;
            Rb.angularDrag = AirAngularDrag;
        }
    }
}
