public interface IHealth_tuan
{
    int Health { get; set; }
    int MaxHealth { get; set; }
    void TakeDamage(int amount);
    void Heal(int amount);
    void Dead();
}
