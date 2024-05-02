using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RenderInterval : ScriptableRendererFeature
{
    [SerializeField]
    private int _fps = 24;
    
    [SerializeField]
    private Material _material;
    
    private RenderIntervalPass _renderPass;
    
    public int Fps
    {
        get => _fps;
        set
        {
            _fps = value;
            OnValidate();
        }
    }

    public Material Material
    {
        get => _material;
        set
        {
            _material = value;
            OnValidate();
        }
    }

    private void OnValidate()
    {
        _renderPass?.SetParam(_fps, _material);
    }
    
    public override void Create()
    {
        _renderPass = new RenderIntervalPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _renderPass.SetTarget(renderer);
        _renderPass.SetParam(_fps, _material);
        renderer.EnqueuePass(_renderPass);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        _renderPass?.Dispose();
    }
}