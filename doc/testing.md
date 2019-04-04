# Testaus
Testit löytyvät kansiosta Assets/Tests <br/>

### Hyödyllisiä linkkejä

https://blogs.unity3d.com/2014/07/28/unit-testing-at-the-speed-of-light-with-unity-test-tools/

https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/

https://nsubstitute.github.io/ (mocking kirjasto)

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
Tehdään kansioon `Assets/Tests/PlayModeTests`. Edit mode testit ovat nopeita suorittaa. Suosi näitä aina kuin mahdollista.

## Play mode testit
Tehdään `Assets/Tests/PlayModeTests` kansioon. Play mode testeissä voidaan simuloida pelimoottorin etenemistä. Nämä testit on hidasta suorittaa.

## Komponenttien (MonoBehaviourin perivät) käyttäminen testeissä

Otetaan esimerkiksi Camera, joka halutaan luoda testin alussa, antaa parametriksi yksikkötestattavalle metodille, ja lopussa tarkistaa onko sen tila muuttunut halutusti. __Emme__ voi luoda komponenttia seuraavasti: ~~`Camera cam = new Camera();`~~. Komponentti on liitettävä GameObjektiin, jolloin Unity works its magic ja sitä pystyy käyttämään kuten olettaisi. Prosessin nopeuttamista varten on staattinen luokka `ComponentCreator`, jolla on staattinen metodi `Create<T>()`. Kameratestissä voisi siis luoda kameran komennolla `Camera cam = ComponentCreator.Create<Camera>();`. Tämän jälkeen esimerkiksi asettaminen `Camera.enabled = false;` ei aiheuta NullPointerExceptioniä.

ComponentCreatorin toiminta: 

 Parametriton versio luo uuden GameObjektin, kutsuu tällä `AddComponent<T>` ja palauttaa sitten game objektilta kysymänsä viitteen ko. komponenttiin. Metodin parametrillinen versio `Create<T>(GameObject go)` taas lisää komponentin parametri-game objektiin. Parametrittoman metodikutsun luoma game object ei jää tavoittamattomiin, sillä siihen saa viitteen luodun komponentin `gameObject` kentän kautta. Luokan määrittely löytyy tiedostosta `Utilities/ComponentCreator.cs`.

# NUnit
[Dokumentaatio](https://github.com/nunit/docs/wiki) <br/>

Unit testit kirjoitetaan NUnit kirjaston avulla. Kun luot testiscriptin se importataan automaattisesti. NUnit vaikuttaa toimivan samaan tamaan kuin JUnit, mutta syntaksi eroaa hieman. Ainakin seuraavat komennot ovat olemassa ja hyödyllisiä:  
`EDIT HUOM! Minulle selveisi että seuraavat ovat vanha tapa tehdä assertioneita! Ne kuitenkin toimivat yhä.`
```C#
Assert.AreEqual(Object A, Object B);
Assert.IsTrue(bool x);
Assert.IsFalse(bool x);
```
`Käytä mieluummin Assert.That() metodia! Lue lisää dokumentaatiosta!`
```C#
int[] array = new int[] { 1, 2, 3 };
Assert.That(array, Has.Exactly(1).EqualTo(3));
Assert.That(array, Has.Exactly(2).GreaterThan(1));
Assert.That(array, Has.Exactly(3).LessThan(100));
```
---
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
---
# Test Doubles: Dummy, stub, spy, mock ja fake
Termistöä ([unity blog postauksen mukaan](https://blogs.unity3d.com/2014/07/28/unit-testing-at-the-speed-of-light-with-unity-test-tools/))

 Termi      | Selitys        
 ---------- | :-------        
 Dummy      | palauttaa kaikesta null
 Stub       | palauttaa staattista testidataa
 Spy        | pitää kirjaa esim. funktiokutsuista
 Mock       | spy joka tarkistaa että oikeat funktiot on kutsuttu
 Fake       | sisältää logiikka ja emuloi oikeaa komponenttia


Kannattanee vilkaista:  
https://blogs.unity3d.com/2014/07/28/unit-testing-at-the-speed-of-light-with-unity-test-tools/ (sama kuin ylempänä)  
https://nsubstitute.github.io  
https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/ (sama kuin ylempänä, **lue** jos teet yksikkötestejä)

 ## MonoBehaviourien yksikkötestaamisen vaikeus (oleellista ennen kuin yrität käyttää NSubstitutea!)

 NSubstitute ei mahdollista ei-abstraktien luokkien korvaamista (substitointia). Jos testattava luokka perii MonnoBehaviourin, ei sitä voi mockata (tietääkseni). Kannattavinta on refaktoroida luokka niin, että sen kytkökset pelimoottoriin ja varsinainen logiikka ovat mahdollisimman erillään toisistaan.


 ### Humble object

Kuvaus patternista löytyy [tästä](https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/). Olkoon testattava luokka nimeltään `Example`. Aluksi luokassa logiikka ja pelimoottorikutsut ovat sekaisin. Luodaan rajapinta, jolla on metodit joilla se antaa kutsujalle komponentteja ja tuottaa vaikutuksia pelimoottorissa; annetaan rajapinnalle nimeksi esimerkiksi `ExampleController`   (vaikka meidän projektissa Controllereita on ehkä jo liikaa ;P). Määrittely voisi olla jotain tämäntapaista: 
``` C#
public interface IExampleController {
    public Camera FetchAllCameras();
}
```

Jonkin verran ehkä joutuu refaktoroimaan luokkaa, jotta nämä pelimoottorin kanssa kommunikoivat osiot ovat omissa metodeissaan (jotka rajapinta määrittelee).

Logiikka eriytetään omaksi luokakseen, jolla on asetettava viite ExampleControlleriin. Esimerkiksi: 

``` C# 

public class ExampleLogic {
    private IExampleController controller; 
    private Camera[] cameras; 
    
    public ExampleLogic(IExampleController controller){
        this.controller = controller;
        cameras = controller.FetchAllCameras();
    }


    public void DoSomething(){
        if(Condition()){
            cameras[0].scale = new Vector3(2, 2, 2);
        }
    }

    .
    .
    .

}
```

Alkuperäinen luokka on muotoa

``` C# 
public class Example : MonoBehaviour, IExampleController {
    private ExampleLogic logic;

    // kaikki callbackit joita pelimoottori kutsuun oltava täällä, koska perii MonoBehaviourin
    private void Start(){
        // tämä luokkakin toteuttaa rajapinnan
        logic = new ExampleLogic(this);
    }

    private void Update(){
        logic.ProcessUpdate();
    }

    public Camera FetchAllCameras(){
        return FindObjectsByType<Camera>();
    }

}
```

Esimerkki ei ole täysin loppuun mietitty, kannattaa lukea linkin versio jotta näkee täyteläisemmän esimerkin jolle myös kirjoitettu testejä.


 ## NSubstitute

 Nämä test doublet (testikaksonen? :D) voi toki kirjoittaa itse omaksi luokakseen mutta voi olla mukavampaa käyttää mockaukseen suunniteltua testauskirjastoa (olette varmaan käyttäneet javavassa mockitoa). NSubstitute on unity test toolseissa mukana joten on loogista käyttää sitä.

 ### Dummyn luominen NSubstitutella


 Dummyn luominen on yksinkertaista:
 ```C#
 var dummyObject = Substitute.For<Class>(parameter1, parameter2); //parametrit siis luotavan luokan mahdolliset parametrit
 ```

 ### Stub
 Dummyn saa palauttamaan arvoja .Returns() avulla:
 ```C#
 var stub = Substitute.For<Class>();
 stub.SomeMethod().Returns("Hello World");
 stub.SomeVariable.Returns("this is a a string");
 //SomeMethod ja SomeVariable siis luokan omia metodeita/muuttujia
 ```
Voit lisätä useita palautuksia (jotka palautetaan yksi kerrallaan per kutsu):
```C#
stub.SomeMethod().Returns("Ensimmäinen", "Toinen");
```
Voit määritellä palaukset myös anonyymifunktioina:
```C#
stub.SomeMethod().Returns(x => "Ensimmäinen", x => "Toinen", x => { throw new Exception(); });
```
Katso myös [.Configure()](https://nsubstitute.github.io/help/configure/)

### Spy 
Blogipostaus sanoo spyn luomisesta NSubstitutella näin:

>It is possible to assemble a test spy using NSubstitute’s Arg.Do<> and When… Do… constructs.
>
>```C#
>int hitCount = 0;
>var opponent = Substitute.For<ISpaceship>();
>opponent.AcceptIncomingShots( Arg.Do<IEnumerable<Shot>>(x => hitCount+=x.Count()) );
>```
>NSubstitute uses special argument matcher Arg.Do to execute a delegate when argument to it passes to the substitute. Which means that hitCount+=x.Count() would be executed on each method call.

Esimerkissä `ISpaceship` on rajapinta avaruusalukselle ja `Shot` sisältää datan yhdestä ampumisesta (ilmeisesti 3D vektorin).

### Mock
Käytän tähän aiemmin linkkaamani blogipostauksen esimerkkiä. Ajatellaan että meillä on luokat `SpaceShip` ja `Weapon`. `SpaceShip` voi aseistaa antamalla sen `SpaceShip.Equip()` metodille viitteen olemassaolevaan aseeseen. Haluamme testata että `Ship` toimii oikein siten että kun kutsumme sen `SpaceShip.Shoo()` metodia, kutsuu se kaikkien aseidensa `Weapon.Shoot()` metodia.

```C#
[Test]
public void EachWeaponShoots_WhenSpaceShiptShootIsCalled()
{
    // Arrange
    var weapon1 = Substitute.for<IWeapon>(); //IWeapon on ilmeisesti aseen rajapinta
    var weapon2 = Substitute.for<IWeapon>();

    var ship = new SpaceShip(2, 0);
    ship.Equip(weapon1);
    ship.Equip(weapon2);

    //Act
    ship.Shoot();

    //Assert
    weapon1.Received(1).Shoot();
    weapon2.Received(1).Shoot();
}
```

### Fake

Fakea ei voi luoda substitutena ja niitä ei kannata käyttää jos ei ole pakko.

