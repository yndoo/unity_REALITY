using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class PythonPlay : MonoBehaviour
{
    public static void python()
    {
        try
        {
            Process psi = new Process();
            //���̽� �������� ���
            psi.StartInfo.FileName = "C:/Users/user/anaconda3/python.exe";
            // ������ ���ø����̼� �Ǵ� ����
            psi.StartInfo.Arguments = Application.dataPath + "/Script/REALITY.py";
            // ���� ���۽� ����� �μ�
            psi.StartInfo.CreateNoWindow = true;
            // ��â �ȶ����
            psi.StartInfo.UseShellExecute = false;
            // ���μ����� �����Ҷ� �ü�� ���� �������
            psi.Start();

            UnityEngine.Debug.Log("[�˸�] .py file ����");

        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("[�˸�] �����߻�: " + e.Message);
        }
    }
}