using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wallet
{
    [HideInInspector] [SerializeField] private List<Currency> _currencies;

    public Wallet()
    {
        _currencies = new List<Currency>();

        for (int i = 0; i < (int)CurrencyType.Count; i++)
        {
            _currencies.Add(new Currency((CurrencyType)i));
        }
    }

    public int GetCurrencyAmount(CurrencyType currencyType)
    {
        Currency currency = GetCurrency(currencyType);

        if (currency != null)
        {
            return currency.Amount;
        }

        return 0;
    }

    public void AddCurrency(int amount, CurrencyType currencyType)
    {
        Currency currency = GetCurrency(currencyType);

        if (currency != null)
        {
            currency.Add(amount);
        }
    }

    public void SpendCurrency(int amount, CurrencyType currencyType)
    {
        Currency currency = GetCurrency(currencyType);

        if (currency != null)
        {
            currency.Spend(amount);
        }
    }

    public void ResetCurrency(CurrencyType currencyType)
    {
        Currency currency = GetCurrency(currencyType);

        if (currency != null)
        {
            currency.Reset();
        }
    }

    private Currency GetCurrency(CurrencyType currencyType)
    {
        foreach (var currency in _currencies)
        {
            if (currency.Type == currencyType)
            {
                return currency;
            }
        }

        return null;
    }
}