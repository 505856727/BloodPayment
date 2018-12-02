using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.

    public float vitality = 100.0f; //活力对角色移动力和跳跃力的影响，暂时是线性的


    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public AudioClip[] taunts;              // Array of clips for when the player taunts.
    public float tauntProbability = 50f;    // Chance of a taunt happening.
    public float tauntDelay = 1f;           // Delay for when the taunt should happen.


    private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;          // Whether or not the player is grounded.
    private Animator anim;                  // Reference to the player's animator component.

    public float burnDamage = 10.0f;//burn Damage代表强度，并不代表实际伤害，实际伤害随着距离越近越高
    public float MaxDamage = 10.0f;//灼伤的最大伤害，防止暴毙
    public float burnInteval = 1.0f;//被阳光灼烧的间隔
    private float burnTime = float.MinValue;
    private DynamicLight2D.DynamicLight[] SunLights;

    public float turnLightRadius = 1.0f;
    private Animator m_anim;
    // Use this for initialization
    void Start()
    {
        //初始化阳光触发时间
        m_anim = GetComponent<Animator>();
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        if (Input.GetKeyDown(KeyCode.F) && grounded)
            TurnLight();
    }

    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce * vitality / 100.0f);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed * vitality / 100.0f, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump)
        {
            // Play a random jump audio clip.
            int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * vitality / 100.0f));

            //保证只跳了一次
            m_anim.SetTrigger("jump");

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void TurnLight()//关闭或者打开灯光
    {
        //检测周围是否有灯光,灯光必须为Trigger，要覆盖才有效果
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach(var collider in colliders)
        {
            if (collider.CompareTag("TurnOn"))
            {
                collider.GetComponent<TurnOn>().turnOn();
            }
        }
    }

    //被吸血
    public void TakeSuckBlood(float suckDamage)
    {
        vitality -= suckDamage;
    }
}
