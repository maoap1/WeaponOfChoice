using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public Player Player { set; protected get; }
	public float reloadTime = 0.8f;
	public int attackStrength = 10;
	public Vector3 MakeAtLocalPosition = new Vector3(0, 0, 0);

    protected abstract bool Melee { get; }

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public virtual float ProjectileAlpha => Melee ? 0 : 1;
	public GameObject Projectile;
	public float ProjectileDiesAfter = 500_000;
	public int projectileSpeed = 5;
	public Vector3 ProjectileStartingAngle = new Vector3(1, 0, 0); 
	public float ProjectileGravityScale = 0.15f;
	float lastTimeAttacked = int.MinValue;

	private void Start()
	{
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
    }
	public bool Attack()
	{
		if (lastTimeAttacked + reloadTime < Time.timeSinceLevelLoad)
		{
			lastTimeAttacked = Time.timeSinceLevelLoad;
			ProjectileBehaviour projectile;
			if (Melee)
			{
				projectile =
				Instantiate(Projectile,
				GetComponent<Transform>().position + MakeAtLocalPosition,
				GetComponent<Transform>().rotation,
				Player.transform)
				.GetComponent<ProjectileBehaviour>();
			}
			else
			{
				projectile =
				Instantiate(Projectile,
				GetComponent<Transform>().position + new Vector3(MakeAtLocalPosition.x * (int)Player.lookingAt,
					MakeAtLocalPosition.y),
				GetComponent<Transform>().rotation)
				.GetComponent<ProjectileBehaviour>();
			}
			projectile.GetComponent<SpriteRenderer>().color = new Color(
				projectile.GetComponent<SpriteRenderer>().color.r,
				projectile.GetComponent<SpriteRenderer>().color.g,
				projectile.GetComponent<SpriteRenderer>().color.b,
				ProjectileAlpha);
			projectile.StartingAngle = ProjectileStartingAngle; 
			projectile.dieAtDistance = ProjectileDiesAfter;
			projectile.GravityScale = ProjectileGravityScale;
			projectile.Shooter = Player;
			projectile.AimingAt = Player.lookingAt;
			projectile.Speed = projectileSpeed;
			projectile.damage = attackStrength;
			return true;
		}
		return false;
	}

    public void PlaySound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}


