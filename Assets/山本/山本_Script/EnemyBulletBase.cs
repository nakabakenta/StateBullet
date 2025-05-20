using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public int firstAttack;    //�����U����
    public int currentAttack;  //���݂̍U����

    public string BulletMaster;     //�e�̎�����̃^�O�ۑ�

    //�v���C���[�X�N���v�g���擾
    //�}�[�W��A�����͏���������
    public TestPlayer testPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //�����l��ݒ�
        currentAttack = firstAttack;  //�U����
    }

    public void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɓ��������Ƃ�
        if (other.gameObject.tag == "Player")
        {
            //�v���C���[�̃X�N���v�g���擾���āAHP�����炷
            testPlayer = other.gameObject.GetComponent<TestPlayer>();
            testPlayer.currentHp -= currentAttack;

        }
        //BulletMaster�ȊO�̃I�u�W�F�N�g�ɓ��������Ƃ�
        if (other.gameObject.tag != BulletMaster)
        {
            //���g������
            Destroy(gameObject);
        }
    }
}
