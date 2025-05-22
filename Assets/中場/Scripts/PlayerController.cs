using UnityEngine;

public class PlayerController : CharacterBase
{
    public CameraController cameraController;//カメラコントローラー

    protected override void Start()
    {
        base.Start();//継承先の関数"Start"を実行する
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//継承先の関数"Update"を実行する

        // 移動量の計算
        float horizontal = Input.GetAxis("Horizontal");//ADキー
        float vertical = Input.GetAxis("Vertical");    //WSキー
        //カメラの前後左右方向に移動
        Vector3 move = cameraController.transform.forward * vertical + cameraController.transform.right * horizontal;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
