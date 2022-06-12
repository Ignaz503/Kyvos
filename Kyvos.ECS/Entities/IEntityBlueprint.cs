using DefaultEcs;
using DefaultEcs.Command;

namespace Kyvos.ECS.Entities;

public interface IEntityBlueprint<TData>
{

    public abstract Entity Create(World w);
    public abstract Entity Create(World w, TData data);

    public abstract EntityRecord Create(WorldRecord record);
    public abstract EntityRecord Create(WorldRecord record, TData data);

    public abstract Entity Apply(Entity entity);
    public abstract Entity Apply(Entity entity, TData data);

    public abstract EntityRecord Apply(EntityRecord record);
    public abstract EntityRecord Apply(EntityRecord record, TData data);

    public abstract Entity Remove(Entity entity);

    public abstract EntityRecord Remove(EntityRecord record);

}


