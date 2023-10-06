using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //текущее кол-во жизней
    private int Health;

    //максимальное кол-во жизней
    public int MaxHealth = 100;

    //очки выносливости
    private int Stamina = 100;

    //Проверка восполнения выносливости в данный момент
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
        Debug.Log("Стамина " + Stamina);
        //Если стамины нет, востаннавливаем
        if (Stamina <= 0) StartCoroutine(StaminaRestore());
    }

    //Асинхронный метод для востановления выносливости
    private IEnumerator StaminaRestore()
    {
        IsStaminaRestoring = true;
        //Задержка в 3 секунды
        yield return new WaitForSeconds(3);
        Stamina = 100;
        IsStaminaRestoring = false;
    }

    public void SpendStamina()
    {
        //Тратим выносливость
        Stamina -= 1;
    }

}
