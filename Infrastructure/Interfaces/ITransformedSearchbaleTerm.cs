namespace Infrastructure.Interfaces;

public interface ITransformedSearchbaleTerm : ISearchbaleTerm
{
    public float[] Features { get; set; } 
}