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
Unityn manuaali sanoo seuraavaa: 

>Edit mode test scripts <br/>
>are defined by the file location you place them in. Valid locations:
>
>Project Editor folder
>
>Assembly Definition file that references test assemblies that are Editor-only
>
>Precompiled assemblies that are in the Project’s Editor folder

## Play mode testit
Tehdään `Assets/Tests` kansioon. Varmista, että kyseisestä kansiosta löytyvässä `Tests.dll` tiedostossa on valittu haluamasi platformit. Jotta play mode testit voidaan suorittaa täytyy valittuna olla (myös) jokin muu kuin Editor
