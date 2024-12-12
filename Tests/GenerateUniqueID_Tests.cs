using Infrastructure.Services;

namespace Tests;

public class GenerateUniqueID_Tests
{

    [Fact]
    public void GenerateShould_GenerateNewIDAsString()
    {
        string id = GenerateUniqueID.Generate();

        Assert.IsType<string>(id);
        Assert.NotEmpty(id);
    }
}
