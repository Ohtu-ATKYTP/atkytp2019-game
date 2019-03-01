# Mietintää arkkitehtuurista ja muusta

Tähän temp juttuja arkkitehtuurista ja muusta, jos ei halua että unohtuu.

## Objektit ja niiden komponentit ja sciptit
  - Jos tehdään objekti jotain scriptiä varten, niin objektin ja sciptin voisi nimetä täysin samalla nimellä, jos se onnistuu
  - Buttoneissa sama nimi kuin niiden scripteissä
  - Jos tekstikenttään tulee sciptillä tekstiä, niin scripti suoraan kiinni tekstiin ja sama nimi

## Nimeämiskäytäntöjä
  - ScenemanagerScenessä ladataan jotain scriptejä joihin saa referenssin yli kaikkien scenejen. Nämä voisi nimetä **[jotain]Manager.cs**
  
  - Buttoneissa kiinni olevat scriptit voisi nimetä **[jotain]Button.cs**
  
  - Tekstikentissä kiinni olevat scriptit **[jotain]Text.cs**
  
  - Webservicen kanssa yleensä tarvii tehdä viittauksien toimimiseksi eka normifunktio ja sitten saman function Coroutine versio.
  Näille voi antaa saman nimen ja coroutiinille loppuun COR tai Coroutine.

# Arkkitehtuurista

## Scriptien tiedostohakemiston järjestys
  - Näitä täytyy järjestää, miten?

## Managerit ja WebServiceScipt
  - Managereilla ei oikein selvää keskinäistä hierarkiaa. Kaikki kutsuvat yhtä webservicesciptiä josta uhkaa tulla melko laaja
