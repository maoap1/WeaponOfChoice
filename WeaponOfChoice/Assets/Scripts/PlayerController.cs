using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{

    public float speed = 200f;
    public float jumpSpeed = 200f;
	public GameObject WeaponsBase;

	public Side LookingAt { get; private set; } = Side.Left;
	GameObject WeaponPrefab;
	private Weapon Weapon => WeaponPrefab.GetComponent<Weapon>();

	InputResults Input => GetComponent<InputManager>().CurrInput;

    // Start is called before the first frame update
    void Start()
    {
		WeaponPrefab = WeaponsBase.GetComponent<BaseOfWeapons>().GetWeaponThatIs(GlobalFields.GetWeapon());
		WeaponPrefab = Instantiate(WeaponPrefab, GetComponent<Transform>());
		Weapon.pc = this;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.Horizontal, 0, 0);

		LookingAt = (Side)(int)(Mathf.Abs(Input.Horizontal) / Input.Horizontal);

        transform.position += movement * speed * Time.fixedDeltaTime;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(jumpSpeed*Input.JumpAxis*Vector2.up);

		if (Input.Fired)
			Weapon.Attack();
    }
}
