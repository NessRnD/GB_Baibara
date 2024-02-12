using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float gravity = -14f;
    [SerializeField] private float jumpHeight = 6f;
    private float yVelocity;
    private CharacterController cc;
    private Controls controls;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        controls = new Controls();
        controls.Player.Jump.performed += JumpMove;
    }

    private void FixedUpdate()
    {
        Gravity();
        Vector2 inputVector = controls.Player.Move.ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(inputVector.x, 0 , inputVector.y);
        moveVector = Camera.main.transform.forward * moveVector.z + Camera.main.transform.right * moveVector.x;
        moveVector.y = yVelocity;
        cc.Move(moveVector * speed);
    }
    private void LateUpdate()
    {
        float y = Camera.main.transform.localEulerAngles.y;
        transform.eulerAngles = new Vector3(0, y, 0);
    }

    private void JumpMove(InputAction.CallbackContext context)
    {

        if (cc.isGrounded)
        {
            if (context.performed)
            {
                yVelocity = jumpHeight;
            }
        }
    }

    private void Gravity() 
    {
        yVelocity += gravity * Time.deltaTime;
    }
    
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

}
