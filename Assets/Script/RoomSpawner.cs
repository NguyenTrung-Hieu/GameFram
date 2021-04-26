using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int OpeningDirection;
    //1 = can cua Bot
    //2 = can cua Left
    //3 = can cua Top
    //4 = can cua Right
    // Start is called before the first frame update
    private int rand;
    private RoomTemplates templates;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (spawned == false)
        {
            if (OpeningDirection == 1)
            {
                rand = Random.Range(0, templates.Botroom.Length);
                Instantiate(templates.Botroom[rand], transform.position, Quaternion.identity);
            }
            else if (OpeningDirection == 2)
            {
                rand = Random.Range(0, templates.Toproom.Length);
                Instantiate(templates.Toproom[rand], transform.position, Quaternion.identity);
            }
            else if (OpeningDirection == 3)
            {
                rand = Random.Range(0, templates.Leftroom.Length);
                Instantiate(templates.Leftroom[rand], transform.position, Quaternion.identity);
            }
            else if (OpeningDirection == 4)
            {
                rand = Random.Range(0, templates.Rightroom.Length);
                Instantiate(templates.Rightroom[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>().spawned==false && spawned == false)
            {
                Instantiate(templates.Closedroom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
