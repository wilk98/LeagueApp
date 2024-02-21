# LeagueApp
## Kroki i działania w trakcie realizacji
Rozpocząłem ten projekt od utworzenia aplikacji internetowej MVC w .NET, mając na celu zaprezentowanie tabeli ligowej z drużynami oraz wynikami meczów. Pierwszym krokiem było zainstalowanie niezbędnych pakietów Entity Framework do zarządzania danymi oraz Identity do obsługi logowania i rejestracji użytkowników.

Następnie skupiłem się na stworzeniu modeli, które miały odzwierciedlać strukturę ligi, drużyn oraz meczów. Po zdefiniowaniu modeli, utworzyłem DbContext, aby móc skomunikować się z bazą danych, a następnie wygenerowałem schemat bazy danych. W trakcie tworzenia aplikacji, równolegle rozwijałem serwisy i interfejsy do obsługi logiki biznesowej, a także kontrollery i widoki do interakcji z użytkownikiem.

Po implementacji funkcji logowania i rejestracji, dodałem możliwość dynamicznego przeglądania meczów z poziomu widoku tabeli ligowej oraz funkcjonalność dodawania drużyn do ulubionych przez zalogowanych użytkowników.

## Kroki uruchomienia aplikacji:
1. Instalacja pakietów.
2. Wykonanie migracji i seedowanie bazy danych poprzez **'Update-Database'** w konsoli menadżera pakietów.
3. Uruchomienie aplikacji.

## Nawigacja po aplikacji
Po uruchomieniu aplikacji, użytkownik od razu zauważy Ligę Belgijską. Po kliknięciu na nią dostępne są dwie opcje: Tabela Ligowa i Mecze.

W sekcji Tabela Ligowa, drużyny są posortowane według zdobytych punktów. Kliknięcie na nazwę drużyny rozwija sekcję, prezentując szczegóły meczów danej drużyny.

W sekcji Mecze, wszystkie mecze są podzielone na kolejki. Dla każdej kolejki, najlepszy mecz jest wyróżniony, co pozwala szybko zidentyfikować najbardziej emocjonujące spotkania.

### Funkcje dla Zalogowanych Użytkowników
W prawym górnym rogu ekranu znajdują się opcje Logowania i Rejestracji. Po zalogowaniu, użytkownik może dodawać wybrane drużyny do swoich ulubionych, co umożliwia szybki dostęp do ich wyników i meczów.

## Dalszy Rozwój Aplikacji
Można dodać JWT Tokeny, aby zapewnić lepsze zarządzanie sesją użytkownika oraz wprowadzić profil admina, który będzie mógł zarządzać(dodawać/usuwać) ligami, drużynami i meczami. Dodatkowo, można rozważyć wprowadzenie komentarzy użytkowników oraz bardziej szczegółowych informacji o drużynach i meczach.
