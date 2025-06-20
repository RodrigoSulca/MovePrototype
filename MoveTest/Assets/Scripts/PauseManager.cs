using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject volumePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            volumePanel.SetActive(!volumePanel.activeSelf);
            if (volumePanel.activeSelf)
            {
                Time.timeScale = 0;
                AudioManager.instance.PauseMusic();
            }
            else
            {
                Time.timeScale = 1;
                AudioManager.instance.ResumeMusic();
            }
        }
    }
}
