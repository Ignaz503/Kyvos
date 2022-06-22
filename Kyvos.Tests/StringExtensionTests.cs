using Kyvos.Utility;
using Xunit.Abstractions;
using Faker;

namespace Kyvos.Tests;

public class StringExtensionTestss
{

    ITestOutputHelper output;

    public StringExtensionTestss(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void CorrectSplitSingleCharTest() 
    {
        const char sepertor = ' ';
        var words = Lorem.Words(3).ToArray();
        var myTest = string.Join(" ", words);
        output.WriteLine(myTest);

        int i = 0;
        foreach (var entry in myTest.AsSpan().Split(sepertor)) 
        {
            Assert.True(i < words.Length,"Too many splits");
            Assert.True(MemoryExtensions.Equals(words[i].AsSpan(),entry, StringComparison.Ordinal), $"{words[i]} != {entry}");
            i++;
        }
    }
    [Fact]
    public void CorrectSplitMultiTest()
    {
        const string sepertor = "](";
        var words = Lorem.Words(3).ToArray();
        var myTest = string.Join("](", words);
        output.WriteLine(myTest);

        int i = 0;
        foreach (var entry in myTest.AsSpan().Split(sepertor))
        {
            Assert.True(i < words.Length, "Too many splits");
            Assert.True(MemoryExtensions.Equals(words[i].AsSpan(), entry, StringComparison.Ordinal), $"{words[i]} != {entry}");
            i++;
        }
    }
}

