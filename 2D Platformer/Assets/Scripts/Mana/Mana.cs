using UnityEngine;

public class Mana : MonoBehaviour
{
    public float CurrentMana { get; private set; }
    [SerializeField] private float maxMana;

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

    public void RestoreManaToFull()
    {
        CurrentMana = maxMana;
    }

    public float GetMaxMana()
    {
        return maxMana;
    }

    private void Awake()
    {
        CurrentMana = maxMana;
    }
}