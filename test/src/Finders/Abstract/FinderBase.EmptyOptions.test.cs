using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_EmptyOptions_DoesNotThrowException()
    {
      Should.NotThrow(() => _sut.Find(_mockFindOptions.Object));
    }

    [Test]
    public void Find_EmptyOptions_Result_ShouldFail()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_EmptyOptions_Result_ShouldNotSucceed()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_EmptyOptions_ResultNominee_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(MBWrapperBaseImpl.Null);
    }

    [Test]
    public void Find_EmptyOptions_ResultHasRunnerUp_ShouldBeFalse()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_EmptyOptions_ResultRunnerUp_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(MBWrapperBaseImpl.Null);
    }

    [Test]
    public void Find_EmptyOptions_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}