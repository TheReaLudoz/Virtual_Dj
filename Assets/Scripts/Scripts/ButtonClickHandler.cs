using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonClickHandler : MonoBehaviour
{
   [Tooltip("TextMeshProUGUI to display the current BPM")]
    [SerializeField] private TextMeshProUGUI buttonText = null;

    void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClick());
        }
    }

    public void OnButtonClick()
    {
        if (buttonText != null)
        {
            buttonText.text = gameObject.name;
            Debug.Log(gameObject.name);
        }
    }
}
