using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHP;     //最大HP
    public float currentHP; //現在のHP

    public float moveSpeed;     //移動速度
    public float originalSpeed; //基本移動速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //現在のHPを最大値に設定
        currentHP = maxHP;
        //moveSpeedの値を基本移動速度に設定
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
