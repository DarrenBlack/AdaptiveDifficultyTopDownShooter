using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    Transform spawnpoint;
    public GameObject gameController;

    void Start()
    {
        
    }

    public void SpawnEnemy(float health, float speed, float damage, float attackSpeed)
    {
        GameObject zombie = (GameObject)Instantiate(Resources.Load("Zombie"));
        zombie.transform.position = gameObject.transform.position;
        ZombieController controller = zombie.GetComponent<ZombieController>();

        gameController.GetComponent<GameControllerScript>().enemies.Add(controller);
        controller.health = health;
        controller.movementSpeed = speed;
        controller.attackDamage = damage;
        controller.attackSpeed = attackSpeed;
        
    }
}
