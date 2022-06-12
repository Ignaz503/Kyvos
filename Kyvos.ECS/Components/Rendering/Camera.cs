using Kyvos.Core;
using Kyvos.Maths;
using Kyvos.VeldridIntegration;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

using Veldrid.Utilities;

namespace Kyvos.ECS.Components.Rendering;
public partial struct Camera
{
    float fov;
    float nearPlane;
    float farPlane;

    float windowWidth;
    float windowHeight;

    float aspectRatio => windowWidth / windowHeight;

    bool yInvertedClipSpace;

    Vector3 position;
    public Vector3 Position
    {
        get => position;
        set
        {
            SetAndCalculateViewMatrix(ref position, value);
        }
    }

    public Vector3 LookDirection { get; private set; }
    Vector3 yawPitchRoll;

    public Vector3 EulerAngles
    {
        get => yawPitchRoll;
        set => SetAndCalculateViewMatrix(ref yawPitchRoll, value);

    }

    public float Yaw
    {
        get => yawPitchRoll.Y;
        set => SetAndCalculateViewMatrix(ref yawPitchRoll.Y, value);

    }

    public float Pitch
    {
        get => yawPitchRoll.X;
        set => SetAndCalculateViewMatrix(ref yawPitchRoll.X, value);
    }

    public float Roll
    {
        get => yawPitchRoll.Z;
        set => SetAndCalculateViewMatrix(ref yawPitchRoll.Z, value);
    }

    public float FoV
    {
        get => fov;
        set => SetAndCalculateProjectionMatrix(ref fov, value);
    }

    public float NearPlane
    {
        get => nearPlane;
        set => SetAndCalculateProjectionMatrix(ref nearPlane, value);
    }

    public float FarPlane
    {
        get => farPlane;
        set => SetAndCalculateProjectionMatrix(ref farPlane, value);
    }

    public Matrix4x4 ViewMatrix { get; private set; }
    public Matrix4x4 ProjectionMatrix { get; private set; }

    public BoundingFrustum Frustum
        => new BoundingFrustum(ViewMatrix * ProjectionMatrix);

    public Camera(IApplication app)
    {
        nearPlane = 0.01f;
        farPlane = 1000f;
        fov = MathF.PI / 4f; //90 deg

        position = Vector3.Zero;

        LookDirection = -Vector3.UnitZ;

        yawPitchRoll = Vector3.Zero;
        
        ViewMatrix = Matrix4x4.Identity;
        ProjectionMatrix = Matrix4x4.Identity;
        
        var window = app.GetComponent<Window>()!;
        var gfxDevice = app.GetComponent<GraphicsDeviceHandle>()?.GfxDevice;


        windowWidth = window.Width;
        windowHeight = window.Height;
        yInvertedClipSpace = gfxDevice?.IsClipSpaceYInverted ?? false; //TODO what is the default
        window.OnWindowEvent += OnWindowEvent;
        CalculateViewMatrix();
        CalculateProjectionMatrix();
    }

    public Camera(float nearPlane, float farPlane, float fov, Vector3 position, Vector3 lookDirection, Vector3 eulerAngles, IApplication app)
    {
        this.nearPlane = nearPlane;
        this.farPlane = farPlane;
        this.fov = fov;

        this.position = position;
        this.LookDirection = lookDirection;
        this.yawPitchRoll = eulerAngles;

        ViewMatrix = Matrix4x4.Identity;
        ProjectionMatrix = Matrix4x4.Identity;

        var window = app.GetComponent<Window>()!;
        var gfxDevice = app.GetComponent<GraphicsDeviceHandle>()?.GfxDevice;

        windowWidth = window.Width;
        windowHeight = window.Height;
        yInvertedClipSpace = gfxDevice?.IsClipSpaceYInverted ?? false;
        window.OnWindowEvent += OnWindowEvent;

        CalculateViewMatrix();
        CalculateProjectionMatrix();
    }

    public Camera()
    {
        throw new InvalidOperationException($"Invalid Ctor for {typeof(Camera)}");
    }

    private void CalculateProjectionMatrix()
    {

        ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, farPlane);
        

        if (yInvertedClipSpace)
        {
            ProjectionMatrix *= new Matrix4x4(
                1, 0, 0, 0,
                0, -1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

    }

    private void CalculateViewMatrix()
    {
        var rot = Quaternion.CreateFromYawPitchRoll(Yaw, Pitch, Roll);
        LookDirection = Vector3.Transform(-Vector3.UnitZ, rot);
        ViewMatrix = Matrix4x4.CreateLookAt(Position, position + LookDirection, Vector3.UnitY);
    }

    private void CalculateRotationFromLookDirection(Vector3 newLookTo) 
    {
        LookDirection = newLookTo;

        var angle = MathF.Acos(Vector3.Dot(-Vector3.UnitZ, newLookTo));
        var rot = Quaternion.CreateFromAxisAngle(LookDirection,angle);

        EulerAngles = rot.ToEuler();
    }

    void OnWindowEvent(Window.Event @event)
    {
        if (@event.Type is Window.EventType.Resized)
            WindowResized(@event.Window.Width, @event.Window.Height);
    }

    public void WindowResized(float width, float height)
    {
        windowWidth = width;
        windowHeight = height;
        CalculateProjectionMatrix();
    }

    public void Cleanup(IApplication app)
    {
        if (app.HasComponent<Window>())
           app.GetComponent<Window>()!.OnWindowEvent -= OnWindowEvent;

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetAndCalculateViewMatrix<T>(ref T backingValue, T newVal)
    {
        backingValue = newVal;
        CalculateViewMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetAndCalculateProjectionMatrix<T>(ref T backingValue, T newVal)
    {
        backingValue = newVal;
        CalculateProjectionMatrix();
    }


    public Vector3 ScreenToWorldSpace(Vector2 screenSpace, float atDistance) 
    {
        Matrix4x4.Invert(ViewMatrix * ProjectionMatrix, out Matrix4x4 mat);

        var vec = new Vector4(screenSpace, atDistance, 1.0f);
        var res = Vector4.Transform(vec, mat);
        return res.XYZ() / res.W;
    }

}

