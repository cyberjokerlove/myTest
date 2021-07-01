using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UItest : MonoBehaviour
{
    public Slider masterVolueSlider;
    public Button pauseBtn;
    public AudioMixer masterMixer;
    public Text scoreText;
    bool isPause = false;
    public float score = 0;
    public float addscore = 100;
    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(PauseGame);
        masterVolueSlider.onValueChanged.AddListener(VloumeChange);
        scoreText.text = "分数：" + score.ToString();

    }

    public void VloumeChange(float volume)
    {
        masterMixer.SetFloat("masterVolume",volume);
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void AddScore()
    {
        score += addscore;
        scoreText.text = "分数：" + score.ToString();
    }
}
