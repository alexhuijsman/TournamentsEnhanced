using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase<FinderBaseImpl>
  {
    [Test]
    public void Find_NoCandidate_NoComparer_Result_ShouldFail()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_NoComparer_Result_ShouldNotSucceed()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_NoComparer_ResultNominee_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_NoComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_NoComparer_ResultRunnerUp_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }

  }
}