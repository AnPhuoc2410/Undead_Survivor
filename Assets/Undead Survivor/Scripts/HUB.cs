using UnityEngine;
using UnityEngine.UI;

public class HUB : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }

    public InfoType infoType;

    Text text;
    Slider slider;


    void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }
    void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.Exp:
                float currExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                slider.value = currExp / maxExp;
                break;
            case InfoType.Level:
                text.text = "Lv." + GameManager.instance.level.ToString();
                break;
            case InfoType.Kill:
                text.text = GameManager.instance.kill.ToString();
                break;
            case InfoType.Time:
                float remainingTime = GameManager.instance.gameTimeLimit - GameManager.instance.gameTime;
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                text.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
                break;
            case InfoType.Health:
                float currHp = GameManager.instance.health;
                float maxHp = GameManager.instance.maxHealth;
                slider.value = currHp / maxHp;
                break;
        }
    }
}
