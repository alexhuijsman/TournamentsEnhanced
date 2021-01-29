using Moq;

namespace Test
{
  public class TestBase
  {
    protected MockRepository MockRepository { get; } = new MockRepository(MockBehavior.Strict);
  }
}