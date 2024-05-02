using UnityEngine;
using UnityEngine.UI;

public class UIObjectFPS : MonoBehaviour
{
    private Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    
    private void Update()
    {
        _text.text = $"UI: {Mathf.FloorToInt(1 / Time.deltaTime)}FPS";
    }
}