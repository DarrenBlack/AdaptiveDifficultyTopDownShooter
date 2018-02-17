using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private SpriteRenderer rend;

    public float moveSpeed;
	private Rigidbody2D myRigidBody;
	
	private Vector3 moveInput;
	private Vector3 moveVelocity;

	private WeaponController weapon;
    public int health = 10;

	private Animator anim;

    public GameControllerScript gameController;

    public int flashTime;
    private int currentFlashTime;

    public Text healthText;

    // Use this for initialization
    void Start () {
        rend = gameObject.GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();	
		anim = GetComponent<Animator>();
		weapon = FindObjectOfType<WeaponController>() as WeaponController;
        gameController = FindObjectOfType<GameControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0f);
		moveVelocity = moveInput * moveSpeed;	

		anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

        weapon.UpdateFiringPoint();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            weapon.Fire(new Vector3(0, 0, 90));   
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            weapon.Fire(new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            weapon.Fire(new Vector3(180, 0, 180));            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            weapon.Fire(new Vector3(0, 180, 270));            
        }

        UpdateHealthText();
    }

	void FixedUpdate(){
		myRigidBody.velocity = moveVelocity;
        currentFlashTime--;
        if (currentFlashTime <= 0)
        {
            rend.material.SetFloat("_FlashAmount", 0);
        }
    }

    public void ReceiveDamage(int damage)
    {
        health = health - damage;

        rend.material.SetFloat("_FlashAmount", 1);
        currentFlashTime = flashTime;

        Debug.Log("Player health is " + health);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //stop bullets colliding with player when firing above or left
        if (coll.gameObject.layer == 9)
        {
            Physics2D.IgnoreLayerCollision(8, 9);
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

}