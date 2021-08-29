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
    private void OnEnable()     // 오브젝트가 활성화되면 실행하는 함수
    {

    }
    private void Update()
    {

    }
}
