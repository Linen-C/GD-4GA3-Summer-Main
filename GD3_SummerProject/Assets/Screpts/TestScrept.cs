using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class TestScrept : MonoBehaviour
{
    void Start()
    {
        // �t�@�C���p�X���w��(Assets����n�܂���ۂ�)
        //string filePath = Application.dataPath + @"\Screpts\File\test.txt";
        string filePath = Application.dataPath + @"\Files\test.txt";

        // �f�[�^�X�g���[���œǂݏo��
        StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

        // ���g���Ȃ��Ȃ�܂ŌJ��Ԃ�
        while (!sr.EndOfStream)
        {
            // �f�o�b�O�o�͂�
            Debug.Log("�ǂݍ��ݓ��e�F" + sr.ReadLine());
        }
    }
}
