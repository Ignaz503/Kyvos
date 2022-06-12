using Kyvos.Core.Assets;
using Kyvos.Utility.Exceptions;
using System.Collections.Concurrent;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public static class Manager
    {
        static readonly ConcurrentDictionary<string, Material> materials;

        static Manager() 
        {
            materials = new();
            Material.OnDispose += OnMaterialDisposed;  //does this open a memory leak cause we never unregister but then  again it's a static class with static lifetime
        }

        public static Material Get(MaterialDescription description, GraphicsDevice gfxDevice) 
        {
            if (materials.TryGetValue(description.Name, out Material? m)) 
            {
                return m.Reference();
            }

            Material newMat = new(description, gfxDevice);
            materials.TryAdd(description.Name,newMat);
            return newMat.Reference();
        }

        public static Material Get(AssetIdentifier assetID, GraphicsDevice gfxDevice) 
        {
            throw new TODO("Asset Manager loading of material description");
        }

        public static void OnMaterialDisposed(Material mat)
        {
            materials.TryRemove(mat.name, out Material _);
        }


    }
}