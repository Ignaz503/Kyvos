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

        public abstract void InitializeBuffer(BufferProperty prop, CreationContext ctx);

        public override Property Get(CreationContext ctx)
        {
            var prop = new BufferProperty(BufferDescription,Order,ctx.GfxDevice);
            InitializeBuffer(prop,ctx);
            return prop;
        }
    }

    public class Mat4BufferDescription : BufferPropertyDescription
    {
        public override BufferDescription BufferDescription => new(Size.Of_U<Matrix4x4>(), BufferUsage.UniformBuffer);

        public override void InitializeBuffer(BufferProperty prop, CreationContext ctx)
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

        public override void InitializeBuffer(BufferProperty prop, CreationContext ctx)
        {
            switch(ColorType)
            {
               case ColorLength.Vec4:
                    prop.Update(Color.ToVector4(), ctx.GfxDevice);
                    break;
               case ColorLength.Vec3:
                    prop.Update(Color.ToVector4().XYZ(), ctx.GfxDevice);
                    break;
            }
        }
    }
}

