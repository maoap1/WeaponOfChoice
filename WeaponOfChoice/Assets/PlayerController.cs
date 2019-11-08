using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 200f;
    public float jumpSpeed = 200f;
	public GameObject WeaponsBase;

	public GameObject WeaponPrefab;
	private Weapon Weapon => WeaponPrefab.GetComponent<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
		WeaponPrefab = WeaponsBase.GetComponent<BaseOfWeapons>().GetWeaponThatIs(GlobalFields.GetWeapon());
    }
        

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal0"), 0, 0);
        transform.position += movement * speed * Time.fixedDeltaTime;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(jumpSpeed*Input.GetAxis("Jump0")*Vector2.up);
    }
}
