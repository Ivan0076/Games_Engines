using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Disparar : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction accionDisparo;

    public GameObject proyectil;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 20f;

    public int municion = 0;
    public TMP_Text textoMunicion;

    void Awake()
    {
        accionDisparo = inputActions.FindAction("Attack"); 
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (accionDisparo != null && accionDisparo.WasPressedThisFrame() && municion > 0)
        {
            DispararProyectil();
        }

        if (textoMunicion != null)
            textoMunicion.text = "Talismanes: " + municion;
    }

    public void DispararProyectil()
    {
        GameObject nuevoProyectil = Instantiate(proyectil, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rbProyectil = nuevoProyectil.GetComponent<Rigidbody>();
        rbProyectil.AddForce(puntoDisparo.forward * fuerzaDisparo, ForceMode.Impulse);
        municion--;
    }

    public void RecogerMunicion(int cantidad)
    {
        municion += cantidad;
    }
}
