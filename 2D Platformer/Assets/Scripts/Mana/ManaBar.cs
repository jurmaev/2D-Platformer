using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Mana playerMana;
    [SerializeField] private Image totalManaBar;
    [SerializeField] private Image currentManaBar;
    
    private void Start()
    {
        totalManaBar.fillAmount = playerMana.CurrentMana / playerMana.GetMaxMana();
    }

    private void Update()
    {
        currentManaBar.fillAmount = playerMana.CurrentMana / playerMana.GetMaxMana();
    }
}
