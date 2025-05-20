using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //基礎能力
    public float maxHP;         //最大HP
    public float currentHP;     //現在のHP
    public float moveSpeed;     //移動速度
    public float originalSpeed; //基本移動速度

    //攻撃関連
    public float shotInterval;        //弾の発射間隔
    public float minInterval;         //間隔の最短値
    public float maxInterval;         //間隔の最長値
    public float shotTimer;           //発射までの計測用
    public float bulletSpeed;         //弾速
    public GameObject[] bulletPrefab; //弾のPrefabを入れるための変数
    public float bulletPosY;          //弾を生成するときの位置調整用
    
    //状態異常関連
    public float stateCount;     //持続時間の計測用
    public float sustainability; //状態異常の持続時間
    public bool isState;         //状態異常になっているか
    //スリップダメージの頻度
    public float frequency;  //繰り返す間隔
    public float moldFre;    //カビの間隔
    public float corrFre;    //腐食の間隔
    public float burnFre;    //燃焼の間隔
    public float actiFre;    //活性化の間隔
    public bool setDuration; //間隔を設定したかどうか(何度も再設定しないように)
    //状態異常の判別
    public bool isMold;      //カビ状態かどうか
    public bool isCorrosion; //腐食状態かどうか
    public bool isBurning;   //燃焼状態かどうか
    public bool isActive;    //活性化状態かどうか
    //割合値
    public float mold;      //カビ
    public float corrosion; //腐食
    public float burning;   //燃焼
    public float active;    //活性化
    //ダメージ値　火　　水　　風　　　爆破　　　金属　　草　　通常　
    public float fire, water, wind, explosion, metal, grass, normal;
    //付与中の属性
    public bool isFire, isWater, isWind, isMetal, isGrass;

    //被ダメージ関連
    public bool isDamage;

    //環境スクリプト(マージ後、置き換え)
    public Environment environment;
    //敵の弾のスクリプト
    public EnemyBulletBase enemyBullet;

    //デバッグ用
    public bool test;
    public bool up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        //現在のHPを最大値に設定
        currentHP = maxHP;
        //moveSpeedの値を基本移動速度に設定
        originalSpeed = moveSpeed;
        //状態異常の持続時間を設定(30秒)
        sustainability = 30.0f;
        //発射間隔の初回設定
        shotInterval = Random.Range(minInterval, maxInterval);
        //環境情報を取得
        environment=GameObject.Find("VirtualEnvironment").GetComponent<Environment>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //体力の管理
        HPManager();

        //攻撃の間隔
        AttackInterval();

        //属性の付与及び状態異常付与の管理
        StateEnchant();

        //状態異常の管理
        StateManager();

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

    //環境状態による変化を管理
    public void Environment()
    {
        //豪雨
        
        //猛暑

        //暴風

        //高重力

        //低重力

        //豊穣

    }

    //属性の付与及び状態異常付与の管理
    public void StateEnchant()
    {
        //状態異常を付与するための条件
        {
            if (isWater && isWind && isGrass)//カビ
                isMold = true;
            else if (isWater && isWind && isMetal)//腐食
                isCorrosion = true;
            else if (isFire && isGrass)//燃焼
                isBurning = true;
            else if (isWater && isGrass)//活性化
                isActive = true;
        }

        //属性付与における相性
        {
            //火と水は共存できず、水が優先される
            if (isWater)
                isFire = false;
        }

        //状態異常になったことを確認するフラグをtrueにする
        if (isMold || isCorrosion || isBurning || isActive)
            isState = true;

        //状態異常になったとき、全属性の付与を解除する
        if (isState)
        {
            isFire = false;
            isWater = false;
            isWind = false;
            isMetal = false;
            isGrass = false;
        }
    }

    //状態異常の時間系の管理
    public void StateManager()
    {
        //なんらかの状態異常になったとき
        if (isState) 
        {
            //持続時間の計測を開始
            stateCount += Time.deltaTime;
            //現在のカウントが持続時間以上になったらカビを解除する
            if (stateCount >= sustainability)
                isState = false;
            //間隔の設定
            if (!setDuration)
            {
                //時間の設定
                //カビ
                if (isMold)  
                    frequency = moldFre;
                //腐食
                if (isCorrosion) 
                    frequency = corrFre;
                //燃焼
                if (isBurning)
                    frequency = burnFre;
                //活性化
                if (isActive)
                    frequency = actiFre;
                //設定を何回もしないようにするため
                setDuration = true; 
            }
            //ダメージ間隔の計測
            frequency -= Time.deltaTime;
            //割合処理
            if (frequency <= 0.0f)
            {
                //カビ
                if (isMold)
                    MoldDamage();
                //腐食
                if (isCorrosion)
                    CorrDamage();
                //燃焼
                if (isBurning)
                    BurningDamage();
                //活性化
                if (isActive)
                    ActiveRecovery();
            }
        }
        else
        {
            setDuration = false;
            isMold = false;
            isCorrosion = false;
            isBurning = false;
            isActive = false;
        }
    }

    //カビ状態の処理
    public void MoldDamage()
    {
        currentHP -= maxHP * (mold / 100);
        frequency = moldFre;
    }
    //腐食状態の処理
    public void CorrDamage()
    {
        currentHP -= maxHP * (corrosion / 100);
        frequency = corrFre;
    }
    //燃焼状態の処理
    public void BurningDamage()
    {
        currentHP -= maxHP * (burning / 100);
        //攻撃力を上昇させる処理を書く
    }
    //活性化状態の処理
    public void ActiveRecovery()
    {
        currentHP += maxHP * (active / 100);
        frequency = actiFre;
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
