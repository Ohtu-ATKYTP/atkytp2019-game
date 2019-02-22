# Testaus
Testit löytyvät kansiosta Assets/Tests <br/>
## Test Runner
`Unity Test Runner`in saa auki `Window -> General -> Test Runner`

## Uusi testi
Lisää uusi testi `hiiren oikea -> create -> Testing -> C# Test Script` (varmista että olet oikeassa kansiossa riippuuen siitä teetkö `edit mode` vai `play mode` testiä)

Unity luo koodin: (jos luot tiedoston itse vaikka komentoriviltä tästä on varmaan hyvä aloittaa)
```C#
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FirstTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FirstTestSimplePasses()
        {

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FirstTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
```
`[Test]` määreen testit suoritetaan normaaleina testeinä kuten missä vaan muussakin rajapinnassa <br/>
`[UnityTest]` määreen testit suoritetaan Unityn moottorin avulla <br/>

>A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use <br/>
>`yield return null;` to skip a frame.




## Edit mode testit
Tehdään kansioon `Assets/Tests/PlayModeTests`

## Play mode testit
Tehdään `Assets/Tests/PlayModeTests` kansioon. HUOM! .dll tiedostossa on oletuksena 

# NUnit
[Dokumentaatio](https://github.com/nunit/docs/wiki) <br/>

Unit testit kirjoitetaan NUnit kirjaston avulla. Kun luot testisciprtin se importataan automaattisesti. NUnit vaikuttaa toimivan samaan tamaan kuin JUnit, mutta syntaksi eroaa hieman. Ainakin seuraavat komennot ovat olemassa ja hyödyllisiä:
```C#
Assert.AreEqual(Object A, Object B);
Assert.IsTrue(bool x);
Assert.IsFalse(bool x);
```
Jos haluat määritellä arvoja muuttujille ennen jokaista testiä käytä `SetUp`:
```C#
int x;
[SetUp]
public void SetUp() {
    x = 5;
}
```
Jos haluat tehdä jonkin clean upin joka testin jälkeen:
```C#
[TearDown]
public void TearDown() {
    //TearDown runs after every test
}
```
Jos haluat tehdä jotain ennen kuin yhtäkään testiä suoritetaan ja vain kerran:
```C#
[OneTimeSetUp]
public void Init() {
    //Init runs once before running test cases.
}
```
Ja jos haluat tehdä clean upin kun kaikki testit on suoritetu:
```C#
[OneTimeTearDown]
public void CleanUp() {
    //CleanUp runs once after all test cases are finished.
}
```