using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireControl : MonoBehaviour {
    public float hp;//最大生命值
    public float suck_interval = 0.5f;
    public float suckDamage = 5.0f;
    public float infactcost;
    public float movespeed;

    //血条，或者渴望值的Slider
    public Slider hpSlider;
    public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public Image m_FillImage;                           // The image component of the slider.
    public Animator m_anim;
    //音效
    public AudioClip[] suckBloodClip;

    private float current_hp;
    private float suckTime = float.MinValue;
    private Vector3 originScale;

    private SpriteRenderer[] bodys;//用来改变自身所有sprited的alpha值
    private float originAlpha = 0.47f;

    private void Start()
    {
        originScale = transform.localScale;
        current_hp = hp;
        if (transform.Find("vampfly_8"))
        {
            m_anim = transform.Find("vampfly_8").GetComponent<Animator>();
        }
        bodys = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        VampireAttack();
    }
    private void FixedUpdate()
    {
        VampireMove();
        VampireSuck();
    }
    /// <summary>
    /// 吸血鬼移动
    /// </summary>
    void VampireMove()
    {

        //GetComponent<Rigidbody2D>().MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10));
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;//人物朝鼠标方向移动
        GetComponent<Rigidbody2D>().velocity = direction.normalized * movespeed;//移动的速度
        
        if (direction.x >= 0.1f)
        {
            transform.localScale = new Vector3(originScale.x*(-1), originScale.y, originScale.z);
        }
        else if (direction.x <= -0.1f)
        {
            transform.localScale = originScale;
        }
    }

    /// <summary>
    /// 吸血鬼吸血
    /// </summary>
    void VampireSuck()
    {
        if (Input.GetMouseButton(1))
        {
            if (Time.time - suckTime > suck_interval)
            {
                print("suck!");
                //检测周围是否Blood
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                if (m_anim)
                    m_anim.SetBool("suckBlood", false);
                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Blood"))
                    {
                        if (collider.GetComponent<Human_PlayerController>().TakeSuckBlood(suckDamage))//如果吸血成功
                        {
                            suckTime = Time.time;
                            if (m_anim)
                                m_anim.SetBool("suckBlood", true);

                            DecreaseHP((-1) * suckDamage);
                            int i = Random.Range(0, suckBloodClip.Length);
                            AudioSource.PlayClipAtPoint(suckBloodClip[i], transform.position);
                            break;
                        }
                    }
                }
            }
        }
        else if (m_anim)
            m_anim.SetBool("suckBlood", false);
    }

    /// <summary>
    /// 吸血鬼攻击
    /// </summary>
    void VampireAttack()
    {
        if (Input.GetMouseButton(0))
        {
            foreach(var body in bodys)
            {
                Color fillColor = body.color;
                fillColor.a = 1;
                body.color = Color.Lerp(body.color, fillColor, 0.51f);
            }
            gameObject.layer = 10;
            //GetComponent<Collider2D>().isTrigger = false;
            GetComponent<BoxCollider2D>().enabled = true;
            DecreaseHP(infactcost);
        }
        else if (Input.GetMouseButtonUp(0))
        {           
            //GetComponent<Collider2D>().isTrigger = true;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = 11;
            foreach (var body in bodys)
            {
                Color fillColor = body.color;
                fillColor.a = originAlpha;
                body.color = Color.Lerp(body.color, fillColor, 1f);
            }
        }
    }

    public void DecreaseHP(float count)
    {
        current_hp -= count;
        if (hpSlider)
        {
            hpSlider.value = current_hp;
            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, current_hp / hp);
        }
    }
}
