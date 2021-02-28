using Moq;

namespace Test.WrapperLib
{
  public class TestBase
  {
    protected MockRepository MockRepository { get; } = new MockRepository(MockBehavior.Strict);
  }

  public class TestBase<T> : TestBase
  where T : new()
  {
    protected T _sut;

    protected virtual void SetUp()
    {
      _sut = new T();
    }
  }
}
