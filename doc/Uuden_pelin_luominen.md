# Uuden pelin luominen

Jokainen minipeli toteutetaan omana Scene(i)nään, ja niissä on joitain yhtäläisyyksiä. Luomisen helpottamiseksi on joitain työkaluja.

## Foundation scene
Aloita luominen valitsemalla Foundation-scene (`Scenes/Development/Foundation`) ja tee siitä kopio painamalla ctrl/cmd + D. Nimeä luotu kopio haluamallasi tavalla ja siirrä se hakemistoon `Scenes/Minigames`.

Luodussa skenessä on valmiina ruudun koon mukaan skaalautuva UI-Canvas, jolle on sijoitettu aikapalkki ja pelistä toiseen näytettävät tiedot. Skenen sort order on laitettu suureksi ( >1, korota tarvittavasti niin että on suurin), jotta se piirretään aina Canvaksista viimeisenä; esimerkiksi jäljellä olevia elämiä kuvastavat sydämet eivät voi näin peittyä.

Erikseen skenestä löytyy Canvas `CanvasGameWorld`. Tämä Canvas on edellisestä erillinen, koska esimerkiksi niiden skaalaustarpeet voivat olla erilaiset (toinen esimerkiksi skaalautuu niin että asiat levenevät ruudun levetessä, toinen niin että paljastuu vain enemmän pelimaailmaa mutta asiat pysyvät saman levyisinä). Myös tämä Canvas skaalautuu automaattisesti ruudun koon mukaan, joten asiasta ei pitäisi koitua huolta ellei mieti skaalauksen hienosäätöä. Toisin kuin UI-canvaksessa, tähän on liitetty Graphic Raycaster -komponentti, joten Canvasten elementtien kanssa pystyy vuorovaikuttamaan.

Scenellä on oma kamera, joka skenen latauksen yhteydessä asetetaan varmuudella pääkameraksi (mitä `Main.camera` palauttaa). Unloadatessa sceneä alkuperäinen pääkamera asetetaan takaisin pääkameraksi.

Lisäksi skenellä on Logic-objekti, joka on tyhjä Game Object. Idea on luoda oma C#-skripti joka perii luokan `MinigameLogic`, ja asettaa se tämän peliolion komponentiksi. Perivä skripti liitetään automaattisesti aikapalkkiin siten, että ajan loppuessa kutsutaan virtuaalista metodia `OnTimerEnd()`.


## Minipelilogiikkaluokan luominen

Luo uusi C#-skripti, joka perii luokan `MinigameLogic`. Koska MinigameLogic perii MonoBehaviourin, perii sen myös luotu luokka. Voit siis vapaasti käyttää callback-metodeja joita pelimoottori kutsuu (OnEnable, Update, Start...). 


### Oletusmetodien overridaminen

MinigameLogic-luokalla on vain kaksi metodia, joita ei ole ajateltu overridattaviksi: `IMinigameEnderin` vaatimat `LoseMinigame`ja `WinMinigame`. Nämä kutsuvat molemmat metodia `EndMinigame` sopivalla parametrilla.

Kaikki muut metodit ovat määritelty joko avainsanalla _virtual_ tai _abstract_. Abstraktit metodit on pakko overridata ja konkreettisesti toteuttaa perivissä luokissa - niille ei välttämättä ole kaikille minipeleille yhteistä toimintaa, mutta ne ovat sellaisia että kaikki minipelit vaativat toteutuksen. Virtuaaliset metodit taas sisältävät konkreettisen määrittelyn myös yläluokassa MinigameLogic, mutta ne on mahdollista overridata. Toteutus yläluokassa sisältää vain hyvin jaetut toiminnot, kuten virtuaalimetodin Start viitteen hakeminen DataControlleriin. Käsittääkseni C# sallii vain abstract/virtual-metodien overridaamisen. Override tapahtuu seuraavasti, esimerkkinä Start:

`public override void Start(){...}`

__Huom: overridaava metodi voidaan määritellä async vaikka yläluokan metodi ei ole!__


### Tynkäesimerkki logiikkaskriptistä



## Pelin olioiden sijoittelu

### Canvakselle - peliin liittyvät game objektiin samanlaisia kuin puhtaasti GUI-elementit?
Varmaan helpompi tapaus, jota useimmat minipelit tuntuvat käyttävän. Suuri plussa on se, että skaalausasioista tai peliolioiden sijoittumisesta suhteessa kameraan ei tarvitse välittää juuri yhtään. Toimii niin että asetat pelioliot haluamasi Canvaksen alle. Jos lisäät esimerkiksi Buttonin jonkin olion komponentiksi, on sen kanssa nyt mahdollista vuorovaikuttaa. Tämä on siis perustapaus uuden pelin luomisessa.

### Game objektien sijoittaminen Unityn 3D-pelimaailmaan? (samaan tasoon / samansuuntaisesti suhteessa kameraan 2D-efektin luomiseksi)

Toisaalta useimmissa peleissä pelioliot sijaitsevat Unityn pelimaailmassa, eivät Canvaksella. Tällöin saattaa olla, että joutuu jonkin verran näkemään vaikvaa esimerkiksi vuorovaikutteisuuden tai sijoittelun suhteen, mutta ratkaisu on luultavasti enemmän 'Unity-henkinen' kuin toinen vaihtoehto. Esimerkkejä vaikkapa kosketuksen paikantamisesta löytyy aiemmista skripteistä, joissa se on tehty RayCastaamisen avulla. 