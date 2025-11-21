using UnityEngine;

public class MenuMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panel;

    public void MostrarPanel()
    {
        panel.SetActive(true);
    }

    public void OcultarPanel()
    {
        panel.SetActive(false);
    }
}
