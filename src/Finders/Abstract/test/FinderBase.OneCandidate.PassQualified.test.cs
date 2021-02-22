using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase<FinderBaseImpl>
  {
    private void SetUpQualifiedCandidateToQualify()
    {
      SetUpMockCandidate(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_Result_ShouldNotFail()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_Result_ShouldSucceed()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_ResultNominee_ShouldBeExpected()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_candidates[0]);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_ResultRunnerUp_ShouldBeNull()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_PassQualified_ResultAllQualifiedCandidates_CountShouldBeOne()
    {
      SetUpQualifiedCandidateToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.ShouldHaveSingleItem();
    }

  }
}