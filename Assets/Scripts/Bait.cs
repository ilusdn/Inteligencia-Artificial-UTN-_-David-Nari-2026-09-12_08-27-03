using Unity.VisualScripting;
using UnityEngine;

public class Bait : MonoBehaviour, IEdible
{
    public Vector3 Position => transform.position;
    public Vector3 Velocity => Vector3.zero;

    [SerializeField] private int _maxHealth = 50;
    private int _currentHealth;
    [SerializeField] private CharacterUI ui;
    public Hunter owner;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        ui.SetHealth(_currentHealth);
        if (IsDepleted)
        { 
            owner.ReturnBait(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    public bool IsDepleted => _currentHealth <= 0;

}
