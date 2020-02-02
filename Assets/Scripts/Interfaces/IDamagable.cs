
public interface IDamagable
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    void TakeDamage(DamageType DamageType, float Damage);
    void Die();
}
