using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHP;     //最大HP
    public float currentHP; //現在のHP

    public float moveSpeed;     //移動速度
    public float originalSpeed; //基本移動速度

    public float stateCount;     //持続時間の計測用
    public float sustainability; //状態異常の持続時間

    public bool isMold; //カビ状態かどうか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //現在のHPを最大値に設定
        currentHP = maxHP;
        //moveSpeedの値を基本移動速度に設定
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    
    //状態異常の管理
    public void StateManager()
    {
        //カビ状態
        if (isMold)
        {
            sustainability = 30.0f;
            InvokeRepeating("MoldDamage", 2.0f, 2.0f);
        }
    }

    //カビ状態の処理
    public void MoldDamage()
    {
        currentHP -= maxHP * 0.005f;
    }
}
