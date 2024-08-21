using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text enemyAliveText;
    public Text totalCoinText;
    public Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyAliveText.text = EnemySpawner.Instance.enemiesAlive.ToString();
        waveText.text = EnemySpawner.Instance.currentWave.ToString();
        totalCoinText.text = GameManager.Instance.totalCoin.ToString();
    }
}
