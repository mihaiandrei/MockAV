MockAV este un serviciu destinat dispozitivelor de tip endpoint și va expune un API menit să controleze funcționalitățile sale de bază.


# Requirements

## Connect
Nu se permite conectarea concurentă a mai multor clienți la produs via API-uri.

## Real  Time Scan
Produsul este capabil să scaneze real time.
În API vom avea două metode prin care scanarea real time poate fi controlată: Activează/Dezactivează scanare.

Cât timp este activă scanarea produsul poate găsi infecții pe disc.
Când produsul găsește o infecție se va genera un eveniment.
Cât timp scanarea este inactivă produsul nu generează astfel de evenimente.

## Scan on demand
Produsul este capabil să scaneze on demand.
Pentru aceasta suportăm două metode, Start și Stop, una care pornește o scanare on demand și a doua care poate opri, forțat, o scanare.

### Start
Metoda Start poate fi apelată de oricâte ori dar nu va porni niciodată mai mult de o scanare.
Dacă este apelată în timp ce o scanare este în desfășurare apelul va returna un cod de eroare corespunzător care indică starea de scanare în desfășurare. 
Dacă reușește să pornească o scanare va returna cod de eroare de succes.
Orice apel de Start efectuat când o altă scanare nu este în curs de desfășurare se va încheia cu succes. 
În caz de succes, o scanare a fost pornită, produsul va genera un eveniment al cărui conținut va fi data și ora la care a pornit scanarea.

### Stop
Metoda Stop se va executa cu success în orice context.

Când apelul metode Stop are loc în timp ce o scanare on-demand este în desfășurare aceasta va fi oprită și produsul va raporta un eveniment ce va semnala oprirea scanării.
Evenimentul va conține data și ora evenimentului și motivul pentru care s-a încheiat scanarea.
În acest caz motivul fiind interacțiune din exterior, scanarea s-a încheiat prematur/forțat.

### Scan
În cazul în care scanarea se termină normal, a terminat de scanat tot ce era de scanat, se va genera un eveniment ce va raporta data și ora evenimentului și motivul pt care s-a încheiat scanarea.
În acest caz motivul este scanare încheiată cu success.

Implementarea mock a scanării on demand poate fi descrisă astfel:
- Nici o scanare nu va continua infinit. Fiecare scanare va rula un timp maxim de secunde, un random între 10 și 30.
- Unele scanări pot raporta multiple infecții altele nici una.

### Threat found
În cazul în care sunt găsite obiecte infectate, la finalul scanării se va genera un eveniment ce va conține lista de obiecte găsite infectate.
Un obiect infectat are următoarele două atribute: cale fișier și nume threat.

## Client not connected
Consum a evenimentelor generate de produs în perioada în care nici un client nu era conectat/înregistrat.

Practic se va implementa un mecanism de persistență și atât timp cât niciun client nu este conectat/înregistrat la produs prin intermediul API-ului orice eveniment generat de produs va fi salvat pentru a fi consumat mai târziu.

În API-uri vom avea nevoie de o metodă prin care un client va cere să primească evenimentele salvate, dacă există.

# Non functional requirements
- Evenimentele de detecție vor fi expuse via API-uri iar un integrator va putea consuma aceste evenimente.
- O bibliotecă ce deservește o integrare locală cu serviciul descris mai sus.
- Ne așteptăm la implementări de clienți care folosesc API-urile, cel puțin două implementări, un client în C# și unul web.
- Autentificare ???

# Deliverables
- Un mock de serviciu/produs AV code name MockAV
- SDK ce deservește o integrare locală cu serviciul descris mai sus.
- Resurse ce deservesc o integrare remote cu serviciul mai sus menționat.
- Un client în C#
- și unul web