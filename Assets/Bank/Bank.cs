using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour, IBillingSystem
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI goldText;
    private void Awake()
    {
        CurrentBalance = startingBalance;
    }
    public int CurrentBalance
    {
        get { return currentBalance; }
        private set { currentBalance = value; goldText.text = currentBalance.ToString(); }
    }
    public int GetCurrentBalance()=>currentBalance;
    public bool Deposit(int amount)
    {

        CurrentBalance += Mathf.Abs(amount);
        return true;
    }
    public bool Withdraw(int amount)
    {

        CurrentBalance -= Mathf.Abs(amount);
        if (currentBalance < 0)
        {
            //lose the game
            Debug.Log("Lose");
            ReloadScene();
            return false;
        }
        return true;
    }
    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
