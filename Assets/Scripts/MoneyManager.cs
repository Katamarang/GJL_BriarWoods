using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] float money;
    public float Money { get { return money; } }

    public TMP_Text text;

    private void Update()
    {
        text.text = money.ToString();
    }

    public void MoneyIN(float moneyIn)
    {
        money += moneyIn;
    }

    public void MoneyOut(float moneyOut)
    {
        money -= moneyOut;
    }
}
