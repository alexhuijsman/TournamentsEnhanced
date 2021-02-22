using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase<FinderBaseImpl>
  {
    [Test]
    public void Find_NoCandidate_ManyComparers_Result_ShouldFail()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_ManyComparers_Result_ShouldNotSucceed()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_ManyComparers_ResultNominee_ShouldBeNull()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_ManyComparers_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_ManyComparers_ResultRunnerUp_ShouldBeNull()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_ManyComparers_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpManyMockComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }

  }
}