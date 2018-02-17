using UnityEngine;
using System.Collections;

[System.Serializable]
public class Gun{

	[SerializeField] private string gunName;
	[SerializeField] private string gunDescription;
	[SerializeField] private Sprite gunSprite;
	public enum FireTypes{
		AUTO,
		SEMIAUTO
	}
	[SerializeField] private FireTypes fireType;
	[SerializeField] private Bullet primaryBullet;
	[SerializeField] private Vector3 firePoint;
    [SerializeField] private float fireSpeed;

    public Gun(){

	}

	public string GunName{
		get{return gunName;}
		set{gunName = value;}
	}

	public string GunDescription{
		get{return gunDescription;}
		set{gunDescription = value;}
		}

	public Sprite GunSprite{
		get{return gunSprite;}
		set{gunSprite = value;}
	}

	public FireTypes FireType{
		get{return fireType;}
		set{fireType = value;}
	}

	public Bullet PrimaryBullet{
		get{return primaryBullet;}
		set{primaryBullet = value;}
	}

	public Vector3 FirePoint{
		get{return firePoint;}
		set{firePoint = value;}
	}

    public float FireSpeed{
        get {return fireSpeed; }
        set { fireSpeed = value; }
    }
}