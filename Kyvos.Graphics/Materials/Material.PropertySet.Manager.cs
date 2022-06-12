using System.Collections.Concurrent;
using System.Collections.Generic;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public partial class PropertySet
    {
        public static class Manager 
        {
            private static readonly ConcurrentDictionary<uint, PropertySet> globals;

            static Manager() 
            {
                globals = new();    
                OnGlobalSetDisposed += OnSetDisposed;
            }

            public static PropertySet Get(PropertySetDescription description, GraphicsDevice gfxDevice) 
            {
                if (globals.TryGetValue(description.SetIndex, out PropertySet? set)) 
                    return set.Reference();

                var newSet = new PropertySet(description,gfxDevice);
                if (newSet.Type == Ownership.Global) 
                {
                    newSet.Reference();
                    globals.TryAdd(newSet.setIndex, newSet);
                }
                return newSet;
            }

            static void OnSetDisposed(PropertySet set) 
            {
                globals.Remove(set.setIndex, out PropertySet _);
            }

            static PropertySet GetSet(uint idx)
            {
#if DEBUG
                if (!globals.ContainsKey(idx))
                    throw new UnkownGlobalSetIdx(idx);
#endif
                return globals[idx];
            }

            public static void Update<T>(uint idx, string propertyName, ref T Data, CommandList cmdList, uint byteOffset = 0)
                where T : struct
            {
                GetSet(idx).Update(propertyName, ref Data, cmdList, byteOffset);
            }
            public static void Update<T>(uint idx, string propertyName, ref T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
                where T : struct
            {
                GetSet(idx).Update(propertyName, ref Data, gfxDevie, byteOffset);
            }

            public static void Update<T>(uint idx, string propertyName, T Data, CommandList cmdList, uint byteOffset = 0)
                where T : struct
            {
                GetSet(idx).Update(propertyName, ref Data, cmdList, byteOffset);
            }
            public static void Update<T>(uint idx, string propertyName, T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
                where T : struct
            {
                GetSet(idx).Update(propertyName, ref Data, gfxDevie, byteOffset);
            }

            public static void Update(uint idx, string propertyName, Texture texture, GraphicsDevice gfxDevice)
            {
                GetSet(idx).Update(propertyName, texture, gfxDevice);
            }

            public static void Update(uint idx, string propertyName, Sampler sampler, GraphicsDevice gfxDevice)
            {
                GetSet(idx).Update(propertyName, sampler, gfxDevice);
            }
            static PropertySet FindSetForProperty(string name)
            {
                foreach (var property in globals.Values)
                    if (property.HasProperty(name))
                        return property;

                throw new NonExistentPropertyException(name);
            }
            public static void Update<T>(string propertyName, ref T Data, CommandList cmdList, uint byteOffset = 0)
                where T : struct
            {
                FindSetForProperty(propertyName).Update(propertyName, ref Data, cmdList, byteOffset);
            }
            public static void Update<T>(string propertyName, ref T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
                where T : struct
            {
                FindSetForProperty(propertyName).Update(propertyName, ref Data, gfxDevie, byteOffset);
            }

            public static void Update(string propertyName, Texture texture, GraphicsDevice gfxDevice)
            {
                FindSetForProperty(propertyName).Update(propertyName, texture, gfxDevice);
            }


        }

    }
}

