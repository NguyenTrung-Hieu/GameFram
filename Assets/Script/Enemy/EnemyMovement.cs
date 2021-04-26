using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public float Speed = 3f;
    private const float minspeed = 1f;
    private const float maxspeed = 6f;
    [SerializeField]
    [Range(minspeed, maxspeed)]
    private float speed = minspeed;
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
    public Rigidbody2D Enemy;
    private Transform Target;
    Vector2 movement;
    Vector2 lastmovement;
    public Animator animator;
    public bool EnemyDead = false;
    private const int _min = 0;
    private const int _max = 2000;

    [SerializeField]
    [Range(_min, _max)]
    private int maxHP = _min;
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
    //public float AttackRate = 1f;
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
    //public float AttackRange = 2f;
    [SerializeField]
    [Range(_min, _max)]
    private int attackRange = _min;
    public int AttackRange
    {
        get { return attackRange; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, attackRange))
                attackRange = value;
        }
    }
    //public float ChaseRange = 4f;
    [SerializeField]
    [Range(_min, _max)]
    private int chaseRange = _min;
    public int ChaseRange
    {
        get { return chaseRange; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, chaseRange))
                chaseRange = value;
        }
    }
    float nextAttacktime = 0f;
    public HealthBar enemyhpbar;
    Vector2 dir;
    void Start()
    {
        CurHP = MaxHP;
        Enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyhpbar.SetMaXHealth(MaxHP);
        Target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        fixHP();
        enemyhpbar.SetHealth(CurHP);
        EnemyIsDead();
        
        if (EnemyDead != true)
        {
            FollowTarget();
            
            AttackingMove();
        }
    }
    private void FixedUpdate()
    {
        lastmovement = movement;
        
    }
    void fixHP()
    {
        if (CurHP <= 0)
        {
            CurHP = 0;
        }
    }
    void EnemyIsDead()
    {
        if (CurHP <= 0 && EnemyDead == false)
        {
            EnemyDead = true;
            animator.SetTrigger("isDead");
            Destroy(this.gameObject, 2f);
        }
    }
    void AttackingMove()
    {
        if (Vector3.Distance(Target.position, transform.position) <= AttackRange && Time.time >= nextAttacktime)
        {
                animator.SetTrigger("IsAttacking");
                nextAttacktime = Time.time + 1f / AttackRate;
        }
    }

    void FollowTarget()
    {
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        
        if (movement.x >= 0.8 || movement.x >= -0.8 || movement.y >= 0.8 || movement.y >= -0.8)
        {
            animator.SetFloat("LastHor", movement.x);
            animator.SetFloat("LastVer", movement.y);
            
        }
        if (movement.x - lastmovement.x == 0 && movement.y - lastmovement.y == 0)
        {
            animator.SetBool("IsMove", false);
        }
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRange
            && Vector3.Distance(Target.position, transform.position) > AttackRange)
        {
            dir = Target.transform.position - transform.position;
            dir.Normalize();
            movement = dir;
            animator.SetBool("IsMove", true);
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            //Enemy.MovePosition(Enemy.position + movement * Speed * Time.fixedDeltaTime);
        }
               
    }
}
