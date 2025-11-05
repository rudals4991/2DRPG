using UnityEngine;

public class TestPanelControll : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public void OffPanel()
    {
        panel.SetActive(false);
    }
    public void OnPanel()
    {
        panel.SetActive(true);
    }
}
