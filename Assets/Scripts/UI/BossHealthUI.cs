using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField]
    private Image bossHealthBar;
    private BossHealthUI _instance;
    public BossHealthUI Instance
    {
        get
        {
            if (_instance == null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                _instance = FindObjectOfType<BossHealthUI>();
#pragma warning restore CS0618 // Type or member is obsolete
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("BossHealthUI");
                    _instance = gameObject.AddComponent<BossHealthUI>();
                }
            }
            return _instance;
        }
    }
    void Update()
    {
        bossHealthBar.fillAmount = LevelManager.Instance.GetBossHealthPercentage();
    }
}