using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core
{
    public static class Time
    {
        static Timer timer;

        public static float DeltaTime => UnscaledDeltaTime * TimeScale;
        public static float UnscaledDeltaTime { get; private set; }
        public static float TimeScale { get; set; } = 1.0f;

        public static float UnscaledFixedDeltaTime { get; private set; }
        public static float FixedDeltaTime => UnscaledFixedDeltaTime * TimeScale;

        private static float fixedUpdateTimeCarry;

        internal static int NumberOfFixedUpdates { get; private set; }

        public static ulong TotalFrames => timer.FrameCounter;

        public static double TotalTimeElapsed => timer.TotalSecondsElapsed;


        internal static void Initialize( Config config, float desiredFrameRate )
        {
            timer = Timer.Create( config, desiredFrameRate );

            UnscaledFixedDeltaTime = config.FixedUpdateTimingMS * 0.001f;
            fixedUpdateTimeCarry = 0f;
        }

        internal static void Start()
        {
            timer.Start();
        }

        internal static void MeassureElapsedTime()
        {
            UnscaledDeltaTime = timer.Meassure();
            CalculateNumberFixedUpdates();
        }

        private static void CalculateNumberFixedUpdates() 
        {
            float t = fixedUpdateTimeCarry + UnscaledDeltaTime;

            NumberOfFixedUpdates = (int)Math.Floor( t / UnscaledFixedDeltaTime );

            fixedUpdateTimeCarry = t - (NumberOfFixedUpdates * UnscaledFixedDeltaTime);
        }


        internal static void Stop()
        {
            timer.Stop();
        }

        struct Timer
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

            public static Timer Create( Config config, float desiredFrameRate ) 
                => new Timer {
                    sw = new Stopwatch(),
                    FrameCounter = 0,
                    TotalSecondsElapsed = 0,
                    desiredFrameRate = desiredFrameRate,
                    VSync = config.Vsync };
        }

        public struct Config
        {
            public bool Vsync { get; init; }

            public float FixedUpdateTimingMS { get; init; }

        }

    }
}
