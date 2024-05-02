using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderIntervalPass : ScriptableRenderPass
{
    private const string COMMAND_BUFFER_NAME = nameof(RenderIntervalPass);
    private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");

    private RenderTexture _oldFrameRenderTexture;
    private ScriptableRenderer _renderer;
    private float _elapsedTime;
    private float _interval;
    private Material _material;

    public RenderIntervalPass()
    {
        renderPassEvent = RenderPassEvent.AfterRendering;
        _oldFrameRenderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_material == null)
        {
            Debug.LogWarning($"{nameof(RenderIntervalPass)}: Material is null");
            return;
        }
        
        CommandBuffer commandBuffer = CommandBufferPool.Get(COMMAND_BUFFER_NAME);
        int width = renderingData.cameraData.cameraTargetDescriptor.width;
        int height = renderingData.cameraData.cameraTargetDescriptor.height;
        bool hasUpdate = UpdateRenderTexture(width, height);

        if (_elapsedTime > _interval || hasUpdate)
        {
            _elapsedTime = 0;
            
            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, _oldFrameRenderTexture);
        }
        else
        {
            _elapsedTime += Time.unscaledDeltaTime;
        }
        
        commandBuffer.Blit(_oldFrameRenderTexture, _renderer.cameraColorTargetHandle, _material);
        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }

    private bool UpdateRenderTexture(int width, int height)
    {
        if (_oldFrameRenderTexture == null)
        {
            _oldFrameRenderTexture = RenderTexture.GetTemporary(width, height, 0);
            _material.SetTexture(_baseMap, _oldFrameRenderTexture);
            return true;
        }
        if (_oldFrameRenderTexture.width != width || _oldFrameRenderTexture.height != height)
        {
            RenderTexture.ReleaseTemporary(_oldFrameRenderTexture);
            _oldFrameRenderTexture = RenderTexture.GetTemporary(width, height, 0);
            _material.SetTexture(_baseMap, _oldFrameRenderTexture);
            return true;
        }

        return false;
    }

    public void SetTarget(ScriptableRenderer renderer)
    {
        _renderer = renderer;
    }

    public void SetParam(int fps, Material material)
    {
        _interval = 1f / fps;
        _material = material;

        if (_material != null)
        {
            _material.SetTexture(_baseMap, _oldFrameRenderTexture);
        }
    }

    public void Dispose()
    {
        RenderTexture.ReleaseTemporary(_oldFrameRenderTexture);
    }
}