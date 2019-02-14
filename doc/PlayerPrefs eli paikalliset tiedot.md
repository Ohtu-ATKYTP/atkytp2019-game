# PlayerPrefs

Pelaajan tiedot ja joitain apumuuttujia tallennetaan kännykkään 
[unityn playerprefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html) luokalla. Tärkeät metodit ovat:
 - PlayerPrefs.HasKey("avain"): tarkastaa onko jotain tietoa olemassa. Käytetään jossain if-logiikoissa hyödyksi.
 - PlayerPrefs.SetString("avain", arvo): asettaa halutulla avaimella uuden arvon. Joko muuttaa vanhaa tai tekee uuden
 - PlayerPrefs.GetInt("avain"): hakee avaimella arvon

Seuraavat pelaajan tiedot tallennetaan kännykkään, kun ne saadaan rekisteröidessä serveriltä palautuksena (WebServiceScript.SendUser):

```C#
PlayerPrefs.SetString("_id", h._id);
PlayerPrefs.SetString("username", h.user);
PlayerPrefs.SetString("token", h.token);
PlayerPrefs.SetInt("highScore", h.score);
```

Lisäksi Playerprefssillä tallennetaan muuttuja **"syncedHS"** (arvot 0 tai 1), joka kertoo voidaanko olettaa, että pelaajan HighScore on
päivitetty serverille. Aina kun pelaaja saa uuden highscoren, se tallennetaan paikallisesti ja asetetaan "syncedHS" nollaan. Metodi 
HighScoreManager.Sync() yrittää päivittää pelaajan Highscorea niin kauan kunnes se onnistuu. Tämän jälkeen syncedHS asetetaan taas
ykköseksi (eli highscore on "synkassa"). "SyncedHS" tarkastetaan myös aina kun appsi käynnistyy.


(EI VIELÄ MASTERISSA, JOTAIN ONGELMIA)
Toinen apumuuttuja on nimeltään **"registered"** (0 tai 1). Muuttuja kertoo onko pelaaja rekisteröitynyt serverille vai ei. Muuttujalla
päätetään ohjelmassa erilaisia asioita:
 - Näytetäänkö pelaajalle alussa rekisteröintiruutu vai ei.
 - Yritetäänkö paikallista highscorea päivittää (Jos ei rekisteröitynyt niin ei kannata)
 - Estää uudelleen rekisteröitymisen
