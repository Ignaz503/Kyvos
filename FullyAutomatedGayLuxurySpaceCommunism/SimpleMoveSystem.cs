using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Input;
using System.Numerics;
using Kyvos.ECS.Components;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    [With(typeof(Transform))]
    public class SimpleMoveSystem : AEntitySetSystem<float>
    {
        float acc;
        public SimpleMoveSystem(World world) : base(world)
        {

        }
        protected override void Update(float deltaTime, in Entity entity)
        {
            ref var input = ref World.Get<MouseAndKeyboard>();
            const float speed = 5f;

            Vector3 dir = Vector3.Zero;

            if (input.IsPressed(Key.W))
                dir.Y += speed;
            if (input.IsPressed(Key.S))
                dir.Y -= speed;

            if (input.IsPressed(Key.A))
                dir.X += speed;
            if (input.IsPressed(Key.D))
                dir.X -= speed;


            ref var trans = ref entity.Get<Transform>();
            trans.Position += dir * deltaTime;

            trans.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (acc += deltaTime)) * Quaternion.CreateFromAxisAngle(Vector3.UnitX, (acc += deltaTime));

        }
    }
}
