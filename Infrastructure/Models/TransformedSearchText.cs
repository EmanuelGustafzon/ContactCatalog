using Infrastructure.Interfaces;

namespace Infrastructure.Models;

internal class TransformedSearchText : SearchText, ITransformedSearchbaleTerm
{
    public float[] Features { get; set; } = [];
}
