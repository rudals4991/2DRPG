using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float attackRange { get; private set; } = 2f;
    [SerializeField] protected int attackDamage = 10;
    public void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var target in targets)
        {
            if (target.TryGetComponent(out IDamagable damage)) damage.ApplyDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
