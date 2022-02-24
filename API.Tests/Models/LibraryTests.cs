using Xunit;
using API.Data.Models;
using API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace API.Tests;

public class LibraryTests
{
  private Library _sut;

  public LibraryTests()
  {
    _sut = new Library();
    //Add languages
    foreach (var languageCode in LanguageData())
    {
      _sut.AddLanguageCode(languageCode);
    }
    //Add literature
    foreach (var literature in LiteratureData())
    {
      _sut.AddLiterature(literature);
    }
  }

  [Fact]
  public void TestAddingDuplicateLangauges()
  {
    _sut = new Library();
    var languageCode = LanguageData().First();
    _sut.AddLanguageCode(languageCode);               //Add langauge
    var result = _sut.AddLanguageCode(languageCode); //Add language again
    Assert.False(result);
  }

  [Fact]
  public void TestAddingNullLangauge()
  {
    _sut = new Library();
    var result = _sut.AddLanguageCode(null);
    Assert.False(result);
  }

  [Fact]
  public void TestAddingEmptyLangauge()
  {
    _sut = new Library();
    var result = _sut.AddLanguageCode(new LanguageCode());
    Assert.False(result);
  }

  [Fact]
  public void TestSettingLangauges()
  {
    _sut = new Library();
    
    var result = _sut.AddLanguageCode(new LanguageCode());
    Assert.False(result);
  }

  [Theory]
  [MemberData(nameof(TestLanguageData))]
  public void TestIdentifyLanguageFromCode(string code, string expected)
  {
    var result = _sut.LookupCode(code);
    Assert.Equal(result.LanguageCode.Language, expected);
  }

  [Theory]
  [MemberData(nameof(TestLiteratureData))]
  public void TestIdentifyLiteratureFromCode(string code, string expected)
  {
    var result = _sut.LookupCode(code);
    Assert.Equal(result.Literature.Name, expected);
  }

  [Theory]
  [MemberData(nameof(TestEditionData))]
  public void TestEditionFoundFromCode(string code, bool expected)
  {
    var result = _sut.LookupCode(code);
    Assert.Equal(result.Year.HasValue, expected);
    Assert.Equal(result.Number.HasValue, expected);
  }

  [Theory]
  [MemberData(nameof(TestEditionValueData))]
  public void TestEditionValueFromCode(string code, int year, int number)
  {
    var result = _sut.LookupCode(code);
    Assert.Equal(result.Year, year);
    Assert.Equal(result.Number, number);
  }

  public static IEnumerable<LanguageCode> LanguageData()
  {
    yield return new LanguageCode() { Code = "E", Language = "English" };
    yield return new LanguageCode() { Code = "S", Language = "Spanish" };
    yield return new LanguageCode() { Code = "CA", Language = "Shona" };
    yield return new LanguageCode() { Code = "H", Language = "Hungarian" };
    yield return new LanguageCode() { Code = "HI", Language = "Hindi" };
  }

  public static IEnumerable<Literature> LiteratureData()
  {
    yield return new Literature() { Name = "Awake!", FullName = "Awake!", Symbol = "g", EditionsPerYear = 3 };
    yield return new Literature() { Name = "Workbook", FullName = "Our Christian Life and Ministry Meeting Workbook", Symbol = "mwb" };
    yield return new Literature() { Name = "Tract - Dead", FullName = "Can the Dead Really Live Again?", Symbol = "T-35" };
    yield return new Literature() { Name = "Watchtower", FullName = "Watchtower (Public)", Symbol = "wp", EditionsPerYear=3 };
    yield return new Literature() { Name = "Watchtower", FullName = "Watchtower (Study)", Symbol = "w", EditionsPerYear=12 };
    yield return new Literature() { Name = "Pure Worship", FullName = "Pure Worship of Jehovah - Restored at Last!", Symbol = "rr" };
  }

  public static IEnumerable<object[]> TestLanguageData()
  {
    yield return new object[] {"rr-E", "English"};
    yield return new object[] {"mwb22.03-E", "English"};
    yield return new object[] {"T-35-E", "English"};
    yield return new object[] {"g21.1-CA", "Shona"};
    yield return new object[] {"wp21.3-H", "Hungarian"};
    yield return new object[] {"wp21.3-HI", "Hindi"};
    yield return new object[] {"w22.3-E", "English"};
  }

  public static IEnumerable<object[]> TestLiteratureData()
  {
    yield return new object[] {"rr-E", "Pure Worship"};
    yield return new object[] {"mwb22.03-E", "Workbook"};
    yield return new object[] {"T-35-E", "Tract - Dead"};
    yield return new object[] {"g21.1-CA", "Awake!"};
    yield return new object[] {"wp21.3-H", "Watchtower"};
    yield return new object[] {"wp21.3-HI", "Watchtower"};
    yield return new object[] {"w22.3-E", "Watchtower"};
  }

  public static IEnumerable<object[]> TestEditionData()
  {
    yield return new object[] {"rr-E", false};
    yield return new object[] {"mwb22.03-E", true};
    yield return new object[] {"T-35-E", false};
    yield return new object[] {"g21.1-CA", true};
    yield return new object[] {"wp21.3-H", true};
    yield return new object[] {"wp21.3-HI", true};
    yield return new object[] {"w22.3-E", true};
  }

  public static IEnumerable<object[]> TestEditionValueData()
  {
    yield return new object[] {"mwb22.03-E", 22, 3};
    yield return new object[] {"g21.1-CA", 21, 1};
    yield return new object[] {"wp21.3-H", 21, 3};
    yield return new object[] {"wp21.3-HI", 21, 3};
    yield return new object[] {"w22.3-E", 22, 3};
  }
}