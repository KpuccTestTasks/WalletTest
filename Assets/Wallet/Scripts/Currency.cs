using System;
using UnityEngine;

[Serializable]
public class Currency
{
    [SerializeField] protected int _amount;
    public int Amount => _amount;

    [SerializeField] protected CurrencyType _type;
    public CurrencyType Type => _type;

    public Currency(CurrencyType type)
    {
        _amount = 0;
        _type = type;
    }

    public void Add(int amount)
    {
        _amount += amount;
    }

    public void Spend(int amount)
    {
        if (amount > _amount)
        {
            _amount = 0;
            return;
        }

        _amount -= amount;
    }

    public void Reset()
    {
        _amount = 0;
    }
}
