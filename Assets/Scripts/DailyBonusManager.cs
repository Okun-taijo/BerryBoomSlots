using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusManager : MonoBehaviour
{
    private const string LastLoginDateKey = "LastLoginDate";
    private const int MaxDaysForReset = 5; // ���������� ���� �� ������ ������

    private ResourcesManager _resourcesManager;

    [Serializable]
    private class DailyBonus
    {
        public int Coins;
        public int Energy;
    }

    [SerializeField]
    private DailyBonus[] dailyBonuses; // ������ ������� ��� ������� �� ���� ����

    private void Awake()
    {
        _resourcesManager = FindObjectOfType<ResourcesManager>(); // ������� ������ � ResourcesManager
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
            // ���������� ������ ��� � ������� �����
            int dayIndex = (int)(today - lastLoginDate).TotalDays % MaxDaysForReset;

            // �������� ��������������� ����� ��� �������� ���
            DailyBonus dailyBonus = GetDailyBonusForDay(dayIndex);

            _resourcesManager.Coins += dailyBonus.Coins;
            _resourcesManager.ChangeCoinCounter();
            _resourcesManager.Energy += dailyBonus.Energy;
            _resourcesManager.ChangeEnergyCounter();


            // ��������� ���� ���������� �����
            PlayerPrefs.SetString(LastLoginDateKey, today.Ticks.ToString());
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("�� ��� �������� ����� �������.");
        }
    }

    private DailyBonus GetDailyBonusForDay(int dayIndex)
    {
        // ���������, ����� ������ ��� ��� � �������� ����������� ���������
        if (dayIndex >= 0 && dayIndex < dailyBonuses.Length)
        {
            return dailyBonuses[dayIndex];
        }
        else
        {
            Debug.LogError("������������ ������ ��� ��� ������ ������.");
            // ���������� ��������� ������� ����� (����� ���������� �� �������)
            return new DailyBonus { Coins = 0, Energy = 0 };
        }
    }
}
