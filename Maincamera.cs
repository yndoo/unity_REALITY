using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maincamera : MonoBehaviour
{
    private Vector3 firstlocation = new Vector3(410, 460, -430); //메인카메라 처음위치
    private Cylindertest target_object; // 선택된 오브젝트 받아오기 위해서
    public Object target;        // 따라다닐 타겟 오브젝트의 Transform
    private float speed_rota = 2.0f;    // 마우스 회전 속도
    private float xRotate = 0.0f;       // 내부 사용할 X축 회전량 별도 정의 (카메라 위 아래 방향)
    public Transform tr;         // 카메라 자신의 Transform
    

    void Start()
    {
        tr = GetComponent<Transform>();
        target_object = GameObject.Find("Canvas").GetComponent<Cylindertest>();
    }

    void LateUpdate()
    {
       
        if (target == null || (target.rightclick == false && target.leftclick == false))
        {
            /*if (target != null)
                Debug.Log("Maincamera: No checked!");
            else
                Debug.Log("Maincamera: target is null!");*/
            tr.position = firstlocation;
            tr.rotation = Quaternion.Euler(60, 0, 0);
        }
        else if (target.rightclick == true || target.leftclick == true)
        {
            if (target.leftclick == true)
            {
                //Debug.Log("Maincamera: Left checked!");
                tr.position = new Vector3(target.object_name.position.x - 0.52f, tr.position.y, target.object_name.position.z - 6.56f);
                tr.rotation = Quaternion.Euler(0, 0, 0);
                tr.LookAt(target.object_name);
            }
            else if (target.rightclick == true)
            {
                //Debug.Log("Maincamera: right checked!");
                tr.position = new Vector3(target.object_name.position.x+1, target.object_name.position.y+45, target.object_name.position.z+1);
                
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // 마우스를 좌우로 움직이면 Y축 회전
                float yRotateSize = mouseX * speed_rota;
                float yRotate = transform.eulerAngles.y + yRotateSize;

                // 마우스 상하, 정면이 각도 0이라고 할 때 아래로 내려다보는 각도는 80도, 하늘을 올려다 보는 각도는 45도로 제한됨
                float xRotateSize = -mouseY * speed_rota;
                xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

                transform.eulerAngles = new Vector3(xRotate, yRotate, 0);   
                
            }
            else
            {
                //Debug.Log("Maincamera: both checked!");
                tr.position = firstlocation;
                tr.rotation = Quaternion.Euler(90, 0, 0);
                /*tr.position = new Vector3(target.object_name.position.x - 0.52f, tr.position.y, target.object_name.position.z - 6.56f);
                tr.LookAt(target.object_name);*/
            }
        }
        if(target != null)
        {
            Debug.Log("Maincamera: " + target.object_name.GetInstanceID() + " leftclick = " + target.leftclick + " rightclick = " + target.rightclick);
        }
    }
}
