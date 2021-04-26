using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProjectType;


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
                Instantiate(ProjectType,transform.position,Quaternion.identity);
            }
        }
    }
}
