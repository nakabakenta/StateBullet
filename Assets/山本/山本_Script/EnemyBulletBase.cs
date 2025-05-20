using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public int firstAttack;    //初期攻撃力
    public int currentAttack;  //現在の攻撃力

    public string BulletMaster;     //弾の持ち主のタグ保存

    //プレイヤースクリプトを取得
    //マージ後、ここは書き換える
    public TestPlayer testPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //初期値を設定
        currentAttack = firstAttack;  //攻撃力
    }

    public void OnTriggerEnter(Collider other)
    {
        //プレイヤーに当たったとき
        if (other.gameObject.tag == "Player")
        {
            //プレイヤーのスクリプトを取得して、HPを減らす
            testPlayer = other.gameObject.GetComponent<TestPlayer>();
            testPlayer.currentHp -= currentAttack;

        }
        //BulletMaster以外のオブジェクトに当たったとき
        if (other.gameObject.tag != BulletMaster)
        {
            //自身を消去
            Destroy(gameObject);
        }
    }
}
