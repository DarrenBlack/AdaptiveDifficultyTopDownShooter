using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bullet{
	[SerializeField] private string bulletName;
	[SerializeField] private Sprite bulletSprite;
	[SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;

	public Bullet(){
	}

	public string BulletName{
		get{return bulletName;}
		set{bulletName = value;}
	}

	public Sprite BulletSprite{
		get{return bulletSprite;}
		set{bulletSprite = value;}
	}

	public float BulletSpeed{
		get{return bulletSpeed;}
		set{bulletSpeed = value;}
	}

    public int BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }
}