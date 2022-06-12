using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Maths;
using Kyvos.ECS.Components;
using Kyvos.Input;
using System.Numerics;
using System;
using Kyvos.ECS.Components.Rendering;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class CameraMoveSystem : AComponentSystem<float, Camera>
    {

        const float Sensitivity = 1f;

        Vector2 mPos;
        public CameraMoveSystem(World world) : base(world)
        {
            mPos = World.Get<MouseAndKeyboard>().MousePosition;
        }

        protected override void Update(float deltaTime, ref Camera component)
        {
            ref var input = ref World.Get<MouseAndKeyboard>();
            var change = input.MouseDelta;
            var l = change.Length();
            if(!(Mathf.AlmostEquals(l,0) || Mathf.AlmostEquals(l,1)))
                change = Vector2.Normalize(change);

            CalculateLookUpDown(deltaTime, ref component, change.Y);

            CalculateLookLeftRight(deltaTime, ref component, change.X);


            mPos = input.MousePosition;

        }

        private void CalculateLookLeftRight(float deltaTime, ref Camera component, float change)
        {
            var rot = component.Yaw + (change * Sensitivity) * deltaTime;

            //TODO clamp look up and down
            component.Yaw = rot;
        }

        void CalculateLookUpDown(float deltaTime, ref Camera component, float change) 
        {
            var rot = component.Pitch + (change * Sensitivity) * deltaTime;

            //TODO clamp look up and down
            component.Pitch = rot;
        }

    }


}
