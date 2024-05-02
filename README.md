# Unity-RenderInterval
Adjusting Camera Update Interval Using Renderer Feature of URP.

![May-02-2024 12-34-45](https://github.com/sr4dev/Unity-RenderInterval/assets/9159336/7ca0d2c7-1e7c-4f0e-94c1-0dd3bc9822fd)

## How to use

- Add RenderInterval to the Renderer Feature.
- Set up FPS and add a dedicated material for RenderInterval using shaders.
  (If the shader is not properly configured, the screen may not render correctly in the Metal API)

<img width="512" alt="image" src="https://github.com/sr4dev/Unity-RenderInterval/assets/9159336/0aad4fa1-3852-471c-8869-deb2e82a7a01">


## Note
Anti-aliasing is not supported. Anti-aliasing for the camera must be set to 'No Anti-aliasing' without exception.

<img width="441" alt="image" src="https://github.com/sr4dev/Unity-RenderInterval/assets/9159336/4abdd1c7-f647-400c-ae8f-ff880abe8f2f">

UI (Canvas) should use either Screen Space - Overlay or Screen Space - Camera, but you must use a Camera that does not apply RenderInterval.

<img width="453" alt="image" src="https://github.com/sr4dev/Unity-RenderInterval/assets/9159336/ff4a462f-ac3a-4111-bf38-4adf4b72c18b">

Separate URPRenderer into Default and Main, and RenderInterval should only be added to the RendererFeature of Main. Otherwise, flickering may occur in the Scene View.

<img width="512" alt="image" src="https://github.com/sr4dev/Unity-RenderInterval/assets/9159336/8ab95c10-fb85-49cd-a0e9-9c8009e3d930">
