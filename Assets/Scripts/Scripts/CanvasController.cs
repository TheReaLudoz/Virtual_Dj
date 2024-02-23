using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private void Start()
    {
        _canvas.SetActive(false);
    }

    public void OpenCanvas()
    {
        _canvas.SetActive(true);

    }

    public void CloseCanvas()
    {
        _canvas.SetActive(false);

    }
}