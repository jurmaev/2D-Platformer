using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float maxMana;

    public float CurrentMana { get; private set; }
    
    
    private void Awake()
    {
        CurrentMana = maxMana;
    }

    public bool SpendMana(float mana)
    {
        if (CurrentMana - mana >= 0)
        {
            CurrentMana -= mana;
            return true;
        }

        return false;
    }

    public void RestoreMana(float mana)
    {
        CurrentMana = Mathf.Clamp(CurrentMana + mana, 0, maxMana);
    }

    public float GetMaxMana()
    {
        return maxMana;
    }
}
