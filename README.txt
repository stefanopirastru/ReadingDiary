
Reading Diary
Descrizione
Reading Diary è un'applicazione web sviluppata in ASP.NET Core che consente di gestire e monitorare i libri letti, quelli in corso di lettura e quelli che si desidera leggere in futuro.
L'applicazione permette di:

- Visualizzare l'elenco dei libri tramite interfaccia web.
- Aggiungere, modificare ed eliminare libri.
- Cercare rapidamente un libro per titolo o autore.
- Monitorare lo stato di lettura.
- Visualizzare statistiche di lettura.
- Gestire elenchi paginati di libri.
- Proteggere le API tramite API Key.


Tecnologie utilizzate
Backend

- ASP.NET Core 8 Web API
- Entity Framework Core
- MySQL
- Swagger/OpenAPI
Frontend

- ASP.NET Core MVC
- Bootstrap 5
- Razor Views
Testing

- xUnit v3
- Moq
Containerizzazione

- Docker
- Docker Compose


Architettura della soluzione
La soluzione è suddivisa in più progetti:
ReadingDiary.Api
Espone le API REST e gestisce la persistenza dei dati.
ReadingDiary.Web
Applicazione MVC che consuma le API e fornisce l'interfaccia utente.
ReadingDiary.Tests
Contiene i test automatici.


Scelte progettuali
Web API separate dal frontend
È stata adottata una separazione tra frontend MVC e backend API.
Questa scelta consente:

- una migliore manutenibilità;
- una più semplice evoluzione futura;
- la possibilità di sostituire il frontend senza modificare il backend;
- il riutilizzo delle API da applicazioni mobile o altri client.


Entity Framework Core
È stato utilizzato Entity Framework Core per semplificare l'accesso ai dati e la gestione delle migrazioni del database.
Questo approccio consente di ridurre il codice SQL manuale e mantenere una buona produttività.


API Key Authentication
Per soddisfare il requisito di sicurezza è stata implementata un'autenticazione tramite API Key utilizzando un middleware dedicato.
La soluzione è semplice ma efficace e adatta al contesto della prova.


Paginazione
È stata implementata una paginazione lato server tramite DTO dedicato (PagedResult<T>).
Questa scelta evita il caricamento completo dei dati e rende l'applicazione più scalabile.


Interfaccia utente
L'interfaccia è stata realizzata utilizzando Bootstrap 5.
Sono state privilegiate:

- semplicità;
- leggibilità;
- responsività;
- rapidità di sviluppo.
La visualizzazione dei libri è stata implementata tramite card anziché tabelle per migliorare l'esperienza utente.


Avvio del progetto
Avvio con Visual Studio

1. Aprire la solution.
2. Configurare MySQL.
3. Eseguire le migration.
4. Impostare come startup project: 
- ReadingDiary.Api
- ReadingDiary.Web
5. Avviare la soluzione.


Avvio tramite Docker
Costruzione ed esecuzione:
docker compose up --build
Oppure:
start-docker.bat
Arresto:
docker compose down
Oppure:
stop-docker.bat


Test
I test possono essere eseguiti tramite Visual Studio Test Explorer oppure tramite:
dotnet test
I test coprono:

- validazione dei modelli;
- calcolo della paginazione;
- middleware per API Key.


Compromessi adottati
Per motivi di tempo sono state privilegiate:

- semplicità dell'architettura;
- rapidità di implementazione;
- chiarezza del codice.
Non sono stati introdotti pattern più complessi come:

- Repository Pattern;
- Service Layer completo;
- CQRS;
- AutoMapper.
Per le dimensioni del progetto tali soluzioni sarebbero state sovradimensionate.


Miglioramenti futuri
Con più tempo a disposizione implementerei:
Autenticazione avanzata

- Login tramite account Microsoft.
- Login tramite account Google.
- Supporto Multi Factor Authentication (MFA).
- Gestione utenti e profili personali.
Gamification
Una possibile evoluzione sarebbe l'introduzione di elementi tipici dei videogiochi:

- Trofei per numero di libri letti.
- Achievement per serie di letture completate.
- Badge per generi diversi.
- Livelli esperienza basati sui progressi di lettura.
- Statistiche avanzate e confronti storici.
Funzionalità aggiuntive

- Upload copertine libri.
- Dashboard statistiche avanzate.
- Ordinamento multi-colonna.
- Filtri avanzati.
- Notifiche per obiettivi di lettura.
- Esportazione dati.


Utilizzo di strumenti AI
Durante lo sviluppo sono stati utilizzati strumenti di Intelligenza Artificiale (Microsoft Copilot) come supporto alla progettazione, alla generazione di codice di base, alla revisione del codice e alla documentazione.
Tutte le soluzioni proposte sono state analizzate, adattate, integrate e validate manualmente con Postman prima dell'utilizzo nel progetto finale.
