
public interface IDamagable
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    void Initialize(float MaxHealth);
    void TakeDamage(DamageType DamageType, float Damage);
    void Die();
}
