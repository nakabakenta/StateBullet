using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public float firstAttack;    //�����U����
    public float currentAttack;  //���݂̍U����

    public float firstSpeed;    //�����e��
    public float currentSpeed;  //���݂̒e��

    Vector3 bulletVec;

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
        if (other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
}
