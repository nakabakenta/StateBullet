using UnityEngine;

public class PlayerController : CharacterBase
{
    public CameraController cameraController;//"CameraController"
    public PlayerBulletBase playerBulletBase;//"PlayerBulletBase"

    protected override void Start()
    {
        base.Start();//�p����̊֐�"Start"�����s����
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//�p����̊֐�"Update"�����s����

        // �ړ��ʂ̌v�Z
        float horizontal = Input.GetAxis("Horizontal");//AD�L�[
        float vertical = Input.GetAxis("Vertical");    //WS�L�[
        //�J�����̑O�㍶�E�����Ɉړ�
        Vector3 move = cameraController.transform.forward * vertical + cameraController.transform.right * horizontal;
        transform.position += move * moveSpeed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            if(playerBulletBase.useElement > 6)
            {

            }
            else
            {
                playerBulletBase.useElement += 1;
            }
        }
        else if (scroll < 0f)
        {
            Debug.Log("�z�C�[���_�E���I");
            playerBulletBase.useElement -= 1;
        }

        Debug.Log(playerBulletBase.useElement);
    }
}
