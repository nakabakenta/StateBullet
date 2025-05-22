using UnityEngine;

public class PlayerBulletBase : BulletBase
{
    [Header("�g�p���̑����e")]
    public int useElement;  //�g�p�����e
    [Header("�e�����e�̉Η�")]
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

        //�����e��ʏ�e�ɐݒ�
        currentAttack = (int)Element.FIRE;
        useElement = (int)Element.FIRE;
    }

    public void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɓ��������Ƃ�
        if (other.gameObject.tag == "Enemy")
        {
            //�v���C���[�̃X�N���v�g���擾���āAHP�����炷
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
        //BulletMaster�ȊO�̃I�u�W�F�N�g�ɓ��������Ƃ�
        if (other.gameObject.tag != BulletMaster)
        {
            //���g������
            Destroy(gameObject);
        }
    }
}
