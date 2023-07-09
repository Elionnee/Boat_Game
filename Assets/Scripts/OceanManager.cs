using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    // ---------------------------- VARIABLES ----------------------------

    // Altura de las olas
    public float wavesHeight = 1f;
    // Frequencia con la que se dan olas
    public float wavesFrequency = 0.2f;
    // Velocidad de las olas
    public float wavesSpeed = 0.1f;
    // Referencia al oceano
    public Transform ocean;

    // Referencia al material del oceano
    Material oceanMat;
    // Referencia al mapa normal que usa como referencia para crear las olas
    Texture2D wavesDisplacement;

    // ---------------------------- CODIGO ----------------------------

    // Llama a la función que se encarga de inicializar las referencias al
    // comienzo de la ejecución.
    void Start()
    {
        SetVariables();
    }

    // Inizializa las variables que hacen referencia al material del oceano y al mapa
    // normal de textura que se usa para crear las olas.
    void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMat.GetTexture("_WavesDisplacement");
    }

    // Actualiza la referencia del oceano segun la altura de las olas en el
    // momento indicado. 
    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y + wavesDisplacement.GetPixelBilinear(position.x 
            * wavesFrequency * ocean.localScale.x, (position.z * wavesFrequency 
            + Time.time * wavesSpeed) * ocean.localScale.z).g * wavesHeight;
    }

    // Actualiza el calor de las variables en caso de que cambien o aún no hayan
    // sido asignadas.
    private void OnValidate()
    {
        if (!oceanMat)
        {
            SetVariables();
        }

        UpdateMaterial();
    }

    // Actualiza el valor de las variables con la referencia del shader del
    // material del oceano.
    void UpdateMaterial()
    {
        oceanMat.SetFloat("_WavesFrequency", wavesFrequency);
        oceanMat.SetFloat("_WavesSpeed", wavesSpeed);
        oceanMat.SetFloat("_WavesHeight", wavesHeight);
    }
}
