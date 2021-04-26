using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectRotation : MonoBehaviour
{
    private Transform player;
    private Vector2 target;
    
    private const float minspeed = 1f;
    private const float maxspeed = 10f;
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
    private const int _min = 0;
    private const int _max = 2000;
    [SerializeField]
    [Range(_min, _max)]
    private int attDMG = _min;
    public int AttDMG
    {
        get { return attDMG; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, attDMG))
                attDMG = value;
        }
    }
    // Start is called before the first frame update
    private Rigidbody2D arrow;

    void Start()
    {
        arrow = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.rotation = angle;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyArrows();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement Player = collision.GetComponent<PlayerMovement>();
            if (Player != null)
            {
                Player.CurHP -= AttDMG;
            }
            DestroyArrows();
        }
    }
    void DestroyArrows()
    {
        Destroy(gameObject);
    }
}
