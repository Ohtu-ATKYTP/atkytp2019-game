## ElevatorGame eli Hissin pudotus

Pelissä on tarkoituus klikkailla pelaajat tarpeeksi ylös, jolloin ilmestyy forcedown -nappi. Nappia painamalla pelaajat mäsäyttävät lattiaan ja pelin voittaa kun hissi menee rikki.

### Vaikeusaste:

Vaikeusastetta säädetään JumpmanLogic -luokasta komennolla adjustDifficulty(). Perusideana on että painovoima nousee, jolloin pelaajat tulevat alas nopeammin. Hyppyvoimaa myös lisätään tasapainottamaan lisääntynyttä painovoimaa. Painovoima ja hyppyvoima nousevat lineaarisesti vaikeusasteen mukana.

### Luokista

- *AddRigidBody* laittaa RigidBody komponentin siihen objektiin jossa se on kiinni. RigidBodyn avulla objektiin vaikuttaa fysiikka. Hissi tippuu voitossa, koska sen osille annetaan rigidbodyt.

- *Collisionlogic* tarkastelee mitä tapahtuu kun pelaajat osuvat lattiaan. Esimerkiksi tärähdysmekaniikka aktivoidaan. Collisionlogic laskee ennen kolmeen, ennenkuin tärähdystä käytetään. Tämä johtuu siitä, että alussa pelaajat kolahtavat lattiaan itsestään ilman hyppyä. Jos alas on tultu forcedownnappia painamalla, niin lisätään damagea (joka tässä tapauksessa voittaa pelin).

- *DamageVisuals* laittaa objektilleen uuden tuhoa näyttävän kuvan.

- *EffectsController* näyttää lopussa tähtiä kun peli voitetaan.

- *ElevatorGameLogic* luokassa on peliin liittyviä metodeja, kuten voitto- ja häviösekvenssit.

- *ElevatorShaftMove* liikuttaa taustalla näkyvää hissikuilua alaspäin

- *JumperPositions* pitää kirjaa niistä hyppääjistä, jotka ovat tarpeeksi ylhäällä. Jos kaikki kolme ovat tarpeeksi ylhäällä asetetaan forcedown nappi aktiiviseksi.

- *Jumpmanlogic* hallitsee hyppääjien käyttäytymistä, kuten liikettä. Myös vaikeusaste laitetaan käytännössä täällä.

- *ShakeBehavior* tärisyttää ruutua
