using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    /// <summary>
    /// Анимация атаки врага
    /// </summary>
    /// <param name="isAttack"></param> true - включает, false - выключает
    public void AttackAnimation(bool isAttack)
    {
        anim.SetBool("Attack", isAttack);
    }
    
    /// <summary>
    /// Анимация ходьбы врага
    /// </summary>
    /// <param name="isWalk"></param> true - включает, false - выключает
    public void WalkAnimation(bool isWalk)
    {
        anim.SetBool("Walk",isWalk);
    }
    
    /// <summary>
    /// Анимация победного рыка врага, может использоваться в момент патрулирования, при ожидании переключения на следующую точку
    /// </summary>
    /// <param name="isVictory"></param> true - включает, false - выключает
    public void VictoryAnimation(bool isVictory)
    {
        anim.SetBool("Victory",isVictory);
    }
    
    /// <summary>
    /// Анимация смерти врага
    /// </summary>
    /// <param name="isDead"></param> true - включает, false - выключает (но к мертвым это не применимо)
    public void DeathAnimation(bool isDead)
    {
        anim.SetBool("Death", isDead);
    }
    
    /// <summary>
    /// Анимация получения урона врага
    /// </summary>
    public void HitAnimation()
    {
        anim.SetTrigger("Hit");
    }
}
