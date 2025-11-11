using UnityEngine;

public class TestPanelControll : MonoBehaviour
{
    [SerializeField] GameObject partyPanel;
    [SerializeField] GameObject infoPanel;
    public void OffPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void OnPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}
