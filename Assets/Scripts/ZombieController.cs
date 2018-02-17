using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public Transform target;
    Vector2[] path;
    private SpriteRenderer rend;

    ScoreController scoreController;
    
    
    int targetIndex;

    public float health;
    public float attackDamage;
    public float movementSpeed;
    public float attackSpeed;

    public int score = 1000;

    float nextAttack = 0.0f;

    
    public int flashTime;
    private int currentFlashTime;
    

    // Use this for initialization
    void Start()
    {

        rend = gameObject.GetComponent<SpriteRenderer>();
        scoreController = FindObjectOfType<ScoreController>();
        

        target = FindObjectOfType<PlayerController>().transform;

        StartCoroutine(RefreshPath());
        StartCoroutine(ScoreDecay());
        
    }    

    public void ReceiveDamage(int damage)
    {
        health = health - damage;

        
        rend.material.SetFloat("_FlashAmount", 1);
        currentFlashTime = flashTime;
        
        
        if (health <= 0) {
            Debug.Log("zombie killed for " + score + " points");
            scoreController.addToScore(score);
            Die();
        }

//        Debug.Log(health);
    }

    void FixedUpdate()
    {
        currentFlashTime--;
        if (currentFlashTime <= 0)
        {
            rend.material.SetFloat("_FlashAmount", 0);
        }
    }    
    
    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially        
        while (true)
        {
            if (target != null)
            {
                if (targetPositionOld != (Vector2)target.position)
                {
                    targetPositionOld = (Vector2)target.position;

                    path = Pathfinding.RequestPath(transform.position, target.position);
                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");
                }
            }
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
                yield return null;

            }
        }
    }
    

    IEnumerator ScoreDecay()
    {
            while (score > 250)
            {
                score = score - 50;
                yield return new WaitForSeconds(1);
            }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
       

        if (coll.collider.GetComponent<PlayerController>() && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            coll.collider.GetComponent<PlayerController>().ReceiveDamage(Mathf.RoundToInt(attackDamage));           
        }
    }

    public void Die()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
       
            }
        }
    }
}
