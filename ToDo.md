<h1>AUDIO</h1>

1. Muzica (aprox. 3-10 minute) de atmosfera generala pentru MainScene (doar assetul)
2. Sunet pentru Pickups (asset + setup)
3. Muzica (aprox. 3-10 minute) de atmosfera pentru MainMenu (doar assetul)
4. Sunet pentru jumpscare (asset + setup)
5. <s>Sunet pentru lightswitch (asset + setup)</s>
6. <s>Sunet pentru "prof" (asset + setup)</s>

  
<h1>SCRIPTS</h1>

1. <s>Un script pentru jumpscares (cu OnTriggerEnter) + plasarea de trigger points pe harta</s>
2. <s>Un script pentru activea unei lumini pe harta cand te afli in apropierea ei si apesi "E" (tot cu OnTriggerEnter / OnTriggerExit)</s>
3. <s>Clasa de GlobalValues (singleton) care NU mosteneste Monobehaviour si poate fi folosita pentru a pasa valori de la o scena la alta (acest script nu trebuie atasat de un GameObject)</s>
4. <s>Prof</s>
    1. <s>Adaugarea unui "prof" (momentan un cub) care merge dintr-un punct (Vector3) in altul (lista de puncte fiind stabilita apriori), fara sa interactioneze cu mediul inconjurator</s>
    2. <s>Punctele reprezinta un "search-area", iar "prof"-ul se plimba de la un punct la altul (pe niste path-uri predefinite - deci lista initiala este impartita in niste subliste de puncte pe care "prof"-ul le va urmari) cautandu-l pe jucator. Daca il gaseste, isi lasa path-ul curent si incearca sa il prinda pe jucator. Daca acesta reuseste sa scape (adica sa iasa din aria "prof"-ului), atunci proful se intoarce la cautat (fie la cel mai aproapiat path pe care il poate urmari, fie la path-uri original, asta e mai mult alegerea celui care implementeaza).</s>
    3. <s>[ALTERNATIVA] "prof"-ul se poate spawna pentru cateva secunde intr-o locatie random dintr-o lista predefinita de locatii, iar daca nu-l gaseste pe player, se respawneaza in alta locatie.</s>
5. Jucatorul poate avea "sprint" pe o perioada scurta pentru a putea fugi de "prof"

  
<h1>GAME</h1>

1. <s>Un nou layout definitiv, destul de mare incat sa poti colecta 10 obiecte in 5-10 minute</s>
2. De pus un "cap" (valoare maxima) pe viteza pe care o poate prinde bila
3. <s>Lightswitches</s>
    1. <s>Adaugarea de light switches (momentan, fara design) - ele ar trebui sa fie singurele lumini vizibile pe minimap (nici macar playerul nu ar trebui sa fie vizibil ca sa nu stie unde se afla daca nu are nicio lumina activata)</s>
    2. <s>Configurarea light switch-urilor cu minimap-ul</s>
4. Configurarea volumului in MainScene bazandu-ne pe valoarea din Options din MainMenu (also trebuie setata o valoare default, let's say 50/100)
5. Jucatorul sa poata fugi (adica sa sprinteze)

  
<h1>GUI</h1>

1. Text pentru "nr. obiecte colectate / nr. obiecte per total"
2. Meniu in game
    1. Optiunea de a apasa "Esc" iar jocul sa se opreasca si sa dea display la un meniu simplu
    2. Meniul efectiv cu butoane "Continue" si "Exit" (momentan)
3. <s>Implementarea unui jumpscare (imagine care acopera tot ecranul si isi da fade in aprox. 3 secunde)</s>
4. <s>Sprint bar pentru player</s>

  
<h1>DESIGN</h1>

1. <s>Design pentru foitele de licenta (Pickups) - de preferat Blender</s>
2. Design pentru un "om" care poate merge - un simplu cub care e animat (deci nu miscat prin forte) ar fi de ajuns (asta e mai traseu, dar daca o facem, iese super smecher.)
3. <s>Design pentru light switch</s>
4. <s>Animatie pentru a trece de la o scena la alta (un loading screen de exemplu)</s>