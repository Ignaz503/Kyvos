using DefaultEcs;
using Veldrid;
using Veldrid.SPIRV;
using DefaultEcs.Resource;
using Kyvos.Graphics;
using System.Text;
using Kyvos.Graphics.Materials;
using Kyvos.ECS.Resources;
using Kyvos.Core;
using Kyvos.VeldridIntegration;
using System.Diagnostics;
using System;
using Kyvos.Graphics.Vertrices;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    //TODO make more generic and move to Kyvos.ECS.Resources
    //TODO create material blue print that remvoes and adds materials correctly to entity
    //as in if material removed, also remove MangerResource<TInfo,Material> and if 
    //ManagedResource<TInfo,Material> remvoed also remove material
    public class MaterialManager : AResourceManager<string, Material>
    {
        private const string VertexCodeColor = @"
#version 450

layout(location = 0) in vec3 Position;
layout(location = 1) in vec4 Color;

layout(set = 0, binding = 0) uniform ProjectionBuffer
{
    mat4 Projection;
};
layout(set = 0, binding = 1) uniform ViewBuffer
{
    mat4 View;
};

layout(set = 0, binding = 2) uniform WorldBuffer
{
    mat4 World;
};

layout(location = 0) out vec4 fsin_Color;



void main()
{
    vec4 worldPosition = World * vec4(Position, 1);
    vec4 viewPosition = View * worldPosition;
    vec4 clipPosition = Projection * viewPosition;
    gl_Position = clipPosition;  


    fsin_Color = Color;
}";

        private const string FragmentCodeColor = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

layout(set = 1, binding = 0) uniform ColorBuffer
{
    vec4 col;
};

void main()
{
    fsout_Color = fsin_Color * col;
}";

        private const string VertexCodeTexture = @"
#version 450
layout(set = 0, binding = 0) uniform ProjectionBuffer
{
    mat4 Projection;
};
layout(set = 0, binding = 1) uniform ViewBuffer
{
    mat4 View;
};
layout(set = 0, binding = 2) uniform WorldBuffer
{
    mat4 World;
};
layout(location = 0) in vec3 Position;
layout(location = 1) in vec2 TexCoords;
layout(location = 0) out vec2 fsin_texCoords;
void main()
{
    vec4 worldPosition = World * vec4(Position, 1);
    vec4 viewPosition = View * worldPosition;
    vec4 clipPosition = Projection * viewPosition;
    gl_Position = clipPosition;  
    fsin_texCoords = TexCoords;
}";
        private const string FragmentCodeTexture = @"
#version 450
layout(location = 0) in vec2 fsin_texCoords;
layout(location = 0) out vec4 fsout_color;
layout(set = 1, binding = 0) uniform texture2D SurfaceTexture;
layout(set = 1, binding = 1) uniform sampler SurfaceSampler;
void main()
{
    fsout_color =  texture(sampler2D(SurfaceTexture, SurfaceSampler), fsin_texCoords);
}";

        MaterialLoader materialLoader;
        GraphicsDeviceHandle gfxDeviceHandle;


        public MaterialManager(IApplication app)
        {
            gfxDeviceHandle = GetComponentFromApp<GraphicsDeviceHandle>(app);
            materialLoader = GetComponentFromApp<MaterialLoader>(app);
        }

        T GetComponentFromApp<T>(IApplication app)
        {
            Debug.Assert(app.HasComponent<T>());
            return app.GetComponent<T>()!;
        }

        protected override Material Load(string info)
        {
            if (info == "textureMat")
                return LoadTextureMat();
            return LoadColorMat();
        }

        Material LoadTextureMat() 
        {
            
            var materialDescription = new MaterialDescription()
            {
                Name = new("basic texture mat"),
                ShaderSetDescription = new ShaderProgramDescription()
                {
                    VertexShaderDescription = new() { EntryPoint = "main", ShaderStages = ShaderStages.Vertex, Code = new() { StorageIdentifier = ShaderCode.StorageType.EmbeddedCode, Data = VertexCodeTexture } },
                    FragmentShaderDescription = new() { EntryPoint = "main", ShaderStages = ShaderStages.Fragment, Code = new() { StorageIdentifier = ShaderCode.StorageType.EmbeddedCode, Data = FragmentCodeTexture } }
                },
                VertexLayouts = new[] { VertexPositionTexture.Description },
                BlendStateDescription = BlendStateDescription.SingleOverrideBlend,
                DepthStencilStateDescription = new(depthTestEnabled: true, depthWriteEnabled: true, comparisonKind: ComparisonKind.LessEqual),
                RasterizerStateDescription = new(cullMode: FaceCullMode.Back, fillMode: PolygonFillMode.Solid, frontFace: FrontFace.Clockwise, depthClipEnabled: true, scissorTestEnabled: false),
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                PropertySetDescription = new[]
                {
                    new Material.PropertySetDescription(
                        type: Material.PropertySet.Ownership.Global,
                        setIdx: 0,
                        new Material.Mat4BufferDescription(){Order = 0, Name ="ProjectionBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex},
                        new Material.Mat4BufferDescription(){Order = 1, Name ="ViewBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex},
                        new Material.Mat4BufferDescription(){Order = 2, Name ="WorldBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex}
                        ),
                    new Material.PropertySetDescription(
                        type: Material.PropertySet.Ownership.PerMaterial,
                        setIdx: 1,
                        new Material.TexturePropertyDescription()
                        {
                            Name="SurfaceTexture",
                            Order = 0,
                            AssetID = new(TemporaryConsts.TextureToUse),
                            Stages=ShaderStages.Fragment,
                            Kind=ResourceKind.TextureReadOnly
                        },
                        new Material.SamplerPropertyDescripton()
                        {
                            Name="SurfaceSampler",
                            Order = 1,
                            DefaultSampler = Material.SamplerPropertyDescripton.DefaultSamplerType.Aniso4x,
                            Stages=ShaderStages.Fragment,
                            Kind=ResourceKind.Sampler
                        }
                    )
                },
                OutputDescription = gfxDeviceHandle.GfxDevice.SwapchainFramebuffer.OutputDescription
            };

            return materialLoader.Get(materialDescription);
        }

        private Material LoadColorMat()
        {
            var materialDescription = new MaterialDescription()
            {
                Name = new("basic color vert"),
                ShaderSetDescription = new ShaderProgramDescription()
                {
                    VertexShaderDescription = new() { EntryPoint = "main", ShaderStages = ShaderStages.Vertex, Code = new() { StorageIdentifier = ShaderCode.StorageType.EmbeddedCode, Data = VertexCodeColor } },
                    FragmentShaderDescription = new() { EntryPoint = "main", ShaderStages = ShaderStages.Fragment, Code = new() { StorageIdentifier = ShaderCode.StorageType.EmbeddedCode, Data = FragmentCodeColor } }
                },
                VertexLayouts = new[] { VertexPositionColor.Description },
                BlendStateDescription = BlendStateDescription.SingleOverrideBlend,
                DepthStencilStateDescription = new(depthTestEnabled: true, depthWriteEnabled: true, comparisonKind: ComparisonKind.LessEqual),
                RasterizerStateDescription = new(cullMode: FaceCullMode.Back, fillMode: PolygonFillMode.Solid, frontFace: FrontFace.Clockwise, depthClipEnabled: true, scissorTestEnabled: false),
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                PropertySetDescription = new[]
                {
                    new Material.PropertySetDescription(
                        type: Material.PropertySet.Ownership.Global,
                        setIdx: 0,
                        new Material.Mat4BufferDescription(){Order = 0, Name ="ProjectionBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex},
                        new Material.Mat4BufferDescription(){Order = 1, Name ="ViewBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex},
                        new Material.Mat4BufferDescription(){Order = 2, Name ="WorldBuffer", Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Vertex}
                        ),
                    new Material.PropertySetDescription(
                        type: Material.PropertySet.Ownership.PerMaterial,
                        setIdx: 1,
                        new Material.ColorBufferDescription(){ Order = 0, Name = "ColorBuffer", ColorType = Material.ColorBufferDescription.ColorLength.Vec4, Color = new(1f,1f,1f,1), Kind = ResourceKind.UniformBuffer, Stages = ShaderStages.Fragment}
                    )
                },
                OutputDescription = gfxDeviceHandle.GfxDevice.SwapchainFramebuffer.OutputDescription
            };

            return materialLoader.Get(materialDescription);
        }

        protected override void OnResourceLoaded(in Entity entity, string info, Material resource)
        {
            entity.Set(resource);
        }
    }
}
