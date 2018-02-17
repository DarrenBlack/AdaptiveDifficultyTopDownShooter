using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy: MonoBehaviour{

    [SerializeField] private string Enemyname;
    [SerializeField] private Sprite Enemysprite;
    [SerializeField] private int Enemyhealth;
    [SerializeField] private int Enemyattackdamage;
    [SerializeField] private int Enemyspeed;




    public Enemy(){

    }

    public string EnemyName{
        get{return Enemyname;}
        set{Enemyname = value;}
    }

    public Sprite EnemySprite
    {
        get { return EnemySprite; }
        set { EnemySprite = value; }
    }

    public int EnemyHealth
    {
        get { return Enemyhealth; }
        set { Enemyhealth = value; }
    }

    public int EnemyAttackDamage
    {
        get { return Enemyattackdamage; }
        set { Enemyattackdamage = value; }
    }

    public int EnemySpeed
    {
        get { return Enemyspeed; }
        set { Enemyspeed = value; }
    }

    public enum State
    {
        Seek,
        Flee,
        Die,
        Patrol,
    }

    public State state;

    IEnumerator SeekState()
    {
        while(state == State.Seek)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator DieState()
    {
        while (state == State.Die)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator PatrolState()
    {
        while (state == State.Patrol)
        {
            yield return 0;
        }
        NextState();
    }

    void Start()
    {
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info = GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
