using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    private EnemySpawner enemySpawner;
    private KillCounter killCounter; // Reference to the KillCounter script

    int expAmount = 100;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        killCounter = FindObjectOfType<KillCounter>(); // Find the KillCounter script in the scene
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health = 1;

    public void Defeated()
    {
        DropLoot();
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated();
        }

        if (killCounter != null) // Increment kill count if KillCounter script is found
        {
            killCounter.IncrementKillCount();
        }
        Debug.Log(expAmount);
        ExperienceManager.Instance.AddExperience(expAmount);
        Destroy(gameObject);
    }

    public void DropLoot()
    {
        if (GetComponent<LootBag>() != null)
        {
            GetComponent<LootBag>().InstantiateLoot(transform.position);
        }
    }
}