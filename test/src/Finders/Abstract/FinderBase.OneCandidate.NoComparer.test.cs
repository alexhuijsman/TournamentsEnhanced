using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private void SetUpQualifiedCandidateWithoutComparer()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
    }
    [Test]
    public void Find_OneCandidate_NoComparer_Result_ShouldNotFail()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_NoComparer_Result_ShouldSucceed()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_NoComparer_ResultNominee_ShouldBeExpected()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidates[0].Object);
    }

    [Test]
    public void Find_OneCandidate_NoComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_NoComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_NoComparer_ResultAllQualifiedCandidates_CountShouldBeOne()
    {
      SetUpQualifiedCandidateWithoutComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}