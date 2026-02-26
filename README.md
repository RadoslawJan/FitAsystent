# FitAsystent

---

##### FitAsystent to aplikacja dla ludzi którym zależy na byciu fit. Aplikacja pozwala zarówno zalogowanym jak i niezalogowanym użytkownikom sprawdzić swoje BMI, przedział BMI oraz zlecane spożycie kcal. Wyświetlana jest również porada AI.

##### Dodatkowe funkcje dla zalogowanych:
- Zapisywanie wyników
- Edycja wyników
- Usuwanie wyników
- Śledzenie progresu dzięki przejrzystemu diagramowi

---

## Dokumentacja projektu

### 1. Zastosowane technologie:
- Aplikacja zbudowana w języku C# (ASP .NET MVC)
- Wykorzystane Razor Views
- Implementacja zewnętrznego API (GoogleAI)
- Rysowanie wykresów przy użyciu Chart.js
- Lokalna baza danych
- Stylowanie przy użyciu Bootstrapa

### 2. Podział podstron
- Strona główna (kalkulator) - elementy:
    - Dane wejścowe:
        - Waga
        - Wzrost
        - Wiek
        - płeć
    - Dane wyjściowe:
        - BMI wraz z informacją o zakresie
        - BMR - kalorie spoczynkowe
        - Porada AI
- Logowanie
- Rejestracja
- Zarządzanie kontem
- Dashboard - elementy:
    - Obecna waga
    - BMI wraz z informacją o zakresie
    - BMR - kalorie spoczynkowe
    - Historia pomiarów
- Moje pomiary - lista rekordów - dla każdego elementu:
    - Data
    - Waga
    - BMI
    - Wynik (infomacja o zakresie wagowym)
    - BMR
    - **Edytuj** - pozwala edytować rekord
    - **Usuń** - umożliwia *trwale* usunąć element

### 3. Kluczowe rozwiązania:
 - Wzorzec projektowy MVC (Model view controller)
 - Bezpieczeństwo danych - zabezpieczenie Klucza API poprzez secrets.json
 - Asynchoroniczna komunikacja za API Google

---

## Instrukcja uruchomienia (lokalnie - przy użyciu Visual Studio)

1. Skolonowanie repozytorium na swoje urządzenie
2. Stworzenie lokalnej bazy danych na podstawie istniejącego już szablonu (wywołanie `Update-database` w menadżerze pakietów NuGet)
3. Wygenerowanie własnego klucza API w Google AI Studio
4. Dodanie klucza w secrets.json
5. Uruchomienie aplikacji