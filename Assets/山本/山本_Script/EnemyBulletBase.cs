using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBase : BulletBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter(Collider other)
    {
        //プレイヤーに当たったとき
        if (other.gameObject.tag == "Player")
        {
            //プレイヤーのスクリプトを取得して、HPを減らす
            character = other.gameObject.GetComponent<TestPlayer>();
            character.currentHP -= currentAttack;
        }
        //BulletMaster以外のオブジェクトに当たったとき
        if (other.gameObject.tag != BulletMaster)
        {
            //自身を消去
            Destroy(gameObject);
        }
    }
}
