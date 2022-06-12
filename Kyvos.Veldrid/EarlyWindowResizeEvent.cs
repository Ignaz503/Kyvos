namespace Kyvos.VeldridIntegration;

public struct EarlyWindowResizeEvent
{
    public int NewWidth;
    public int NewHeight;

    public EarlyWindowResizeEvent(int width, int height)
    {
        NewWidth = width;
        NewHeight = height;
    }
}
