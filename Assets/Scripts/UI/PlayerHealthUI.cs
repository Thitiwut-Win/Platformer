using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerHealth;
    [SerializeField]
    private Image playerHealthBar;
    private PlayerHealthUI _instance;
    public PlayerHealthUI Instance
    {
        get
        {
            if (_instance == null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                _instance = FindObjectOfType<PlayerHealthUI>();
#pragma warning restore CS0618 // Type or member is obsolete
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("PlayerHealthUI");
                    _instance = gameObject.AddComponent<PlayerHealthUI>();
                }
            }
            return _instance;
        }
    }
    void Update()
    {
        playerHealth.SetText(LevelManager.Instance.GetPlayerHealth().ToString());
        playerHealthBar.fillAmount = LevelManager.Instance.GetPlayerHealthPercentage();
    }
}