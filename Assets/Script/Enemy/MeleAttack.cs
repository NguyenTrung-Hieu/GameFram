using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    // Start is called before the first frame update
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
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement Player = collision.GetComponent<PlayerMovement>();
            if (Player != null)
            {
                Player.CurHP -= AttDMG;
            }
        }
    }
}
