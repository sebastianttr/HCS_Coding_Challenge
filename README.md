## Coding Challenge Tatar - HCS Health Communtications Services

### Angabe der Challenge

```text
Write a C# command line program that gives me two options. One to ask a specific question and the other option is to add questions and their answers
 
Therefore the following restrictions apply:
    A Question is a String with max 255 chars
    An Answer is a String with max 255 chars
    
    A Question can have multiple answers (like bullet points)
        - If the user asks a question it has to be exactly the same as entered – no “best match”.
        - If the user asks a question which is not stored yet the program should print “the answer to life, universe and everything is 42” according to “The hitchhikers guide to the Galaxy”
        - If the user asks a question whish is stored the program should print all answers to that question. Every Answer in a separate line
    
    Adding a question looks like:
        <question>? “<answer1>” “<answer2>” “<answerX>”
        Char “?” is the separator between question and answers
        
    Every Question needs to have at least one answer but can have unlimited answers all inside of char “
        Provide tests for the functionality described in 1)
        Provide the source code on any GIT repo worldwide (GITLAB, GITHUB, whatever you prefer and/or use)
       
    No other restrictions apply
 
    Examples:
        Adding a question:
            What is Peters favorite food? “Pizza” “Spaghetti” “Ice cream”
            
        Asking a question which is in the system:
            What is Peters favorite food?
                Answers will be
                    - Pizza
                    - Spaghetti
                    - Ice cream
        Asking a question which is not in the system:
            When is Peters birthday?
                Answer will be
                    - the answer to life, universe and everything is 42
```

### Vorraussetzungen

Um dieses Program ausführen zu können, muss das dotnet SDK installiert. In meinem Fall 8.0.0 LTS
Dies kann sehr einfach über Chocolatey auf Windows oder Homebrew auf macOS installiert werden. 

Windows Chocolatey installation
```shell
choco install dotnet
```

macOS Chocolatey installation
```shell
brew install --cask dotnet
```

Nachdem dotnet installiert wurde, Terminal neu starten und ```dotnet --info``` eintippen. Es sollte information über die CLI kommen. 

### Ausführung

Um dieses Programm auszuführen, muss man zum Root- Ordner navigieren und folgendes eingeben:

```shell
dotnet run
```

Danach müssen sie einfach die Angaben der Console folgen, um das ganze Program durchzugehen.

### Test

Für die Tests habe ich mich für NUnitLentschieden, welches das gängste Unit Testing Framework für die .NET Umgebung ist. 

Folgendes Befehl führt die Tests aus: 

```shell
dotnet run -test
```