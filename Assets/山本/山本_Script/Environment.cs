using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    //���ω��t���O
    //           �J�A �ҏ��A�\���A   ���d�́A     ��d�́A    �L����
    public bool rain, hot, storm, high_gravity, low_gravity, abundant;
    //�Q�[���J�n�J�E���g
    public float start_time;
    //���ω��܂ł̃J�E���g
    public float change_time;
    //���ω��܂ł̌��݂̃J�E���g
    public float now_time;
    //���ω��̗����i�[�p
    public int random_environment;


    public Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ////���t���O��S��false�ɂ���i�ŏ��̊����t���b�g�ɂ���j
        //FlatEnvironment();

        ////���ω��܂ł̎��Ԃ�ݒ�
        //now_time = 0;

        //image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (start_time > 0.0f)
        //{
        //    //�Q�[���J�n�܂ł̃J�E���g�_�E��
        //    start_time -= Time.deltaTime;
        //}
        //else
        //{
        //    //���Ԍo�߂Ŋ���ω�������
        //    ChangeEnvironment();

        //    //���Ԍo�߂ɔ����āAUI�ɂ��ω���^����
        //    image.fillAmount = now_time / change_time;
        //}
    }

    //���Ԍo�߂Ŋ���ω�������
    void ChangeEnvironment()
    {
        now_time += Time.deltaTime;

        if (now_time >= change_time)
        {
            random_environment = Random.Range(0, 100);

            //���t���O��S��false�ɂ���i�ŏ��̊����t���b�g�ɂ���j
            FlatEnvironment();

            //�i�[�����l���ƂɁA�ω�������̃t���O��true�ɂ���
            if (random_environment <= 24)
                rain = true;
            else if (random_environment <= 39)
                hot = true;
            else if (random_environment <= 64)
                storm = true;
            else if (random_environment <= 79)
                high_gravity = true;
            else if (random_environment <= 94)
                low_gravity = true;
            else if (random_environment <= 99)
                abundant = true;

            //�J�E���g�����Z�b�g����
            now_time = 0;
        }
    }

    //���t���O��S��false�ɂ���i�ŏ��̊����t���b�g�ɂ���j
    void FlatEnvironment()
    {
        rain = false;
        hot = false;
        storm = false;
        high_gravity = false;
        low_gravity = false;
        abundant = false;
    }
}
