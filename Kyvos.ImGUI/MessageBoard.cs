using Kyvos.Utility.Collections;
using DefaultEcs.System;
using DefaultEcs;
using DefaultEcs.Threading;
using ImGuiNET;
using Kyvos.Maths.Tweening;
using System.Numerics;

namespace Kyvos.ImGUI;

public class MessageBoard : UILeafNode
{
    RoundRobinArray<Message> messages;
    object lockObj;
    public MessageBoard(int maxMessagesAtOnce = 10, float messagesShownForSeconds = 10f)
    {
        messages = new RoundRobinArray<Message>(maxMessagesAtOnce, new MessageInitializer(messagesShownForSeconds));
        lockObj = new();
    }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.Begin(nameof(MessageBoard),ImGuiWindowFlags.Modal);

        for (int i = messages.Size - 1; i >= 0; i--)
        {
            messages[messages.Head + i].Show();
        }

        ImGui.End();
    }

    void Update(float deltaTime)
    {
        for (int i = 0; i < messages.Size; i++)
        {
            ref var msg = ref messages.GetRef(i);
            msg.Update(deltaTime);
        }
    }

    public void AddMessage(string message)
    {
        lock (lockObj) 
        {
            ref var msg = ref messages.GetRef(messages.Head);
            msg.Text.Content = message;
            msg.ResetTimer();
            messages.AdvanceHead();
            //messages[messages.Head].Text.Content = message;
            //messages[messages.Head].ResetTimer();
            //messages.AdvanceHead();
        }
    }

    public override void Dispose()
    {}

    struct MessageInitializer : IArrayInitializer<Message>
    {
        float maxTime;
        public MessageInitializer(float maxTime)
        {
            this.maxTime = maxTime;       
        }

        public Message GetForIdx(int idx)
        {
            return new("", maxTime, maxTime);
        }
    }

    struct Message 
    {
        public ColoredText Text;
        
        float time;
        float maxTime;
        
        public Message(string text, float maxTime)
        {
            Text = new(text);
            time = 0;
            this.maxTime = maxTime;
        }

        public Message(string text,float time,  float maxTime)
        {
            Text = new(text);
            this.time = time;
            this.maxTime = maxTime;
        }

        public void Update(float deltaTime)
        {
            time += deltaTime;

            var alpha = Tween.EaseOutQuint(1f-(time / maxTime));

            Text.Color = Vector4.One * alpha;

        }

        public void ResetTimer()
            => time = 0;

        public bool IsShown
            => time < maxTime;

        public void Show() 
        {
            if(IsShown)
                Text.Show();
        }

    }

    public class System : AComponentSystem<float, MessageBoard>
    {
        public System(World world) : base(world)
        {
        }

        public System(World world, IParallelRunner runner) : base(world, runner)
        {
        }

        public System(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {
        }

        protected override void Update(float deltaTime, ref MessageBoard component)
        {
            component.Update(deltaTime);
        }
    }

}
