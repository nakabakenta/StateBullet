using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHP;     //�ő�HP
    public float currentHP; //���݂�HP

    public float moveSpeed;     //�ړ����x
    public float originalSpeed; //��{�ړ����x

    public float stateCount;     //�������Ԃ̌v���p
    public float sustainability; //��Ԉُ�̎�������

    
    public bool isState; //��Ԉُ�ɂȂ��Ă��邩

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
    public float mold;       //�J�r
    public float corrosion;  //���H
    public float burning;    //�R��
    public float active;     //������

    //�_���[�W�l�@�΁@�@���@�@���@�@�@���j�@�@�@�����@�@���@�@�ʏ�@
    public float fire, water, wind, explosion, metal, grass, normal;
    //�t�^���̑���
    public bool isFire, isWater, isWind, isMetal, isGrass;


    //�f�o�b�O�p
    public bool test;
    public bool up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //���݂�HP���ő�l�ɐݒ�
        currentHP = maxHP;
        //moveSpeed�̒l����{�ړ����x�ɐݒ�
        originalSpeed = moveSpeed;
        //��Ԉُ�̎������Ԃ�ݒ�(30�b)
        sustainability = 30.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        //�̗͂̊Ǘ�
        HPManager();

        //�����̕t�^�y�я�Ԉُ�t�^�̊Ǘ�
        StateEnchant();

        //��Ԉُ�̊Ǘ�
        StateManager();

        //�f�o�b�O�p
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

    public void OnTriggerEnter(Collider other)
    {
        //�󂯂��e���ƂɁA�󂯂�_���[�W��ς��A
        //������t�^�ł���ꍇ�A�t�^����
        if(other.CompareTag("Fire")) //��
        {
            currentHP -= fire;
            isFire = true;
        }
        if (other.CompareTag("Water"))//��
        {
            currentHP -= water;
            isWater = true;
        }
        if (other.CompareTag("Wind"))//��
        {
            currentHP -= wind;
            isWind = true;
        }
        if (other.CompareTag("Explosion"))//���j
        {
            currentHP -= explosion;
        }
        if (other.CompareTag("Metal"))//����
        {
            currentHP -= metal;
            isMetal = true;
        }
        if (other.CompareTag("Grass"))//��
        {
            currentHP -= grass;
            isGrass = true;
        }
        if (other.CompareTag("Normal"))//�ʏ�
        {
            currentHP -= normal;
        }
    }
}
