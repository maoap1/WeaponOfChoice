using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InputManager))]

[RequireComponent(typeof(Controller2D),typeof(InputManager))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    public GameObject WeaponsBase;

    private Animator legAnimator;

    public Side LookingAt { get; private set; } = Side.Left;
    GameObject WeaponPrefab;
    private Weapon Weapon => WeaponPrefab.GetComponent<Weapon>();

    InputResults Input => GetComponent<InputManager>().CurrInput;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        WeaponPrefab = WeaponsBase.GetComponent<BaseOfWeapons>().GetWeaponThatIs(GlobalFields.GetWeapon());
        WeaponPrefab = Instantiate(WeaponPrefab, GetComponent<Transform>());
        Weapon.pc = this;

        legAnimator = gameObject.transform.Find("Leg").gameObject.GetComponent<Animator>();
        legAnimator.SetTrigger(gameObject.layer == 9 ? "setPlayer0": "setPlayer1"); // 9 is Player0 layer
    }


    void FixedUpdate()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.Horizontal, 0);

        if (Input.StartJumping && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            legAnimator.SetTrigger("jump");
        }

        if (Input.EndJumping)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

		if(Input.Horizontal != 0)
			LookingAt = (Side)(int)(Mathf.Abs(Input.Horizontal) / Input.Horizontal);

        if (Input.Fired)
            Weapon.Attack();
    }
}