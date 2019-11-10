using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Controller2D),typeof(InputManager))]
public class Player : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int _currHealth = MAX_HEALTH;
	public int CurrHealth {
		get => _currHealth;
		set => Dead = ((_currHealth = value) <= 0);
	}
	bool alreadyDead = false;
	public KeyCode jumpKey => GetComponent<InputManager>().jumpName;
	public KeyCode attackKey => GetComponent<InputManager>().attackName;

    public bool Dead { get; private set; } = false;
	public static readonly int MAX_HEALTH = 100;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
	public GameObject SceneManager;
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
    private Animator bodyAnimator;

    public Side lookingAt = Side.Right;
    GameObject WeaponPrefab;
    private Weapon Weapon => WeaponPrefab.GetComponent<Weapon>();

    InputResults Input2 => GetComponent<InputManager>().CurrInput;

    private bool jumpKeyPressed = false;
    private bool jumpKeyReleased = false;
    private bool attackKeyPressed = false;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        WeaponPrefab = WeaponsBase.GetComponent<BaseOfWeapons>().GetWeaponThatIs(GlobalFields.GetWeapon());
        WeaponPrefab = Instantiate(WeaponPrefab, GetComponent<Transform>());
        Weapon.Player = this;
		WeaponPrefab.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

		string player = gameObject.layer == 9 ? "setPlayer0" : "setPlayer1"; // 9 is Player0 layer
        legAnimator = gameObject.transform.Find("Leg").gameObject.GetComponent<Animator>();
        legAnimator.SetTrigger(player);
        bodyAnimator = gameObject.transform.Find("Body").gameObject.GetComponent<Animator>();
        bodyAnimator.SetTrigger(player);

        if (player == "setPlayer1")
        {
            lookingAt = Side.Left;
            gameObject.transform.localScale = new Vector3(
                        -1 * gameObject.transform.localScale.x,
                        gameObject.transform.localScale.y
                        );
        }

		switch (Weapon)
		{
			case Nokia n:
				bodyAnimator.SetTrigger("setNokia");
				break;
			case Toaster t:
				bodyAnimator.SetTrigger("setToaster");
				break;
            // TODO 
			// bodyAnimator.SetTrigger("setTentacle");
            
            default:
				throw new NotImplementedException();
		}

        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Input musi byt v Update(), aby to dobre fungovalo. Kdyby byl ve FixedUpdate(), tak se nemusi zavolat, i kdyz se ta klavesa zmackne
    void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            jumpKeyPressed = true;
        }
        if (Input.GetKeyUp(jumpKey))
        {
            jumpKeyReleased = true;
        }
        if (Input.GetKeyDown(attackKey))
        {
            attackKeyPressed = true;
        }
    }

    void FixedUpdate()
    {
		if (Dead)
		{
			if(!alreadyDead)
			{
				alreadyDead = true;
				GlobalFields.ILost(transform);
				SceneManager.GetComponent<SceneManager>().LoadScene("WeaponChoosing");
			}
		}
		else
		{
			if (controller.collisions.above || controller.collisions.below)
			{
				velocity.y = 0;
			}

			Vector2 input = new Vector2(Input2.Horizontal, 0);

            if (jumpKeyPressed)
            {
                jumpKeyPressed = false;
                if (controller.collisions.below)
                {
                    velocity.y = maxJumpVelocity;
                    legAnimator.SetTrigger("jump");
                }
            }

            if (jumpKeyReleased)
			{
                jumpKeyReleased = false;
				if (velocity.y > minJumpVelocity)
				{
					velocity.y = minJumpVelocity;
				}
			}

			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);

			if (Input2.Horizontal != 0)
			{
				Side old = lookingAt;
				lookingAt = (Side)(int)(Mathf.Abs(Input2.Horizontal) / Input2.Horizontal);
				if (old != lookingAt)
					gameObject.transform.localScale = new Vector3(
						-1 * gameObject.transform.localScale.x,
						gameObject.transform.localScale.y
						);
			}

			if (attackKeyPressed)
			{
                attackKeyPressed = false;
				if(Weapon.Attack())
                {
                    Weapon.PlaySound();
                    bodyAnimator.SetTrigger("setAttack");
                }
            }
		}
    }
}