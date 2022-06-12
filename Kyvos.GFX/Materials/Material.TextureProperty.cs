using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class TextureProperty : Property
    {
        Kyvos.Graphics.Texture texture;
        TextureView textureView;

        internal override BindableResource Bindable => textureView;

        internal TextureProperty(Texture texture,int order, GraphicsDevice gfxDevice)
            : base(order)
        {
            this.texture = texture;
            this.textureView = texture.GetTextureView(gfxDevice);
        }

        public void Update(Texture texture, GraphicsDevice gfxDevice) 
        {
            this.texture.Dispose();
            this.textureView.Dispose();

            this.texture = texture;
            this.textureView = texture.GetTextureView(gfxDevice);
        }

        public override void Dispose()
        {
            if (isDisposed)
                return;

            texture.Dispose();
            textureView.Dispose();

            isDisposed = true;
        }
    }



}

