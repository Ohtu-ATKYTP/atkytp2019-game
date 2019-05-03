## Debug mode


### Play in debug mode
Pelissä on debug mode josta pääsee pelaamaan valitsemaansa peliä valitsemallaan vaikeusasteella. Debug modeen pääsee main menusta
painamalla "Play in debug mode" -nappia. Nappi on vakiona disabloitu, ettei debug mode näy pelaajille, mutta sen saa 
aktivoitua unitystä kohdasta "DebugPlayButton."

### Settings
Pelissä on settings ruutu, josta näkee ja pääsee muokkaamaan pelaajaan liittyviä tietoja. Settings ruutuun pääsee main menun kautta
painamalla mutteri-ikonia. Myös tämä nappi on vakiona disabloitu, mutta sen saa aktivoitua unitystä kohdasta kohdasta 
"Settings button."

### Kehittäjäasetusten aktivoiminen koodista:
Jos ei halua asentaa unitya, mutta käyttää kuitenkin devausasetuksia, voi ne asettaa suoraan koodista päälle tiedostosta /Assets/Scenes/MainMenu/MainMenu.unity.

DebugPlayButtonin saa päälle etsimällä kohta "m_Name: DebugPlayButton". Joitain rivejä alempana on kohta "m_IsActive: 0", joka asetetaan arvoon 1.

Vastaavasti SettingsButtonin saa päälle etsimällä kohta "m_Name: SettingsButton" ja aktivoimalla alempana oleva kohta "m_IsActive: 0"
