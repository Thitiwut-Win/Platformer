using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}