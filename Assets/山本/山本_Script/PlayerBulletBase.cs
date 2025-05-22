using UnityEngine;

public class PlayerBulletBase : BulletBase
{
    [Header("使用中の属性弾")]
    public int useElement;  //使用属性弾
    [Header("各属性弾の火力")]
    public int fire;
    public int water;
    public int wind;
    public int explosion;
    public int metal;
    public int grass;
    public int normal;

    public enum  Element
    {
        FIRE,
        WATER,
        WIND,
        EXPLOSION,
        METAL,
        GRASS,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        //初期弾を通常弾に設定
        currentAttack = (int)Element.FIRE;
        useElement = (int)Element.FIRE;
    }

    public void OnTriggerEnter(Collider other)
    {
        //プレイヤーに当たったとき
        if (other.gameObject.tag == "Enemy")
        {
            //プレイヤーのスクリプトを取得して、HPを減らす
            character = other.gameObject.GetComponent<CharacterBase>();
            if (useElement == (int)Element.FIRE)
            {
                currentAttack = fire;
                character.isFire = true;
            }
            else if (useElement == (int)Element.WATER)
            {
                currentAttack = water;
                character.isWater = true;
            }
            else if (useElement == (int)Element.WIND)
            {
                currentAttack = wind;
                character.isWind = true;
            }
            else if (useElement == (int)Element.EXPLOSION)
            {
                currentAttack = explosion;
            }
            else if (useElement == (int)Element.METAL)
            {
                currentAttack = metal;
                character.isMetal = true;
            }
            else if (useElement == (int)Element.GRASS)
            {
                currentAttack = grass;
                character.isGrass = true;
            }

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
