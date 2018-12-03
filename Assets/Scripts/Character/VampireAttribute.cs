using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAttribute : MonoBehaviour {
    public float burnDamage = 10.0f;//burn Damage代表强度，并不代表实际伤害，实际伤害随着距离越近越高
    public float MaxDamage = 10.0f;//灼伤的最大伤害，防止暴毙
    public float burnInteval = 1.0f;//被阳光灼烧的间隔
    private float burnTime = float.MinValue;
    private DynamicLight2D.DynamicLight[] SunLights;
    //动画状态机
    public Animator m_anim;
    //音效
    public AudioClip[] beLightedClip;
    // Use this for initialization
    void Start () {
        //初始化阳光触发时间
        GameObject SunLightsTransform = GameObject.Find("SunLights");
        if (SunLightsTransform)
        {
            SunLights = SunLightsTransform.GetComponentsInChildren<DynamicLight2D.DynamicLight>();
            foreach (var light in SunLights)
            {
                light.OnEnterFieldOfView += OnEnterSunLight;
                light.OnExitFieldOfView += OnExitSunLight;
                light.InsideFieldOfViewEvent += OnInsideSunLight;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnterSunLight(GameObject g, DynamicLight2D.DynamicLight light)//见到阳光
    {
        if (gameObject.GetInstanceID() == g.GetInstanceID())
        {
            Debug.Log("OnEnterSunshine");
            //GetComponent<SpriteRenderer>().color = Color.red;
            burnTime = Time.time;//开始计时
            m_anim.SetBool("beLighted", true);
        }
    }

    public void OnInsideSunLight(GameObject[] go, DynamicLight2D.DynamicLight light)
    {
        foreach (GameObject _go in go)
            if (gameObject.GetInstanceID() == _go.GetInstanceID())
            {
                if (Time.time - burnTime >= burnInteval)
                {
                    //burn
                    Debug.Log("Burn ");
                    
                    //扣血跟距离挂钩
                    //Debug.Log((gameObject.transform.position - light.transform.position));
                    float rawDamage = burnDamage / (gameObject.transform.position - light.transform.position).sqrMagnitude;
                    //Debug.Log(Mathf.Clamp(rawDamage, 0.0f, MaxDamage));
                    burnTime = Time.time;
                    int i = Random.Range(0, beLightedClip.Length);
                    AudioSource.PlayClipAtPoint(beLightedClip[i], transform.position);
                }
                break;
            }
    }

    void OnExitSunLight(GameObject g, DynamicLight2D.DynamicLight light)
    {
        if (gameObject.GetInstanceID() == g.GetInstanceID())
        {
            Debug.Log("OnExitSunshine");
            GetComponent<SpriteRenderer>().color = Color.white;
            m_anim.SetBool("beLighted", false);
        }
    }
}
