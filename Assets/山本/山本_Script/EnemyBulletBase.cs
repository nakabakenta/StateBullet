using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public float firstAttack;    //�����U����
    public float currentAttack;  //���݂̍U����

    public float firstSpeed;    //�����e��
    public float currentSpeed;  //���݂̒e��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�����l��ݒ�
        currentAttack = firstAttack;  //�U����
        currentSpeed = firstSpeed;    //�e��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
