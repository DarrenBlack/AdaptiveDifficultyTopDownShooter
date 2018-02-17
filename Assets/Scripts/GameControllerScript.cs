using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameControllerScript : MonoBehaviour {

    public List<ZombieController> enemies = new List<ZombieController>();
    public PlayerController player;
    public MasterSpawner spawnController;
    public ScoreController scoreController;

    public Text endGameText;
    public Text lvlNumberText;

    public float lvl1Score, lvl2Score;
    public int currentLvl;
    public int enemiesKilled, enemiesToSpawn;
    
	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(5);
        StartGame();	
	}
	
	// Update is called once per frame
	void Update () {

        lvlNumberText.text = "Level: " + currentLvl.ToString();

        if (player.health <= 0)
        {
            EndGame();
        }
        else if(enemiesKilled == spawnController.noToSpawn)
        {
            EndGame();
        }        
	}

    public void EndGame()
    {
        if (enemies.Count > 0)
        {
            foreach (ZombieController i in enemies.ToArray())
            {
                if (i != null)
                {
                    i.Die();
                }
                enemies.Remove(i);
                enemiesKilled = 0;
            }
            
        }           

        if(currentLvl == 1)
        {
            lvl1Score = scoreController.score + (player.health * 1000);
            if(lvl1Score < 0) { lvl1Score = 0; }

            scoreController.score = 0; 
            Debug.Log("Level One Score: " + lvl1Score);            
        }
        else if(currentLvl == 2)
        {
            lvl2Score = scoreController.score + (player.health * 1000);
            if (lvl2Score < 0) { lvl2Score = 0; }
            scoreController.score = 0;
            Debug.Log("Level Two Score: " + lvl2Score);
        }
        enemiesKilled = 0;
        currentLvl++;
        ResetGame();
    }

    void CalculateDifficulty()
    {
        float percentageOfMax = lvl1Score / 30000;



        if (percentageOfMax <= 0.25)
        {
            spawnController.minHealth = 1;
            spawnController.maxHealth = 2;
            spawnController.minSpeed = 0.5f;
            spawnController.maxSpeed = 1f;
            spawnController.minAttackDamage = 1;
            spawnController.maxAttackDamage = 2;
            spawnController.minAttackSpeed = 0.5f;
            spawnController.maxAttackSpeed = 0.75f;
            spawnController.spawnDelay = 1.5f;
        }
        else
        {

            spawnController.minHealth = percentageOfMax * 10;
            spawnController.maxHealth = percentageOfMax * 20;
            spawnController.minSpeed = percentageOfMax * 4;
            spawnController.maxSpeed = percentageOfMax * 8;
            spawnController.minAttackDamage = 2;
            spawnController.maxAttackDamage = 4;
            spawnController.minAttackSpeed = 0.5f;
            spawnController.maxAttackSpeed = 0.75f;
            spawnController.spawnDelay = 1.5f;
        }
    }

    public void ResetGame()
    {
        Vector3 startingPositon = new Vector3(0, 0, -5);
        player.transform.position = startingPositon;

        player.health = 10;
        StartGame();
    }

    public void StartGame()
    {
        if(currentLvl == 1)
        {
            Debug.Log("starting level 1");
            spawnController.noToSpawn = 20;
            spawnController.minHealth = 5;
            spawnController.maxHealth = 10;
            spawnController.minSpeed = 2;
            spawnController.maxSpeed = 4;
            spawnController.minAttackDamage = 2;
            spawnController.maxAttackDamage = 4;
            spawnController.minAttackSpeed = 0.5f;
            spawnController.maxAttackSpeed = 0.75f;
            spawnController.spawnDelay = 1.5f;

            spawnController.spawn = true;
        }

        else if(currentLvl == 2)
        {
            Debug.Log("starting level 2");
            CalculateDifficulty();
            spawnController.spawn = true;
        }

        else if(currentLvl == 3)
        {
            spawnController.spawn = false;
            endGameText.text = "Thanks for Playing! \n Please note down these scores for \n the  questionnaire below \n Level 1 Score: " + lvl1Score.ToString() + "\n Level 2 Score: " + lvl2Score.ToString();
        }

        spawnController.Reset();
    }
}
