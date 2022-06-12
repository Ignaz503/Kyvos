using System.Diagnostics;
using Veldrid;

namespace Kyvos.VeldridIntegration.Materials;

public partial class Material
{
    public class BufferProperty : Property
    {
        DeviceBuffer buffer;

        internal override BindableResource Bindable => buffer;

        internal BufferProperty(BufferDescription description,int order, GraphicsDevice gfxDevice)
            :base(order)
        {
            buffer = gfxDevice.ResourceFactory.CreateBuffer(description);
        }

        public void Update<T>(ref T data, CommandList cmdList, uint byteOffset = 0)
        where T: struct
        {
            cmdList.UpdateBuffer(buffer,byteOffset,ref data);
        }

        public void Update<T>(ref T data,GraphicsDevice gfxDevice, uint byteOffset = 0)
            where T : struct
        {
            gfxDevice.UpdateBuffer(buffer,byteOffset,ref data);
        }

        public void Update<T>(T data, CommandList cmdList, uint byteOffset = 0)
            where T : struct
        {
            cmdList.UpdateBuffer(buffer, byteOffset, ref data);
        }

        public void Update<T>(T data, GraphicsDevice gfxDevice, uint byteOffset = 0)
            where T : struct
        {
            gfxDevice.UpdateBuffer(buffer, byteOffset, ref data);
        }

        public override void Dispose()
        {
            if (isDisposed)
                return;
            buffer.Dispose();
            isDisposed = true;
        }
    }



}

