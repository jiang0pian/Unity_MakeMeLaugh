using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatherController : MonoBehaviour
{
    public GameObject[] Prefabs;

    public float maxHealth;
    public float currentHealth;

    private float createDurationTimer = 4f;

    public float minCreateDurationTime = 5.0f;
    public float maxCreateDurationTime = 10.0f;

    public float moveSpeed = 30f;
    public float changeDirectionTime = 4f;
    private float changeDirectionTimer = -1f;
    private Vector3 lookDirection = new Vector3(1, 0, 0);

    //Instantiate(bulletPrefab, rigidbody2D.position + Vector2.up* 5f + -1 * lookDirection* 5f, Quaternion.identity);

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
        if (createDurationTimer < 0)
        {
            CreateEnemy();
            createDurationTimer = Random.Range(minCreateDurationTime, maxCreateDurationTime);
        }
        createDurationTimer -= Time.deltaTime;
    }

    public void Move()
    {
        if(changeDirectionTimer < 0)
        {
            lookDirection.x *= -1;
            changeDirectionTimer = changeDirectionTime;
        }
        changeDirectionTimer -= Time.deltaTime;
        Vector3 addDirection = new Vector3(lookDirection.x * moveSpeed * Time.deltaTime, 0, 0);
        transform.SetPositionAndRotation(transform.position + addDirection, Quaternion.identity);
    }

    public void CreateEnemy()
    {
        int randomIntInclusive = Random.Range(0, 100);
        GameObject enemy = Instantiate(Prefabs[randomIntInclusive % 4], transform.position, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 3000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //..............
    }
}
