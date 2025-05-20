using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //��b�\��
    public float maxHP;         //�ő�HP
    public float currentHP;     //���݂�HP
    public float moveSpeed;     //�ړ����x
    public float originalSpeed; //��{�ړ����x

    //�U���֘A
    public float shotInterval;        //�e�̔��ˊԊu
    public float minInterval;         //�Ԋu�̍ŒZ�l
    public float maxInterval;         //�Ԋu�̍Œ��l
    public float shotTimer;           //���˂܂ł̌v���p
    public float bulletSpeed;         //�e��
    public GameObject[] bulletPrefab; //�e��Prefab�����邽�߂̕ϐ�
    public float bulletPosY;          //�e�𐶐�����Ƃ��̈ʒu�����p
    
    //��Ԉُ�֘A
    public float stateCount;     //�������Ԃ̌v���p
    public float sustainability; //��Ԉُ�̎�������
    public bool isState;         //��Ԉُ�ɂȂ��Ă��邩
    //�X���b�v�_���[�W�̕p�x
    public float frequency;  //�J��Ԃ��Ԋu
    public float moldFre;    //�J�r�̊Ԋu
    public float corrFre;    //���H�̊Ԋu
    public float burnFre;    //�R�Ă̊Ԋu
    public float actiFre;    //�������̊Ԋu
    public bool setDuration; //�Ԋu��ݒ肵�����ǂ���(���x���Đݒ肵�Ȃ��悤��)
    //��Ԉُ�̔���
    public bool isMold;      //�J�r��Ԃ��ǂ���
    public bool isCorrosion; //���H��Ԃ��ǂ���
    public bool isBurning;   //�R�ď�Ԃ��ǂ���
    public bool isActive;    //��������Ԃ��ǂ���
    //�����l
    public float mold;      //�J�r
    public float corrosion; //���H
    public float burning;   //�R��
    public float active;    //������
    //�_���[�W�l�@�΁@�@���@�@���@�@�@���j�@�@�@�����@�@���@�@�ʏ�@
    public float fire, water, wind, explosion, metal, grass, normal;
    //�t�^���̑���
    public bool isFire, isWater, isWind, isMetal, isGrass;

    //��_���[�W�֘A
    public bool isDamage;

    //���X�N���v�g(�}�[�W��A�u������)
    public Environment environment;
    //�G�̒e�̃X�N���v�g
    public EnemyBulletBase enemyBullet;

    //�f�o�b�O�p
    public bool test;
    public bool up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        //���݂�HP���ő�l�ɐݒ�
        currentHP = maxHP;
        //moveSpeed�̒l����{�ړ����x�ɐݒ�
        originalSpeed = moveSpeed;
        //��Ԉُ�̎������Ԃ�ݒ�(30�b)
        sustainability = 30.0f;
        //���ˊԊu�̏���ݒ�
        shotInterval = Random.Range(minInterval, maxInterval);
        //�������擾
        environment=GameObject.Find("VirtualEnvironment").GetComponent<Environment>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //�̗͂̊Ǘ�
        HPManager();

        //�U���̊Ԋu
        AttackInterval();

        //�����̕t�^�y�я�Ԉُ�t�^�̊Ǘ�
        StateEnchant();

        //��Ԉُ�̊Ǘ�
        StateManager();

        //�f�o�b�O�p
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

    //HP�̊Ǘ�
    public void HPManager()
    {
        //�̗͂�0�����ɂȂ�Ȃ��悤�ɂ��A
        //0�ȉ��ɂȂ������������
        if (currentHP <= 0.0f) 
        {
            currentHP = 0.0f;

            Destroy(gameObject);
        }
        //�̗͂��ő�l������Ȃ��悤�ɂ���
        if (currentHP >= maxHP) 
        {
            currentHP = maxHP;
        }
    }

    //�U���̊Ԋu
    public void AttackInterval()
    {
        //���˂܂ł̎��Ԃ��v��
        shotTimer += Time.deltaTime;

        //shotTimer��shotInterval�̒l�������ɂȂ�����
        if(shotTimer >= shotInterval)
        {
            //�e�𔭎�
            Shot();
            //���ˊԊu���Đݒ�
            shotInterval = Random.Range(minInterval, maxInterval);
            //�^�C�}�[��0�ɖ߂�
            shotTimer = 0;
        }
    }

    //���ˏ���
    void Shot()
    {
        Vector3 bulletPos = this.transform.position;
        bulletPos.y += bulletPosY;
        GameObject bullet = Instantiate(bulletPrefab[0], bulletPos, Quaternion.identity); //�e�𐶐�
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed); //�L�����N�^�[�������Ă�������ɒe�ɗ͂�������
    }

    //����Ԃɂ��ω����Ǘ�
    public void Environment()
    {
        //���J
        
        //�ҏ�

        //�\��

        //���d��

        //��d��

        //�L��

    }

    //�����̕t�^�y�я�Ԉُ�t�^�̊Ǘ�
    public void StateEnchant()
    {
        //��Ԉُ��t�^���邽�߂̏���
        {
            if (isWater && isWind && isGrass)//�J�r
                isMold = true;
            else if (isWater && isWind && isMetal)//���H
                isCorrosion = true;
            else if (isFire && isGrass)//�R��
                isBurning = true;
            else if (isWater && isGrass)//������
                isActive = true;
        }

        //�����t�^�ɂ����鑊��
        {
            //�΂Ɛ��͋����ł����A�����D�悳���
            if (isWater)
                isFire = false;
        }

        //��Ԉُ�ɂȂ������Ƃ��m�F����t���O��true�ɂ���
        if (isMold || isCorrosion || isBurning || isActive)
            isState = true;

        //��Ԉُ�ɂȂ����Ƃ��A�S�����̕t�^����������
        if (isState)
        {
            isFire = false;
            isWater = false;
            isWind = false;
            isMetal = false;
            isGrass = false;
        }
    }

    //��Ԉُ�̎��Ԍn�̊Ǘ�
    public void StateManager()
    {
        //�Ȃ�炩�̏�Ԉُ�ɂȂ����Ƃ�
        if (isState) 
        {
            //�������Ԃ̌v�����J�n
            stateCount += Time.deltaTime;
            //���݂̃J�E���g���������Ԉȏ�ɂȂ�����J�r����������
            if (stateCount >= sustainability)
                isState = false;
            //�Ԋu�̐ݒ�
            if (!setDuration)
            {
                //���Ԃ̐ݒ�
                //�J�r
                if (isMold)  
                    frequency = moldFre;
                //���H
                if (isCorrosion) 
                    frequency = corrFre;
                //�R��
                if (isBurning)
                    frequency = burnFre;
                //������
                if (isActive)
                    frequency = actiFre;
                //�ݒ����������Ȃ��悤�ɂ��邽��
                setDuration = true; 
            }
            //�_���[�W�Ԋu�̌v��
            frequency -= Time.deltaTime;
            //��������
            if (frequency <= 0.0f)
            {
                //�J�r
                if (isMold)
                    MoldDamage();
                //���H
                if (isCorrosion)
                    CorrDamage();
                //�R��
                if (isBurning)
                    BurningDamage();
                //������
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

    //�J�r��Ԃ̏���
    public void MoldDamage()
    {
        currentHP -= maxHP * (mold / 100);
        frequency = moldFre;
    }
    //���H��Ԃ̏���
    public void CorrDamage()
    {
        currentHP -= maxHP * (corrosion / 100);
        frequency = corrFre;
    }
    //�R�ď�Ԃ̏���
    public void BurningDamage()
    {
        currentHP -= maxHP * (burning / 100);
        //�U���͂��㏸�����鏈��������
    }
    //��������Ԃ̏���
    public void ActiveRecovery()
    {
        currentHP += maxHP * (active / 100);
        frequency = actiFre;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PBullet"))
        {
            //�_���[�W���󂯂�
            isDamage = true;
        }
    }
}
