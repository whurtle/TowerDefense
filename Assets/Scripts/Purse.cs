using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purse : MonoBehaviour
{
    private int coin;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin(int coin)
    {
        this.coin += coin;
        this.SetText();
    }

    private void SetText()
    {
        this.coinText.text = $"{this.coin}".PadLeft(6, '0');
    }
}
