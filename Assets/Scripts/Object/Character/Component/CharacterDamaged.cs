using System;
using UnityEngine;

public class CharacterDamaged : MonoBehaviour
{
    public event Action<int> OnDamaged;
    [SerializeField] float invincibileTime = 0.2f;
    float timer = 0f;

    public void Tick(float dt)
    {
        if (timer > 0f) timer -= dt;
    }
    public void ApplyDamage(int damage)
    {
        if (timer > 0f) return;
        OnDamaged?.Invoke(damage);
        timer = invincibileTime;
    }
    public void GetDamaged()
    { 
        //TODO: ¿¬Ãâ
    }
    public bool IsInvincibleOver() => timer <= 0f;
}
