using UnityEngine;

public class WorldObjectFPS : MonoBehaviour
{
    public RenderInterval renderInterval;
    
    private TextMesh _textMesh;
    
    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();

        Application.targetFrameRate = 60;
    }
    
    private void Update()
    {
        _textMesh.text = $"World: {renderInterval.Fps} FPS";
    }
}