using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {

	public bool isFiring;
	private SpriteRenderer rend;
	public Gun[] guns;
	public Bullet[] bullets;
	public Gun currentlyEquipped;
	public Vector3 firingPoint;

    private float shotTimeStamp;

	// Use this for initialization
	void Start () {

		rend = gameObject.GetComponent<SpriteRenderer>();
		guns = new Gun[3];
		bullets = new Bullet[2];

		LoadBullets();
		LoadGuns();

		currentlyEquipped = guns[0];

		rend.sprite = currentlyEquipped.GunSprite;
	}
	
	// Update is called once per frame
	void Update () {
        
        //add gun firepoint offset to firingpoint
        

        /*
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;	

		//if aiming to left of character flip weapon by 180 degrees on y axis
		if((angle >= 90 && angle <= 180) ||(angle >= -180 && angle <= -90)){
			transform.rotation = Quaternion.Euler(new Vector3(0, -180, -angle - 180));
		}
		else{
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		//add gun firepoint offset to firingpoint
		firingPoint = ((transform.rotation * (Vector3.forward + currentlyEquipped.FirePoint)) + transform.position);
		firingPoint.z = 0;

		Vector3 forward = transform.TransformDirection(Vector3.right) * 10;
		Debug.DrawRay(firingPoint, forward, Color.green);
        */
    }

   public void UpdateFiringPoint()
    {
        firingPoint = ((transform.rotation * (Vector3.forward + currentlyEquipped.FirePoint)) + transform.position);
        firingPoint.z = 0;
    }

	void LoadBullets(){
		Sprite[] bulletSprites = Resources.LoadAll<Sprite>("BulletSpriteSheet");

		bullets[0] = new Bullet();
		bullets[0].BulletName = "Revolver Bullet";
		bullets[0].BulletSprite = bulletSprites[0];
		bullets[0].BulletSpeed = 750;
        bullets[0].BulletDamage = 2;

		bullets[1] = new Bullet();
		bullets[1].BulletName = "Rifle Bullet";
		bullets[1].BulletSprite = bulletSprites[1];
		bullets[1].BulletSpeed = 5;
	}

	void LoadGuns(){
		Sprite[] gunSprites = Resources.LoadAll<Sprite>("GunSpriteSheet");

		guns[0] = new Gun();
		guns[0].GunName = "Revolver";
		guns[0].GunDescription = "Neither red nor dead";
		guns[0].GunSprite = gunSprites[0];
		guns[0].FireType = Gun.FireTypes.SEMIAUTO;
		guns[0].PrimaryBullet = bullets[0];
		guns[0].FirePoint = new Vector3(0.550f,0.246f,0f);
        guns[0].FireSpeed = 0.2f;

		guns[1] = new Gun();
		guns[1].GunName = "Sawn Off";
		guns[1].GunDescription = "Insert pun here";
		guns[1].GunSprite = gunSprites[1];
		guns[1].FireType = Gun.FireTypes.SEMIAUTO;
		guns[1].PrimaryBullet = bullets[1];
		
		guns[2] = new Gun();
		guns[2].GunName = "Shotgun";
		guns[2].GunDescription = "Insert pun here";
		guns[2].GunSprite = gunSprites[2];
		guns[2].FireType = Gun.FireTypes.SEMIAUTO;
		guns[2].PrimaryBullet = bullets[1];
	}
	
	public void Fire(Vector3 direction){

        if (Time.time >= shotTimeStamp)
        {
            this.transform.rotation = Quaternion.Euler(direction);

            GameObject bullet = (GameObject)Instantiate(Resources.Load("FiredBullet"), firingPoint, transform.rotation);
            bullet.SendMessage("FireBullet", currentlyEquipped.PrimaryBullet);
            shotTimeStamp = Time.time + currentlyEquipped.FireSpeed;
        }
	}
}