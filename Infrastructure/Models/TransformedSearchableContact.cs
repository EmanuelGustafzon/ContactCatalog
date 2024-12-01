using Infrastructure.Interfaces;

namespace Infrastructure.Models;

internal class TransformedSearchableContact : SearchableContact, ITransformedSearchbaleTerm
{
    public float[] Features { get; set; } = [];
}
