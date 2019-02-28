# Uuden pelin luominen

Koska peleillä on monia samankaltaisuuksia, on olemassa valmis skene jossa näitä on yritetty asettaa valmiiksi. Skenen nimi on Foundation. Jos luot uuden pelin, valitse kyseinen skene ja paina Ctrl/command + D. 
Pitäisi ilmestyä duplikaattiskenen, jonka nimen voi vaihtaa haluamansa minipelin nimeksi. UI:ssa on valmiina aikapalkki, elämät ja pisteet, ja sen pitäisi skaalautua kaikille näytöille fiksun näköisesti. Skenessä on game object 'Logic', 
joka asettaa tarvittavat kytkökset ja sisältää metodit minipeliin voittamiseen / häviämiseen. Varsinainen pelin kulkuun liittyvä logiikka jää pelin toteuttajan vastuulle.