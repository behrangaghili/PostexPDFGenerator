namespace Postex.SharedKernel.Domain
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
