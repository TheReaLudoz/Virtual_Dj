using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private CanvasController _canvasController2;
    [SerializeField] private CanvasController _canvasController3;
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

    public void OpenBPM1()
    {
        _canvasController2.OpenCanvas();
    }
    public void OpenBPM2()
    {
        _canvasController3.OpenCanvas();
    }
}