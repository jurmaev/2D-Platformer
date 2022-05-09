using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Image totalBar;

    [SerializeField] private float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bar.fillAmount = 1;
    }

    public void FillAmount(Image bar, float current, float total)
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, current / total,
            lerpSpeed * Time.deltaTime);
        var healthColor = Color.Lerp(Color.red, Color.green, current / total);
        bar.color = healthColor;
    }
    
}
