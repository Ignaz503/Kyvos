using System.Numerics;
using System.Runtime.CompilerServices;
using Veldrid;

namespace Kyvos.Core.Inputs
{
    public static class Input
    {
        static ChangeTracker inputStateTraker = ChangeTracker.Default;

        static InputSnapshot snapshot;

        public static float ScrollWheelDelta => snapshot.WheelDelta;

        public static Vector2 MousePosition => snapshot.MousePosition;

        internal static void Update( InputSnapshot s )
        {
            inputStateTraker.Advance();
            snapshot = s;

            var keyEvents = s.KeyEvents;

            for (int i = 0; i < keyEvents.Count; i++)
            {
                inputStateTraker.Update( keyEvents[i] );
            }

            var mouseEvents = s.MouseEvents;

            for (int i = 0; i < mouseEvents.Count; i++)
            {
                inputStateTraker.Update( mouseEvents[i] );
            }
        }

        public static bool IsDown( Key k ) => inputStateTraker.IsDown( k );
        public static bool IsDown( MouseButton b ) => inputStateTraker.IsDown( b );

        public static bool IsReleased( Key k ) => inputStateTraker.IsReleased( k );
        public static bool IsReleased( MouseButton b ) => inputStateTraker.IsReleased( b );

        public static bool IsPressed( Key k ) => inputStateTraker.IsPressed( k );
        public static bool IsPressed( MouseButton b ) => inputStateTraker.IsPressed( b );

        public static string StringifyState() => inputStateTraker.Stringify();

        struct ChangeTracker
        {
            InputState previous;
            InputState current;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal bool CheckKeyCurrent( Key t ) => current[t];

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool CheckKeyPrevious( Key t ) => previous[t];

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsPressed( Key t ) => CheckKeyCurrent( t ) && CheckKeyPrevious( t );

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsDown( Key t ) => CheckKeyCurrent( t ) && !CheckKeyPrevious( t );

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsReleased( Key t ) => !CheckKeyCurrent( t ) && CheckKeyPrevious( t );

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool CheckButtonCurrent( MouseButton t ) => current[t];

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool CheckButtonPrevious( MouseButton t ) => previous[t];

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsPressed( MouseButton t ) => CheckButtonCurrent( t ) && CheckButtonPrevious( t );

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsDown( MouseButton t ) => CheckButtonCurrent( t ) && !CheckButtonPrevious( t );

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            internal bool IsReleased( MouseButton t ) => !CheckButtonCurrent( t ) && CheckButtonPrevious( t );

            internal void Advance()
            {
                previous = current;
                //current = InputState.Default;
            }

            internal void Update( KeyEvent @event )
            {
                current[@event.Key] = @event.Down;
            }

            internal void Update( MouseEvent @event )
            {
                current[@event.MouseButton] = @event.Down;
            }

            internal string Stringify()
            {
                return $"current State: {current}\nprevious State: {previous}";
            }

            internal static ChangeTracker Default => new ChangeTracker() { current = InputState.Default, previous = InputState.Default };
        }
    }
}
