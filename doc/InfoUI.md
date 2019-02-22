# InfoUI prefab
InfoUI-prefab löytyy prefabs kansiosta ja se sisältää timer palkin, 3 elämää merkkaavaa sydäntä sekä pisteet ja scriptit niiden toimintaan.

## Kuinka käytän
Vedä InfoUI-prefab sceneesi canvakselle. Valitse prefabin sisältä TimeProgressBar. Sillä on komponentti Time Progress (script) ja lista Timer Ready Methods (). Lisää tähän se metodi jonka haluat suoritettavan silloin kuin aika on täysi (samalla lailla kuin esim. buttonin onClick). Samassa komponentissa on muuttuja seconds. Aseta seconds muuttujan arvo haluamaasi määrään, se määrittää kuinka monta sekuntia on aikaa.