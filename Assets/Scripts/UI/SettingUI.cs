using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI volumePercentage;
    private int volume;
    void Update()
    {
        volume = (int)(slider.value * 100);
        volumePercentage.SetText(volume + "%");
        SetVolume();
    }
    private void SetVolume()
    {
        LevelManager.Instance.SetVolume(volume);
    }
}