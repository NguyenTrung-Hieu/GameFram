using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSkill : MonoBehaviour
{
    private const int _min = 0;
    private const int _max = 2000;
    // Start is called before the first frame update
    public Image ImageCoolDown;
    public string InputName;
    bool IsCooldown=false;
    [SerializeField]
    [Range(_min, _max)]
    private float cooldown = _min;
    public float Cooldown
    {
        get { return cooldown; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, cooldown))
                cooldown = value;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(InputName))
        {
            IsCooldown=true;
        }
        if (IsCooldown)
        {
            ImageCoolDown.fillAmount += 1 / Cooldown * Time.deltaTime;
            if (ImageCoolDown.fillAmount >=1)
            {
                ImageCoolDown.fillAmount = 0;
                IsCooldown = false;
            }
        }
    }
}
