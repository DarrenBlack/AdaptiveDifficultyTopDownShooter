using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public float score;
    public GameControllerScript gameController;

    public void addToScore(int a)
    {
        gameController.enemiesKilled++;
        score = score + a;        
    }
}
