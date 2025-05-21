using UnityEngine;

public class EnemyBase : CharacterBase
{ 
    [Header("攻撃関連")]
    public float shotInterval;        //弾の発射間隔
    public float minInterval;         //間隔の最短値
    public float maxInterval;         //間隔の最長値
    public float shotTimer;           //発射までの計測用
    public float bulletSpeed;         //弾速
    public GameObject[] bulletPrefab; //弾のPrefabを入れるための変数
    public float bulletPosY;          //弾を生成するときの位置調整用

    [Header("ダメージフラグ")]
    public bool isDamage; //ダメージを受けたかどうか

    [Header("スクリプト参照")]
    public EnemyBulletBase enemyBullet; //敵の弾

    //デバッグ用
    public bool test;
    public bool up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        //発射間隔の初回設定
        shotInterval = Random.Range(minInterval, maxInterval);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //体力の管理
        HPManager();

        //攻撃の間隔
        AttackInterval();

        //デバッグ用
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (up)
                {
                    if (!test)
                    {
                        Time.timeScale = 0;
                        test = true;
                        up = false;
                    }
                    else
                    {
                        Time.timeScale = 1;
                        test = false;
                        up = false;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                up = true;
            }
        }  
    }

    //HPの管理
    public void HPManager()
    {
        //体力が0未満にならないようにし、
        //0以下になったら消去する
        if (currentHP <= 0.0f) 
        {
            currentHP = 0.0f;

            Destroy(gameObject);
        }
        //体力が最大値を上回らないようにする
        if (currentHP >= maxHP) 
        {
            currentHP = maxHP;
        }
    }

    //攻撃の間隔
    public void AttackInterval()
    {
        //発射までの時間を計測
        shotTimer += Time.deltaTime;

        //shotTimerとshotIntervalの値が同じになったら
        if(shotTimer >= shotInterval)
        {
            //弾を発射
            Shot();
            //発射間隔を再設定
            shotInterval = Random.Range(minInterval, maxInterval);
            //タイマーを0に戻す
            shotTimer = 0;
        }
    }

    //発射処理
    void Shot()
    {
        Vector3 bulletPos = this.transform.position;
        bulletPos.y += bulletPosY;
        GameObject bullet = Instantiate(bulletPrefab[0], bulletPos, Quaternion.identity); //弾を生成
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed); //キャラクターが向いている方向に弾に力を加える
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PBullet"))
        {
            //ダメージを受けた
            isDamage = true;
        }
    }
}
