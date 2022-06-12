using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Kyvos.Maths;

namespace Kyvos.Core;
public class Timer : IDisposable
{

    public const string CONFIG_KEY = "time";

    InternalTimer internalTimer;

    public float DeltaTime => UnscaledDeltaTime * TimeScale;
    public float UnscaledDeltaTime { get; private set; }
    public float TimeScale { get; set; } = 1.0f;

    public float UnscaledFixedDeltaTime { get; private set; }
    public float FixedDeltaTime => UnscaledFixedDeltaTime * TimeScale;

    private float fixedUpdateTimeCarry;

    public int NumberOfFixedUpdates { get; private set; }

    public ulong TotalFrames => internalTimer.FrameCounter;

    public double TotalTimeElapsed => internalTimer.TotalSecondsElapsed;

    bool isDisposed = false;

    public float DesiredFrameRate
    {
        get => internalTimer.desiredFrameRate;
        set => internalTimer.desiredFrameRate = value;
    }

    internal Timer(Config config, float desiredFrameRate)
    {
        internalTimer = InternalTimer.Create(config, desiredFrameRate);

        ThrowIfZero(config.FixedUpdateTimingMS);

        UnscaledFixedDeltaTime = config.FixedUpdateTimingMS * 0.001f;
        fixedUpdateTimeCarry = 0f;
    }

    private static void ThrowIfZero(float fixedUpdateTimingMS)
    {
        if (Mathf.AlmostEquals(fixedUpdateTimingMS, 0f))
            throw new FixedUpdateTimingZero();
    }

    private void Start()
    {
        internalTimer.Start();
    }

    private void MeassureElapsedTime()
    {
        UnscaledDeltaTime = internalTimer.Meassure();
        CalculateNumberFixedUpdates();
    }

    private void CalculateNumberFixedUpdates()
    {
        float t = fixedUpdateTimeCarry + UnscaledDeltaTime;

        NumberOfFixedUpdates = (int)Math.Floor(t / UnscaledFixedDeltaTime);

        fixedUpdateTimeCarry = t - (NumberOfFixedUpdates * UnscaledFixedDeltaTime);
    }


    internal void Stop()
    {
        internalTimer.Stop();
    }

    public void Dispose()
    {
        if (isDisposed)
            return;
        Stop();
        isDisposed = true;
    }

    public class System : IAppComponentSystem<Timer>
    {

        public void Update(Timer timer, IApplication application)
        {
            timer.MeassureElapsedTime();
        }

        public void Dispose()
        { }

        public void Initialize(Timer timer, IApplication ctx)
        {
            timer.Start();
        }
    }


    struct InternalTimer
    {
        Stopwatch sw;

        public ulong FrameCounter { get; private set; }

        public double TotalSecondsElapsed { get; private set; }

        public float desiredFrameRate;

        bool vsync;
        public bool VSync
        {
            get => vsync;
            set
            {
                vsync = value;
            }
        }

        internal void Start() => sw.Start();

        internal void Stop() => sw.Stop();

        internal float Meassure()
        {
            sw.Stop();
            var value = sw.Elapsed.TotalSeconds;
            TotalSecondsElapsed += value;
            FrameCounter++;

            //TODO if vsync sleep for difference - tiny amount
            //loop for tiny amount
            //value = target value;
            if (VSync)
                throw new NotImplementedException();

            sw.Restart();
            return (float)value;
        }

        public static InternalTimer Create(Config config, float desiredFrameRate)
            => new InternalTimer
            {
                sw = new Stopwatch(),
                FrameCounter = 0,
                TotalSecondsElapsed = 0,
                desiredFrameRate = desiredFrameRate,
                VSync = config.Vsync
            };
    }

    public struct Config
    {
        public bool Vsync { get; init; }

        public float FixedUpdateTimingMS { get; init; }

    }

    public abstract class TimerException : Exception
    {
        protected TimerException()
        {
        }

        protected TimerException(string? message) : base(message)
        {
        }

        protected TimerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected TimerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    public class FixedUpdateTimingZero : TimerException 
    {
        public FixedUpdateTimingZero() : base("Fixed update timing can't be zero")
        {
                
        }
    }

}

