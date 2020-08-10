using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIInfoManager : MonoBehaviour
{
    public static UIInfoManager BaseUI;

    float realTimerSeconds = 0;
    float timerSeconds = 0;
    int timerMinutes = 0;

    // Timer
    [SerializeField]
    Text timer;

    // Coin
    [SerializeField]
    Text coinCounter;


    // Hp
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Text hpText;

    //Colors
    public List<Image> ImageField;

    public void DisplayCoins(int coinQuantity)
    {
        coinCounter.text = coinQuantity.ToString();
    }

    public void DisplayHp(float CharacterHp)
    {
        hpSlider.value = CharacterHp;
        hpText.text = CharacterHp.ToString();
    }

    private void Awake()
    {
        BaseUI = this;
    }

    // Use this for initialization
    void Start ()
    {
        for(int i = 0; i < 4; i++)
        {
            ImageField[i].color = Player.Single.color[i];
        }

        hpSlider.maxValue = Player.maxHp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timerSeconds += Time.deltaTime;
        realTimerSeconds += Time.deltaTime;
        if (timerSeconds >= 59)
        {
            timerMinutes += 1;
            timerSeconds = 0;
        }
        string timerSecondsStr = Mathf.Round(timerSeconds).ToString();
        if(timerSecondsStr.Length <= 1)
        {
            timerSecondsStr = "0" + timerSecondsStr;
        }
        string timerText = timerMinutes + ":" + timerSecondsStr;
        timer.text = timerText;

        hpText.text = Mathf.Round(Player.Single.hp / Player.maxHp * 100).ToString() + "%";
        hpSlider.value = Player.Single.hp;
    }

}
