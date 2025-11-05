using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 


public class Movimiento : MonoBehaviour
{
    public InputActionAsset InputActions;

    InputAction playerMove;
    InputAction playerRun;

    Vector2 moveInput;

    public Rigidbody rb;
    public float walkSpeed;
    public float runSpeed;

    public float maxStamina; // segundos corriendo
    public float staminaRecoveryTime; // tiempo de espera para recuperar
    private float currentStamina;
    private bool isRunning = false;
    private bool canRun = true;

    private PoderesJugador poderesJugador;
    public float dashForce = 10f;

    public Slider staminaBar; // Asigna el slider desde el inspector

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
        currentStamina = maxStamina;
        poderesJugador = GetComponent<PoderesJugador>();
    }

    void Update()
    {
        moveInput = playerMove.ReadValue<Vector2>();
        bool runPressed = playerRun.IsPressed();

        // Verifica si puede correr
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
                currentStamina += Time.deltaTime; // recuperación lenta mientras camina
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

        if (Input.GetKeyDown(KeyCode.Space) && poderesJugador.tieneDash)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        }
    }

    void RecuperarEstamina()
    {
        canRun = true;
        currentStamina = maxStamina;
    }


}
