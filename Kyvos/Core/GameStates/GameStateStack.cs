using Kyvos.Core.GameStates.Builder.Stages;
using System;
using System.Collections.Generic;
using Kyvos.Core.GameStates.Exceptions;
using Kyvos.Core.Applications;

namespace Kyvos.Core.GameStates
{
    internal class GameStateStack : IDisposable
    {
        Node topNode;

        internal GameState Top => topNode.GameState;

        Dictionary<string, IStateBuilderFinalStage> builders;

        internal GameStateStack(IEnumerable<IStateBuilderFinalStage> builderList)
        {
            builders = new( Expand( builderList ) );
        
            IEnumerable<KeyValuePair<string, IStateBuilderFinalStage>> Expand( IEnumerable<IStateBuilderFinalStage> builderList)
            {
                foreach (var builder in builderList)
                    yield return new KeyValuePair<string, IStateBuilderFinalStage>( builder.Name, builder );
            }
        }

        public void Pop() 
        {
            var newTop = topNode.Previous;

            var old = topNode;

            old.Previous = null;

            var state = old.GameState.StackBehaviour.PopBehaviour( old.GameState );
            old.GameState.CurrentState = state;
            state = newTop.GameState.StackBehaviour.MoveToTopBehaviour( newTop.GameState );
            newTop.GameState.CurrentState = state;
            topNode = newTop;
            
        }

        public void Push( string stateName, bool unloadPrev = false) 
        {
            if (!builders.ContainsKey( stateName ))
                throw new UnknownGameStateName( stateName );

            var prev = topNode;
            var builder = builders[stateName];


            topNode = new(){GameState = builder.Build(Application.Instance.AppData.WindowData,Application.Instance.AppData.GfxDevice), Previous = prev};

            if (prev != null)
            { 
                var internalState = prev.GameState.StackBehaviour.MoveDownBehaviour( prev.GameState, unloadPrev );
                prev.GameState.CurrentState = internalState;
            }

            var state = topNode.GameState.StackBehaviour.PushBehaviour( topNode.GameState );
            topNode.GameState.CurrentState = state;
        }

        //TOODO maybe instead of dispose use unload
        public void MoveOnTop( string name, bool unloadPrev = false ) 
        {
            (var current, var next)= TraversToName(name);

            ExtractNodeFromGraph( current, next );

            //get old top node and point to new top node
            //point new topNode prev to old top node, and remove any potential next from new top node
            var oldTopNode = topNode;
            current.Previous = topNode;
            topNode = current;

            var state = oldTopNode.GameState.StackBehaviour.MoveDownBehaviour( oldTopNode.GameState, unloadPrev );
            oldTopNode.GameState.CurrentState = state;
            state = topNode.GameState.StackBehaviour.MoveToTopBehaviour( topNode.GameState );
            topNode.GameState.CurrentState = state;
        }

        void ExtractNodeFromGraph( Node n, Node next) 
        {
            //remove current node from where it is currently
            //by  making prev <--- current <--- next
            //into prev <--- next
            var prev = n.Previous;

            if (next != null)
                next.Previous = prev;

            n.Previous = null;
        }

        (Node node, Node next) TraversToName( string name ) 
        {
            var current = topNode;
            Node next = null;

            while (current != null)
            {
                if (current.GameState.Name == name)
                    return (current, next);
                next = current;
                current = current.Previous;
            }
            throw new NoPreviousStateWithName( name );
        }

        public void Remove( string name ) 
        {
           (var node, var next) = TraversToName(name);
            ExtractNodeFromGraph( node, next );

            node.GameState.Dispose();
        }


        public void Dispose()
        {
            var current = topNode;
            while (current != null)
            {
                current.GameState.Dispose();
                var prev = current.Previous;
                //destroy links
                current.Previous = null;
                current = prev;
            }
        }

        class Node 
        {
            public GameState GameState { get; init; }

            public Node Previous { get; set; }
        }

    }

}
