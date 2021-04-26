using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float minspeed = 1f;
    private const float maxspeed = 6f;
    [SerializeField]
    [Range(minspeed, maxspeed)]
    private float speed=minspeed;
    public float Speed
    {
        get { return speed; }
        set
        {
            value = Mathf.Clamp(value, minspeed, maxspeed);
            if (!Mathf.Approximately(value, speed))
                speed = value;
        }
    }

    public Rigidbody2D Player;
    Vector2 movement;
    public Animator animator;
    public bool PlayerDead = false;

    private const int _min = 0;
    private const int _max = 2000;
    
    [SerializeField]
    [Range(_min, _max)]
    private int maxHP= _min;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, maxHP))
                maxHP = value;
        }
    }
    [SerializeField]
    [Range(0, _max)]
    private int curHP;
    public int CurHP
    {
        get { return curHP; }
        set
        {
            value = Mathf.Clamp(value, 0, _max);
            if (!Mathf.Approximately(value, curHP))
                curHP = value;
        }
    }
   
    [SerializeField]
    [Range(_min, _max)]
    private float maxStamina = _min;
    public float MaxStamina
    {
        get { return maxStamina; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, maxStamina))
                maxStamina = value;
        }
    }
    [SerializeField]
    [Range(_min, _max)]
    private float curStamina;
    public float CurStamina
    {
        get { return curStamina; }
        set
        {
            value = Mathf.Clamp(value, 0, _max);
            if (!Mathf.Approximately(value, curStamina))
                curStamina = value;
        }
    }

    [SerializeField]
    [Range(_min, _max)]
    private int staminaAtk = _min;
    public int StaminaAtk
    {
        get { return staminaAtk; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, staminaAtk))
                staminaAtk = value;
        }
    }
    [SerializeField]
    [Range(_min, _max)]
    private int genStamina = _min;
    public int GenStamina
    {
        get { return genStamina; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, genStamina))
                genStamina = value;
        }
    }
    float nextGen = 0f;
    [SerializeField]
    [Range(_min, _max)]
    private int genRateStamina = _min;
    public int GenRateStamina
    {
        get { return genRateStamina; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, genRateStamina))
                genRateStamina = value;
        }
    }
    [SerializeField]
    [Range(_min, _max)]
    private int attackRate = _min;
    public int AttackRate
    {
        get { return attackRate; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, attackRate))
                attackRate = value;
        }
    }
    float nextAttacktime = 0f;
    // Start is called before the first frame update
    public HealthBar hpbar;
    public StaminaBar staminaBar;
    public GameObject Menu;
    public GameObject GameOver;
    public GameObject HideText;
    public GameObject HideButton;
    [SerializeField]
    [Range(_min, _max)]
    private int healpotionrate = _min;
    public int HealPotionRate
    {
        get { return healpotionrate; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, healpotionrate))
                healpotionrate = value;
        }
    }
    [SerializeField]
    [Range(_min, _max)]
    private int healpotion = _min;
    public int HealPotion
    {
        get { return healpotion; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, healpotion))
                healpotion = value;
        }
    }

    float nextHealPotionRate = 0f;
    private void Start()
    {
        CurStamina = 50;
        CurHP = MaxHP;
        Player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hpbar.SetMaXHealth(MaxHP);
        staminaBar.SetMaXStamina(MaxStamina);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        Movement();
        GoToPauseMenu();
    }
    void IsPlayerDead()
    {

        if (CurHP <= 0 && PlayerDead == false)
        {
            PlayerDead = true;
            animator.SetTrigger("isDead");
            Invoke("MenuGameOver", 2);
        }
    }
    void MenuGameOver()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        GameOver.SetActive(true);
        HideText.SetActive(false);
        HideButton.SetActive(false);
    }
    void fixHPandStamina()
    {
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
        if(curStamina>maxStamina)
        {
            curStamina = maxStamina;
        }
        if (Time.time >= nextGen && CurStamina<MaxStamina)
        {
            CurStamina += GenStamina;
            nextGen = Time.time + 1f / GenRateStamina;
            CurStamina += GenStamina;
            staminaBar.SetStamina(CurStamina);
        }
        DrinkHealPotion();
        hpbar.SetHealth(CurHP);
    }
    void Movement()
    {
        IsPlayerDead();
        if (PlayerDead==false)
        {
            fixHPandStamina();
            //Movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
            {
                animator.SetFloat("LastHor", movement.x);
                animator.SetFloat("LastVer", movement.y);
            }
            //attack
            if (Time.time >= nextAttacktime)
            {
                if (Input.GetButtonDown("Attacking")&&CurStamina>0&&CurStamina>=StaminaAtk)
                {

                    AttackingMove();
                    nextAttacktime = Time.time + 1f / AttackRate;
                    CurStamina-= StaminaAtk;
                    staminaBar.SetStamina(CurStamina);
                }
            }
        }
        
    }
    void DrinkHealPotion()
    {
        if (Time.time >= nextHealPotionRate)
        {
            if (Input.GetButtonDown("HealButton") && CurHP > 0)
            {
                nextHealPotionRate = Time.time + HealPotionRate;
                CurHP += HealPotion;
            }
        }
    }
    void AttackingMove()
    {
        animator.SetTrigger("IsAttacking");
    }
    void GoToPauseMenu()
    {
        if (Input.GetButtonDown("Cancel") && Menu.active == false && GameOver.active== false)
        {
            Menu.SetActive(true);
            HideButton.SetActive(true);
            Time.timeScale = 0f;
        }else
        if (Input.GetButtonDown("Cancel") && Menu.active == true && GameOver.active == false)
        {
            Menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    void FixedUpdate()
    {
        if(PlayerDead==false)
            Player.MovePosition(Player.position + movement * Speed * Time.fixedDeltaTime);
    }
}
