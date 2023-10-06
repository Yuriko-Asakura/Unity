using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������� ���-�� ������
    private int Health;

    //������������ ���-�� ������
    public int MaxHealth = 100;

    //���� ������������
    private int Stamina = 100;

    //�������� ����������� ������������ � ������ ������
    public bool IsStaminaRestoring = false;

    private void Start()
    {
        Health = MaxHealth;
    }

    private void FixedUpdate()
    {
        StaminaCheck();
    }

    private void StaminaCheck()
    {
        Debug.Log("������� " + Stamina);
        //���� ������� ���, ���������������
        if (Stamina <= 0) StartCoroutine(StaminaRestore());
    }

    //����������� ����� ��� ������������� ������������
    private IEnumerator StaminaRestore()
    {
        IsStaminaRestoring = true;
        //�������� � 3 �������
        yield return new WaitForSeconds(3);
        Stamina = 100;
        IsStaminaRestoring = false;
    }

    public void SpendStamina()
    {
        //������ ������������
        Stamina -= 1;
    }

}
