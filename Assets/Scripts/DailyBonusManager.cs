using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusManager : MonoBehaviour
{
    private const string LastLoginDateKey = "LastLoginDate";
    private const int MaxDaysForReset = 5; // Количество дней до сброса бонуса

    private ResourcesManager _resourcesManager;

    [Serializable]
    private class DailyBonus
    {
        public int Coins;
        public int Energy;
    }

    [SerializeField]
    private DailyBonus[] dailyBonuses; // Массив бонусов для каждого из пяти дней

    private void Awake()
    {
        _resourcesManager = FindObjectOfType<ResourcesManager>(); // Находим объект с ResourcesManager
    }

    public void ClaimDailyBonus()
    {
        DateTime lastLoginDate;

        if (PlayerPrefs.HasKey(LastLoginDateKey))
        {
            long ticks = Convert.ToInt64(PlayerPrefs.GetString(LastLoginDateKey));
            lastLoginDate = new DateTime(ticks);
        }
        else
        {
            lastLoginDate = DateTime.Now;
        }

        DateTime today = DateTime.Now.Date;

        if (today > lastLoginDate.Date)
        {
            // Определяем индекс дня в текущем цикле
            int dayIndex = (int)(today - lastLoginDate).TotalDays % MaxDaysForReset;

            // Получаем соответствующий бонус для текущего дня
            DailyBonus dailyBonus = GetDailyBonusForDay(dayIndex);

            _resourcesManager.Coins += dailyBonus.Coins;
            _resourcesManager.ChangeCoinCounter();
            _resourcesManager.Energy += dailyBonus.Energy;
            _resourcesManager.ChangeEnergyCounter();


            // Обновляем дату последнего входа
            PlayerPrefs.SetString(LastLoginDateKey, today.Ticks.ToString());
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Вы уже получили бонус сегодня.");
        }
    }

    private DailyBonus GetDailyBonusForDay(int dayIndex)
    {
        // Проверяем, чтобы индекс дня был в пределах допустимого диапазона
        if (dayIndex >= 0 && dayIndex < dailyBonuses.Length)
        {
            return dailyBonuses[dayIndex];
        }
        else
        {
            Debug.LogError("Недопустимый индекс дня для выдачи бонуса.");
            // Возвращаем некоторый базовый бонус (можно доработать по желанию)
            return new DailyBonus { Coins = 0, Energy = 0 };
        }
    }
}
