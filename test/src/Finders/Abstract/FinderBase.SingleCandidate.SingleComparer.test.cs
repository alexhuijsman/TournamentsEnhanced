using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_SingleCandidate_SingleComparer_Result_ShouldNotFail()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_Result_ShouldSucceed()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_ResultNominee_ShouldBeExpected()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidate.Object);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(MBWrapperBaseImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpSingleCandidate();
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}