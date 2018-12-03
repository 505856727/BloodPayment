using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Human_PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    public bool isBeingSucked = false;      //被吸血的时候无法移动和跳跃

    public float vitality = 100.0f; //活力对角色移动力和跳跃力的影响，暂时是线性的
    //public float moveForce = 365f;          // Amount of force added to move the player left and right.
    //public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    //public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    private Animator anim;                  // Reference to the player's animator component.

    public float burnDamage = 10.0f;//burn Damage代表强度，并不代表实际伤害，实际伤害随着距离越近越高
    public float MaxDamage = 10.0f;//灼伤的最大伤害，防止暴毙
    public float burnInteval = 1.0f;//被阳光灼烧的间隔
    private float burnTime = float.MinValue;
    private DynamicLight2D.DynamicLight[] SunLights;

    public float turnLightRadius = 1.0f;
    private Animator m_anim;

    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public AudioClip[] switchClips;           // Array of clips for when the player jumps.

    //血条，或者渴望值的Slider
    public Slider hpSlider;

    void Start()
    {
        //初始化阳光触发时间
        m_anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
    //    grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

    //    if (isBeingSucked)
    //        return;
    //    // If the jump button is pressed and the player is grounded then the player should jump.
    //    if (Input.GetButtonDown("Jump") && grounded)
    //        jump = true;

    //    if (Input.GetKeyDown(KeyCode.F) && grounded)
    //        TurnTrigger();
    //}

    //void FixedUpdate()
    //{
    //    if (isBeingSucked)
    //        return;
    //    // Cache the horizontal input.
    //    float h = Input.GetAxis("Horizontal");

    //    // The Speed animator parameter is set to the absolute value of the horizontal input.
    //    anim.SetFloat("Speed", Mathf.Abs(h));

    //    // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
    //    if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
    //        // ... add a force to the player.
    //        GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce * vitality / 100.0f);

    //    // If the player's horizontal velocity is greater than the maxSpeed...
    //    if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
    //        // ... set the player's velocity to the maxSpeed in the x axis.
    //        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed * vitality / 100.0f, GetComponent<Rigidbody2D>().velocity.y);

    //    // If the input is moving the player right and the player is facing left...
    //    if (h > 0 && !facingRight)
    //        // ... flip the player.
    //        Flip();
    //    // Otherwise if the input is moving the player left and the player is facing right...
    //    else if (h < 0 && facingRight)
    //        // ... flip the player.
    //        Flip();

    //    // If the player should jump...
    //    if (jump)
    //    {
    //        // Play a random jump audio clip.
    //        int i = Random.Range(0, jumpClips.Length);
    //        //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

    //        // Add a vertical force to the player.
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * vitality / 100.0f));

    //        //保证只跳了一次
    //        m_anim.SetTrigger("jump");

    //        // Make sure the player can't jump again until the jump conditions from Update are satisfied.
    //        jump = false;
    //    }
    //}

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public float speed;
    public bool isjump;
    public float jumpspeed;
    void Update()
    {
        if (isBeingSucked)
            return;
        MoveCharacter();
        if (Input.GetKeyDown(KeyCode.F) && !isjump)
            TurnTrigger();
    }

    void MoveCharacter()
    {
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h < 0)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (h > 0)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        if (Input.GetKeyDown(KeyCode.Space) && isjump == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpspeed * Time.deltaTime));
            //保证只跳了一次
            m_anim.SetTrigger("jump");
            isjump = true;
            //音效
            int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isjump = false;
        }
    }

    void TurnTrigger()//关闭或者打开机关
    {
        anim.SetTrigger("RaiseHand");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (var collider in colliders)
        {
            //检测周围是否有灯光,灯光必须为Trigger，要覆盖才有效果
            if (collider.CompareTag("TurnOn"))
            {
                collider.GetComponent<TurnOn>().turnOn();
                int i = Random.Range(0, switchClips.Length);
                AudioSource.PlayClipAtPoint(switchClips[i], transform.position);
            }
        }
    }

    //被吸血，有一定时间间隔地触发,只有当动画处于stand的时候才可以吸血
    public bool TakeSuckBlood(float suckDamage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("girlstand") || anim.GetCurrentAnimatorStateInfo(0).IsName("girlblood"))
        {
            //if (hpSlider)
            //{
            //    hpSlider.value = vitality;
            //}
            //vitality -= suckDamage;
            changeVitality((-1) * suckDamage);
            anim.SetBool("beingSuckedBlood", true);
            isBeingSucked = true;
            return true;
        }
        else return false;//角色在移动 无法吸血
    }

    public void changeVitality(float amount)
    {
        
        vitality += amount;
        if (vitality > 100)
            vitality = 100;
        if (hpSlider)
        {
            hpSlider.value = vitality;
        }
        if(vitality <0)
            SceneManager.LoadScene("GameLose");
    }
}
