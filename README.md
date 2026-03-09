# TicketManager

Ein einfaches Ticket-Management-System in C# zur Verwaltung von Tickets, Kunden und Benutzern.  
Das Projekt wurde als Lern- und Übungsprojekt entwickelt, um wichtige Grundlagen der Anwendungsentwicklung praktisch umzusetzen.

## Funktionen

- Benutzer anlegen
- Benutzer anmelden
- Passwortprüfung über eine eigene Klassenbibliothek
- Sichere Passwortspeicherung mit Hash und Salt
- Tickets verwalten
- Kunden verwalten
- Strukturierte Trennung in Models, Services und Security

## Projektstruktur

```text
TicketManager
├── TicketManager
├── PasswordValidator
└── TicketManager.sln
```

## Projekte

### TicketManager

Das Hauptprojekt mit der eigentlichen Anwendung.  
Hier befinden sich die Models, Services, Sicherheitsklassen und die Programmlogik.

### PasswordValidator

Eine eigene Klassenbibliothek für die Passwortvalidierung.  
Dadurch wurde die Passwortprüfung sauber vom Hauptprojekt getrennt und übersichtlich ausgelagert.

## Verwendete Technologien

- C#
- .NET
- Visual Studio
- Git
- GitHub
- GitHub Desktop

## Aufbau des Hauptprojekts

### Models

Enthält die Datenklassen, zum Beispiel:

- User
- Customer
- Ticket
- TicketList
- Address

### Services

Enthält die Geschäftslogik, zum Beispiel:

- Benutzerverwaltung
- Kundeverwaltung
- Ticketverwaltung
- Datenspeicherung

### Security

Enthält sicherheitsrelevante Funktionen, insbesondere:

- PasswordHashing

## Passwortvalidierung

Die Passwortprüfung wurde in eine eigene Klassenbibliothek ausgelagert (`PasswordValidator`).  
Dadurch ist die Validierungslogik getrennt vom Hauptprojekt und kann später auch in anderen Projekten wiederverwendet werden.

Geprüft werden unter anderem:

- Mindestlänge
- Großbuchstaben
- Kleinbuchstaben
- Zahlen
- Sonderzeichen

Wenn ein Passwort ungültig ist, wird eine eigene Exception verwendet:

- `PasswordException`

Dadurch können mehrere Fehler gesammelt und verständlich ausgegeben werden.

## Passwortsicherheit

Für die Passwortsicherheit wird nicht das Klartext-Passwort gespeichert, sondern ein Hashwert.

Dabei wurde folgendes umgesetzt:

- Das Passwort wird gehasht
- Zusätzlich wird ein individueller Salt erzeugt
- Hash und Salt werden gespeichert
- Beim Login wird das eingegebene Passwort erneut mit dem gespeicherten Salt verarbeitet und mit dem vorhandenen Hash verglichen

### Warum Salt wichtig ist

Ein Salt ist ein zusätzlicher zufälliger Wert, der vor dem Hashing mit dem Passwort kombiniert wird.

Das bringt wichtige Sicherheitsvorteile:

- Gleiche Passwörter haben nicht automatisch den gleichen Hash
- Vorberechnete Tabellenangriffe werden deutlich erschwert
- Die Passwortspeicherung wird sicherer als bei einfachem Hashing ohne Salt

### Einfach erklärt

Nicht das Passwort selbst wird gespeichert, sondern nur ein berechneter Wert daraus.  
Der Salt sorgt dafür, dass dieser Wert besser geschützt ist.

## Lerninhalte im Projekt

Mit diesem Projekt wurden unter anderem folgende Themen geübt:

- Klassen und Objekte
- Trennung von Zuständigkeiten
- Projektstruktur in Visual Studio
- Arbeiten mit mehreren Projekten in einer Solution
- Klassenbibliotheken
- Exceptions
- Passwortvalidierung
- Passwort-Hashing mit Salt
- Lokale Datenspeicherung
- Versionsverwaltung mit Git und GitHub

## Ziel des Projekts

Ziel des Projekts ist es, wichtige Grundlagen der Softwareentwicklung praktisch anzuwenden und ein strukturiertes C#-Projekt mit sinnvoller Aufteilung aufzubauen.

Besonderer Fokus lag auf:

- Sauberer Struktur
- Wiederverwendbarkeit von Code
- Trennung von Logik und Validierung
- Sicherer Passwortverarbeitung
- Praktischer Arbeit mit GitHub

## Projekt starten

1. Repository herunterladen oder klonen
2. `TicketManager.sln` in Visual Studio öffnen
3. Projektmappe erstellen
4. `TicketManager` als Startprojekt auswählen
5. Anwendung starten

## Hinweis

Dieses Projekt wurde als Lernprojekt erstellt und dient der praktischen Übung im Bereich Anwendungsentwicklung.
