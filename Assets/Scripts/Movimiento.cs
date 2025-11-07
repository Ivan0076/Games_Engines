using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public InputActionAsset InputActions;

    InputAction playerMove;
    InputAction playerRun;
    InputAction activarPoder;

    Vector2 moveInput;

    public Rigidbody rb;
    public float walkSpeed;
    public float runSpeed;
    public float dashCooldown = 3f;
    private bool dashDisponible = true;
    public float dashForce = 10f;

    public float maxStamina;
    public float staminaRecoveryTime;
    private float currentStamina;
    private bool isRunning = false;
    private bool canRun = true;

    private PoderesJugador poderesJugador;

    public Slider staminaBar;

    void OnEnable()
    {
        InputActions.Enable();
    }

    void OnDisable()
    {
        InputActions.Disable();
    }

    void Awake()
    {
        playerMove = InputActions.FindAction("Move");
        playerRun = InputActions.FindAction("Sprint");
        activarPoder = InputActions.FindAction("ActivarPoder"); // ← Asegúrate de tener esta acción en el Input System

        currentStamina = maxStamina;
        poderesJugador = GetComponent<PoderesJugador>();
    }

    void Update()
    {
        moveInput = playerMove.ReadValue<Vector2>();
        bool runPressed = playerRun.IsPressed();

        // Selección de poder con teclas alfanuméricas
        if (Keyboard.current.zKey.wasPressedThisFrame)
            poderesJugador.poderSeleccionado = PoderActivo.Dash;
        else if (Keyboard.current.xKey.wasPressedThisFrame)
            poderesJugador.poderSeleccionado = PoderActivo.Invisibilidad;
        else if (Keyboard.current.cKey.wasPressedThisFrame)
            poderesJugador.poderSeleccionado = PoderActivo.Intangibilidad;

        // Activación del poder con Input System (Espacio)
        if (activarPoder.WasPressedThisFrame())
        {
            switch (poderesJugador.poderSeleccionado)
            {
                case PoderActivo.Dash:
                    if (poderesJugador.tieneDash && dashDisponible)
                    {
                        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                        dashDisponible = false;
                        Invoke(nameof(ReiniciarDash), dashCooldown);
                    }
                    break;
                case PoderActivo.Invisibilidad:
                    if (poderesJugador.tieneInvisibilidad)
                        poderesJugador.ActivarInvisibilidad();
                    break;
                case PoderActivo.Intangibilidad:
                    if (poderesJugador.tieneIntangibilidad)
                        poderesJugador.ActivarIntangibilidad();
                    break;
            }
        }

        // Movimiento y estamina
        if (runPressed && canRun && currentStamina > 0)
        {
            isRunning = true;
            currentStamina -= Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                canRun = false;
                isRunning = false;
                Invoke(nameof(RecuperarEstamina), staminaRecoveryTime);
            }
        }
        else
        {
            isRunning = false;
            if (currentStamina < maxStamina && canRun)
            {
                currentStamina += Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
        }

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + new Vector3(moveInput.x, 0, moveInput.y) * currentSpeed * Time.deltaTime);

        if (moveInput != Vector2.zero)
        {
            transform.forward = new Vector3(moveInput.x, 0, moveInput.y);
        }

        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }

    void RecuperarEstamina()
    {
        canRun = true;
        currentStamina = maxStamina;
    }
    void ReiniciarDash()
    {
        dashDisponible = true;
    }


}
