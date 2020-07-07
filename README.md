# API Rest Multidialogo

## Esempi d'uso
Questa è un'applicazione di tipo console scritta in VB.NET che mostra alcuni esempi d'uso significativi delle API di Rest Multidialogo.

## Requisiti
E' richiesto il framework .NET Core 3.0.

## Impostazioni iniziali 
Nel file Constants.vb è necessario impostare una serie di parametri di identificazione, che sono spiegati sotto.

Le credenziali fornite per l'accesso alla piattaforma di stage beta.multidialogo.it:

```
    Public Const REST_MULTIDIALOGO_STAGE_USERNAME As String = "inserire_username"
    Public Const REST_MULTIDIALOGO_STAGE_PASSWORD As String = "inserire_password"
```
I dati del mittente:
```
    Public Const SENDER_DISPLAY_ADDRESS As String = ""
    Public Const SENDER_NOTIFICATION_ADDRESS As String = ""
    Public Const SENDER_CERTIFIED_ADDRESS As String = ""
    Public Const SENDER_COMPANY_NAME As String = ""
    Public Const SENDER_STREET_ADDRESS As String = ""
    Public Const SENDER_ADM_LVL3 As String = ""
    Public Const SENDER_ADM_LVL2 As String = ""
    Public Const SENDER_COUNTRY As String = ""
    Public Const SENDER_ZIP_CODE As String = ""
    Public Const SENDER_VAT_CODE As String = ""
```
L'indirizzo del destinatario che ha la Multicerta attiva (necessario per l'esempio d'uso della Multicerta):
```
    Public Const MULTICERTA_ENABLED_ADDRESS As String = ""
```
I parametri per l'autenticazione del client:
```
    Public Const X_API_CLIENT_NAME As String = ""
    Public Const X_API_KEY As String = ""
    Public Const X_API_CLIENT_VERSION As String = ""
```

Le credenziali di accesso e di identificazione del client verranno fornite separatamente.

## Build e esecuzione
L'applicazione si può compilare con i seguenti comandi da command shell:
```
dotnet clean
dotnet build
```
Per eseguirla:
```
dotnet run
```
