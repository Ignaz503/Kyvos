using DefaultEcs;
using DefaultEcs.System;
using Kyvos.ECS.Systems.Rendering;
using System;

namespace Kyvos.ECS.Systems;

public class EmptyFloatSytem : ISystem<float>
{
    public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Dispose()
    {}

    public void Update(float state)
    {
        throw new NotImplementedException();
    }
}

//public class SystemDesignerTets
//{
//    public void Test()
//    {
//        var designer = new SystemDesigner<float>()
//            .With<TestSystem, RenderContext>(world => new TestSystem(world),
//                connectionBuilder =>
//                {
//                    connectionBuilder.Before<OtherSystem>()
//                    .After<OhterOtherSystem>();
//                },
//                childSystems => childSystems.With<DummyRenderSystem,RenderContext>(w => new DummyRenderSystem(),connectionBuilder => { }, children => { }));


//        designer.Validate();//no loops

//        var system = designer.Build();

//        system.Update(0.1f);
//    }
//}

//internal class DummyRenderSystem : ISystem<RenderContext>
//{
//    public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public void Dispose()
//    {
//        throw new NotImplementedException();
//    }

//    public void Update(RenderContext state)
//    {
//        throw new NotImplementedException();
//    }
//}

//internal class OhterOtherSystem : ISystem<float>
//{
//        public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public void Dispose()
//    {
//        throw new NotImplementedException();
//    }

//    public void Update(float state)
//    {
//        throw new NotImplementedException();
//    }
//}

//internal class OtherSystem : ISystem<float>
//{
//    public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public void Dispose()
//    {
//        throw new NotImplementedException();
//    }

//    public void Update(float state)
//    {
//        throw new NotImplementedException();
//    }
//}

//internal class SystemDesigner<SystemContext>
//{
//    public SystemDesigner()
//    {
//    }

//    public SystemDesigner<SystemContext> With<TSystem, ChildContext>(Func<World, TSystem> systemFactory, Action<ConnectionBuilder<SystemContext>> connectionBuilder, Action<SystemDesigner<ChildContext>> children)
//    {
//        throw new NotImplementedException();
//    }

//    public void Validate()
//    {
//        throw new NotImplementedException();
//    }

//    public ISystem<SystemContext> Build()
//    {
//        throw new NotImplementedException();
//    }

//}
//internal class TestSystem : ISystem<float>
//{
//    public TestSystem(World world)
//    {
//    }

//    public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public void Dispose()
//    { }

//    public void Update(float state)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class ConnectionBuilder<TContext>
//{
//    public ConnectionBuilder<TContext> Before<TSystem>()
//            where TSystem : ISystem<TContext>

//    {
//        throw new NotImplementedException();
//    }
        
//    public ConnectionBuilder<TContext> After<TSystem>()
//        where TSystem : ISystem<TContext>

//    {
//        throw new NotImplementedException();
//    }
//}
