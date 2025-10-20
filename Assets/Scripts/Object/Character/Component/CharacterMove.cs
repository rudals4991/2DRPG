using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 3f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool Move(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        return true;
    }
    public bool Stop()
    {
        rb.linearVelocity = Vector2.zero;
        return false;
    }
}
