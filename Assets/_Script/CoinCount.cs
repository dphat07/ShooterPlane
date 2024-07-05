using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CoinCount : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    int count =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = count.ToString();
    }

    public void AddCount()
    {
        count++;
    }
}
