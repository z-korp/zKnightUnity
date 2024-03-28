using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
    }
}
