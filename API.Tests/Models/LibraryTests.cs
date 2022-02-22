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

  public static IEnumerable<LanguageCode> LanguageData()
  {
    yield return new LanguageCode() { Code = "E", Language = "English" };
    yield return new LanguageCode() { Code = "S", Language = "Spanish" };
    yield return new LanguageCode() { Code = "CA", Language = "Shona" };
    yield return new LanguageCode() { Code = "H", Language = "Hungarian" };
    yield return new LanguageCode() { Code = "HI", Language = "Hindi" };
  }

  public static IEnumerable<object[]> TestLanguageData()
  {
    yield return new object[] {"rr-E", "English"};
    yield return new object[] {"mwb22.03-E", "English"};
    yield return new object[] {"T-37-E", "English"};
    yield return new object[] {"g21.1-CA", "Shona"};
    yield return new object[] {"wp21.3-H", "Hungarian"};
    yield return new object[] {"wp21.3-HI", "Hindi"};
  }
}