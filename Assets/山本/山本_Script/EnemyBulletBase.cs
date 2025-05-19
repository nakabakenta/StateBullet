using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public int firstAttack;    //初期攻撃力
    public int currentAttack;  //現在の攻撃力

    public float firstSpeed;    //初期弾速
    public float currentSpeed;  //現在の弾速

    public string BulletMaster;     //弾の持ち主のタグ保存

    //プレイヤースクリプトを取得
    //マージ後、ここは書き換える
    public TestPlayer testPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //初期値を設定
        currentAttack = firstAttack;  //攻撃力
        currentSpeed = firstSpeed;    //弾速
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            testPlayer = other.gameObject.GetComponent<TestPlayer>();
            testPlayer.currentHp -= currentAttack;

        }
        if (other.gameObject.tag != BulletMaster)
        {
            Destroy(gameObject);
        }
    }
}
