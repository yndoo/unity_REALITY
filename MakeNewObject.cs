using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/*
 * 
 * ��ǥ ��ȯ ���� �����Դϴ�.
 * 
 * 
 */

public class MakeNewObject : MonoBehaviour
{

    //public GameObject cubePrefab;

    int num = 0; // ������ ���� �� �ѹ�
    int frSize = 627; // ������ ����
    int maxID = 383;  // �������� id �ִ�
    private bool check = true;


    // MakeData�� ���� ����
    static string strFile = "C:\\Users\\user\\Yndoo's Practice\\Assets\\Resources\\reality_data.txt";
    string undotext;
    public TextAsset txt;
    public string[,] Sentence;
    public int lineSize, rowSize;
    public float[,] frames; // �����Ӹ��� ��ü �� ������
    public int[,] objectMap; // �����Ӹ��� 0~10 (������ �ִ�) id ������ line, ������ 

    public void Start()
    {
        strFile = Application.dataPath + "/Resources/reality_data.txt";
        PythonPlay.python();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FileInfo fileInfo = new FileInfo(strFile);
        //Debug.Log(fileInfo.Exists);
        if (fileInfo.Exists)
        {
            txt = Resources.Load("reality_data") as TextAsset;
            //Debug.Log(txt);
            if (check)
                MakeData();

            if (num > frSize - 1)     // frames ������ Update�� �����
            {
                return;
            }
            else if (frSize % 100 == 0)
            {       //100�����Ӹ��� ID Ȯ��?

            }

            /*  ID �ϳ��� �ٲ� �� �ִ� ��� ã�� �߳�..! ��  */
            //ObjectManager.instance.transform.GetChild(1).gameObject.GetComponent<Move>().objectID = 333;

            for (int i = 1; i <= maxID; i++)    // i �� Move ������Ʈ �� ID, i-1�� ��ü ���� ( GetChild�� ���� �� )
            {
                if (objectMap[num, i] == 0)      // �̹� �����ӿ��� i�� ID�� ��ü�� ����
                {
                    // �� ��ȣ�� ��ü ����
                    ObjectManager.instance.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
                else if (objectMap[num, i] > 0) // �̹� �����ӿ� i�� ID�� ��ü�� ����
                {
                    // �� ��ȣ�� ��ü�� �� ��ǥ�� �����ֱ�
                    ObjectManager.instance.transform.GetChild(i - 1).gameObject.SetActive(true);

                    if (num > 1)
                    {
                        Vector3 before = new Vector3(float.Parse(Sentence[objectMap[num - 1, i], 1]), 0, float.Parse(Sentence[objectMap[num - 1, i], 2]));
                        Vector3 target = new Vector3(float.Parse(Sentence[objectMap[num, i], 1]), 0, float.Parse(Sentence[objectMap[num, i], 2]));

                        ObjectManager.instance.transform.GetChild(i - 1).position = Vector3.MoveTowards(before, target, 1f * Time.deltaTime);

                        ObjectManager.instance.transform.GetChild(i - 1).transform.rotation = Quaternion.LookRotation(target - before);

                    }
                    else
                    {
                        ObjectManager.instance.transform.GetChild(i - 1).position
                         = new Vector3(float.Parse(Sentence[objectMap[num, i], 1]), 0, float.Parse(Sentence[objectMap[num, i], 2]));
                        ObjectManager.instance.transform.GetChild(i - 1).transform.rotation = Quaternion.LookRotation(ObjectManager.instance.transform.GetChild(i - 1).position);
                        Debug.Log("�ѹ������;ߴ�");
                    }


                }
            }


            num++;
            //Debug.Log("num : " + num);
        }
    }

    void Create(int index)          // ���ڶ���ŭ �߰�
    {
        for (int i = 0; i < index; i++)
        {
            ObjectManager.instance.Pop();
        }
    }

    void Return(int index)          // ���¸�ŭ ��ȯ
    {
        for (int i = 0; i < index; i++)
        {
            ObjectManager.instance.Push(ObjectManager.instance.transform.GetChild(i).gameObject.GetComponent<Move>());
        }
    }

    public int MakeData()
    {
        // ���ʹ����� ������ ������ �迭�� ũ�� ����
        string curretText = txt.text.Substring(0, txt.text.Length - 1);
        if (undotext == curretText)
        {
            check = false;
            return -1;
        }
        undotext = curretText;
        string[] line = curretText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];


        // �� �ٿ��� ������ ������ Sentence�� ä��
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
                //(i + "," + j + "   " + Sentence[i, j] + "   " + rowSize + "\n");
            }
        }

        // frame � ��ü�� �� ������ ����. frame �迭�� 0���� ����. �� frame�� ������ ��line�������� ����.
        // frame �� �� �������� �̸� �ޱ�� ��. 
        frames = new float[frSize, 2];

        // ��ü id ���� �ִ밡 383
        objectMap = new int[frSize, maxID + 1];

        int f = 0;

        for (int i = 0; i < lineSize; i++)
        {
            if (Sentence[i, 0] == "frame")
            {
                frames[f, 0] = float.Parse(Sentence[i, 2]); // ��ü ����
                frames[f, 1] = i;                           // ���°��������
                objectMap[f, 0] = f;

                for (int k = i + 1; k <= i + frames[f, 0]; k++) // frame������ line���� idȮ��
                {
                    for (int id = 0; id < maxID; id++)
                    {
                        if (float.Parse(Sentence[k, 0]) == id)
                        {
                            objectMap[f, id] = k;   // id�� ��ü ������ ���° �������� ����
                        }
                        else
                        {
                            //objectMap[f, id] = -1;  // ������ ����
                        }

                    }
                }
                //print(frames[f, 0] + ", " + frames[f, 1]);
                f++;
            }

        }
        check = true;
        return lineSize;
    }
}