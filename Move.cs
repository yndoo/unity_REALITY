using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int objectID = 0;
    private void Start()
    {

        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
    private void OnEnable()     // ������Ʈ�� Ȱ��ȭ�Ǹ� �����ϴ� �Լ�
    {

    }
    private void Update()
    {

    }
}
