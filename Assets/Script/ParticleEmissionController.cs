using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderInterval : ScriptableRendererFeature
{
    [SerializeField]
    private int _fps = 24;

    private RenderIntervalPass _renderPass;
    
    private void OnValidate()
    {
        _renderPass?.SetInterval(_fps);
    }
    
    public override void Create()
    {
        _renderPass = new RenderIntervalPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _renderPass.SetTarget(renderer);
        _renderPass.SetInterval(_fps);
        renderer.EnqueuePass(_renderPass);
    }
}

public class RenderIntervalPass : ScriptableRenderPass
{
    private const string COMMAND_BUFFER_NAME = nameof(RenderIntervalPass);

    private RenderTexture _oldFrameRenderTexture;
    private ScriptableRenderer _renderer;
    private float _elapsedTime;
    private float _interval;

    public RenderIntervalPass()
    {
        renderPassEvent = RenderPassEvent.AfterRendering;
        _oldFrameRenderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.Default);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get(COMMAND_BUFFER_NAME);

        if (_elapsedTime > _interval)
        {
            _elapsedTime = 0;
            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, _oldFrameRenderTexture);
        }
        else
        {
            _elapsedTime += Time.unscaledDeltaTime;
        }

        commandBuffer.Blit(_oldFrameRenderTexture, _renderer.cameraColorTargetHandle);
        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }

    public void SetTarget(ScriptableRenderer renderer)
    {
        _renderer = renderer;
    }

    public void SetInterval(int fps)
    {
        _interval = 1f / fps;
    }
}