using Kyvos.Maths;
using Kyvos.Core.Memory;
using System.Numerics;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public abstract class BufferPropertyDescription : PropertyDescription
    {
        public abstract BufferDescription BufferDescription { get; }

        public abstract void InitializeBuffer(BufferProperty prop, GraphicsDevice gfxDevice);

        public override Property Get(GraphicsDevice gfxDevice)
        {
            var prop = new BufferProperty(BufferDescription,Order,gfxDevice);
            InitializeBuffer(prop,gfxDevice);
            return prop;
        }
    }

    public class Mat4BufferDescription : BufferPropertyDescription
    {
        public override BufferDescription BufferDescription => new(Size.Of_U<Matrix4x4>(), BufferUsage.UniformBuffer);

        public override void InitializeBuffer(BufferProperty prop, GraphicsDevice gfxDevice)
        {
            
        }
    }

    public class ColorBufferDescription : BufferPropertyDescription
    {
        public enum ColorLength 
        {
            Vec3,
            Vec4
        }

        public ColorLength ColorType { get; init; }
        public RgbaFloat Color { get; init; }
        public override BufferDescription BufferDescription
            => new(ColorType == ColorLength.Vec3 ? Size.Of_U<Vector3>() : Size.Of_U<Vector4>(), BufferUsage.UniformBuffer);

        public override void InitializeBuffer(BufferProperty prop, GraphicsDevice gfxDevice)
        {
            switch(ColorType)
            {
               case ColorLength.Vec4:
                    prop.Update(Color.ToVector4(), gfxDevice);
                    break;
               case ColorLength.Vec3:
                    prop.Update(Color.ToVector4().XYZ(), gfxDevice);
                    break;
            }
        }
    }
}

