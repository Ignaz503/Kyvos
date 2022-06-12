using System.Numerics;

namespace Kyvos.ECS.Components
{
    public struct Transform
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public Vector3 Forward =>
            Vector3.Transform(-Vector3.UnitZ, Rotation);

        public Transform()
        {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = Vector3.One;
        }

        public Matrix4x4 Matrix
        {
            get => Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateTranslation(Position);
            set =>
                Matrix4x4.Decompose(value, out Scale, out Rotation, out Position);
        }

    }
}
