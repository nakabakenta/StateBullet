using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public int firstAttack;    //�����U����
    public int currentAttack;  //���݂̍U����

    public float firstSpeed;    //�����e��
    public float currentSpeed;  //���݂̒e��

    public string BulletMaster;     //�e�̎�����̃^�O�ۑ�

    //�v���C���[�X�N���v�g���擾
    //�}�[�W��A�����͏���������
    public TestPlayer testPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //�����l��ݒ�
        currentAttack = firstAttack;  //�U����
        currentSpeed = firstSpeed;    //�e��
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
