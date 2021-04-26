using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public void SetMaXStamina(float stamina)
    {
        slider.maxValue = stamina;
    }
    public void SetStamina(float stamina)
    {
        slider.value = stamina;
    }
}
