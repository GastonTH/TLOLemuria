using TMPro;
using UnityEngine;

public class CoinCountController : MonoBehaviour
{

    public TextMeshProUGUI txtCoin;
    public int coinCount = 0;

    private void Start()
    {
        txtCoin.text = coinCount.ToString();
    }
    
    public void addCoins(int coins)
    {
        coinCount += coins;
        txtCoin.text = coinCount.ToString();
    }
    
    public void removeCoins(int coins)
    {
        coinCount -= coins;
        txtCoin.text = coinCount.ToString();
    }
    
    public void setCoinCount(int coins)
    {
        coinCount = coins;
        txtCoin.text = coinCount.ToString();
    }
}
