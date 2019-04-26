# PlayerPrefs

**HUOM:** Playerprefsien poisto tietokoneelta löytyy unityn sivuilta alla olevasta linkistä

Pelaajan tiedot ja joitain apumuuttujia tallennetaan kännykkään 
[unityn playerprefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html) luokalla. Tärkeät metodit ovat:
 - PlayerPrefs.HasKey("avain"): tarkastaa onko jotain tietoa olemassa. Käytetään jossain if-logiikoissa hyödyksi.
 - PlayerPrefs.SetString("avain", arvo): asettaa halutulla avaimella uuden arvon. Joko muuttaa vanhaa tai tekee uuden
 - PlayerPrefs.GetInt("avain"): hakee avaimella arvon

Seuraavat pelaajan tiedot tallennetaan kännykkään, kun ne saadaan rekisteröidessä serveriltä palautuksena:

```C#
 PlayerPrefs.SetString ("_id");
 PlayerPrefs.SetString ("username");
 PlayerPrefs.SetString ("token");
 PlayerPrefs.SetInt ("highScore");
 PlayerPrefs.SetInt ("rank");
 PlayerPrefs.SetInt ("registered");
```
