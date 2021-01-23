using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private Mock<MBWrapperBaseImpl> _mockCandidate;
    private void SetUpSingleCandidate()
    {
      _mockCandidate = new Mock<MBWrapperBaseImpl>();
      _mockCandidate.SetupGet(candidate => candidate.IsNull).Returns(false);
      _candidates.Add(_mockCandidate.Object);
    }

    [Test]
    public void Find_SingleCandidate_Result_ShouldNotFail()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_Result_ShouldSucceed()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_ResultNominee_ShouldBeExpected()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidate.Object);
    }

    [Test]
    public void Find_SingleCandidate_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_ResultRunnerUp_ShouldBeNull()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(MBWrapperBaseImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpSingleCandidate();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}