using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{
    private Animator animator;
    private Controls controls;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new Controls();

        controls.Player.Jump.performed += JumpAnimation;
        controls.Player.Attack.performed += AttackAnimation;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = controls.Player.Move.ReadValue<Vector2>();
        MoveAnimation(inputVector);
    }

    /// <summary>
    /// движение игрока
    /// </summary>
    /// <param name="inputVector"></param> считывает значения системы ввода Player.Move
    private void MoveAnimation(Vector2 inputVector)
    {
        animator.SetFloat("RunVelocity", inputVector.y);
        if (inputVector.x<-0.2f)
            animator.SetFloat("StrafeVelocity", inputVector.x - 0.5f);
        else if (inputVector.x > 0.2f)
            animator.SetFloat("StrafeVelocity", inputVector.x + 0.5f);
        else
            animator.SetFloat("StrafeVelocity", inputVector.x);
    }

    /// <summary>
    /// Включение анимации прыжка
    /// </summary>
    /// <param name="context"></param> считывает значения системы ввода Player.Jump
    private void JumpAnimation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Jump");
        }
    }
    
    /// <summary>
    /// Включение анимации удара
    /// </summary>
    /// <param name="context"></param> считывает значения системы ввода Player.Attack
    private void AttackAnimation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// Выключение управления при смерти
    /// </summary>
    public void DeathAnimation()
    {
        OnDisable();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
