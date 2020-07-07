Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks

Namespace RestMultidialogoClient
    Class Program
        Private Shared http As HttpClient
        Private Shared done As Boolean = False

        Public Shared Sub Main(ByVal args As String())
            http = Utils.CreateHttpClient()
            TokenWallet.ReadTokens()

            While Not done
                Menu()
                Dim choice As Integer = Utils.GetChoice()

                If choice <= 0 Then
                    done = True
                    Continue While
                End If

                Authenticate()

                Select Case choice
                    Case 1
                        Scenario_1().Wait()
                    Case 2
                        Scenario_2().Wait()
                    Case 3
                        Scenario_3().Wait()
                    Case 4
                        Scenario_4().Wait()
                    Case 5
                        Scenario_5().Wait()
                    Case 6
                        Scenario_6().Wait()
                    Case 7
                        Scenario_7().Wait()
                    Case 8
                        Scenario_8().Wait()
                    Case Else
                        Console.WriteLine("Scelta errata")
                End Select
            End While
        End Sub

        Private Shared Sub Authenticate()
            If String.IsNullOrEmpty(TokenWallet.GetCurrentTokens().Token) Then
                Console.WriteLine("Richiesta di un nuovo token")
                Login(Constants.REST_MULTIDIALOGO_STAGE_USERNAME, Constants.REST_MULTIDIALOGO_STAGE_PASSWORD).Wait()
            Else
                Console.WriteLine("Riutilizzo token salvato " & TokenWallet.GetCurrentTokens().Token)
            End If
        End Sub

        Private Shared Sub Menu()
            Console.WriteLine("")
            Console.WriteLine("-------------------")
            Console.WriteLine("Scenari disponibili")
            Console.WriteLine("-------------------")
            Console.WriteLine(" 1 - Invio a 3 destinatari: 3 con posta tradizionale ma affrancatura diversa a carico dell'account (es. studio o condominio)")
            Console.WriteLine(" 2 - Invio a 3 destinatari: 1 con posta tradizionale, 2 MultiCerta che generano due canali alternativi con affrancatura diversa a carico dell'account (es. studio o condominio)")
            Console.WriteLine(" 3 - Esempio con errore: file globale associato a un destinatario")
            Console.WriteLine(" 4 - Legge loghi impostati in preferenze")
            Console.WriteLine(" 5 - Legge PEC impostata in preferenze")
            Console.WriteLine(" 6 - Elenco utenti collegati")
            Console.WriteLine(" 7 - Invio Certificazione Unica 2020")
            Console.WriteLine(" 8 - Legge tipo di affrancatura impostata in preferenze")
            Console.WriteLine(" 0 - Fine")
            Console.WriteLine("Scegli lo scenario: ")
        End Sub

        Private Shared Async Function Scenario_1() As Task
            Try
                Dim account As String = Utils.GetAccount()
                Dim uploadSessionId As String = Await GetUploadSessionId(account)
                Dim personale1 As File = Await PostFile(account, uploadSessionId, "personale1", "application/pdf", "private", Nothing)
                Dim personale2 As File = Await PostFile(account, uploadSessionId, "personale2", "application/pdf", "private", Nothing)
                Dim personale3 As File = Await PostFile(account, uploadSessionId, "personale3", "application/pdf", "private", Nothing)
                Dim globale1 As File = Await PostFile(account, uploadSessionId, "globale1", "application/pdf", "global", AttachmentOptions.CreateAttachmentOptions(True, "bw", "A4", 80, True, False))
                Dim attachments As Attachments = Attachments.CreateAttachments(uploadSessionId, New List(Of File) From {
                    personale1,
                    personale2,
                    personale3,
                    globale1
                })
                Dim recipient1 As Recipient = Recipient.CreateRecipient("Via Emilia Ovest 129/2", "43126", "Parma", "PR", "it", "pt", "RACCOMANDATA1", "person", "Winton", "Marsalis", "Multidialogo Srl", "esempio1@catchall.netbuilder.it", Nothing, New List(Of String) From {
                    personale1.Id
                }, "sendposta", Nothing, Nothing)
                Dim recipient2 As Recipient = Recipient.CreateRecipient("Via Zarotto 63", "43123", "Collecchio", "PR", "it", "pt", "RACCOMANDATA1AR", "person", "Clara", "Schumann", "ASA Srl", "esempio2@catchall.netbuilder.it", Nothing, New List(Of String) From {
                    personale2.Id
                }, "sendposta", Nothing, Nothing)
                Dim recipient3 As Recipient = Recipient.CreateRecipient("Via Zarotto 63", "43123", "Collecchio", "PR", "it", "pt", "PRIORITARIA1", "person", "Amilcare", "Ponchielli", "AM Spa", "esempio3@catchall.netbuilder.it", "info@pec.testtest.it", New List(Of String) From {
                    personale3.Id
                }, "sendposta", Nothing, Nothing)
                Dim postQueueDto As PostQueueDto = PostQueue.CreatePostQueue(Sender.CreateSender(), attachments, New List(Of Recipient) From {
                    recipient1,
                    recipient2,
                    recipient3
                }, "Convocazione assemblea", "Caro sei convocato per l'assemblea. Visualizza l'allegato. Grazie.", False, False, False, False, "Test scenario 1", Nothing)
                Await SendPostQueueRequest(account, postQueueDto)
            Catch e As ApiDialogException
                Console.WriteLine("Errore: " & e.Message)
            End Try
        End Function

        Private Shared Async Function Scenario_2() As Task
            Try
                Dim account As String = Utils.GetAccount()
                Dim uploadSessionId As String = Await GetUploadSessionId(account)
                Dim personale1 As File = Await PostFile(account, uploadSessionId, "personale1", "application/pdf", "private", Nothing)
                Dim personale2 As File = Await PostFile(account, uploadSessionId, "personale2", "application/pdf", "private", Nothing)
                Dim personale3 As File = Await PostFile(account, uploadSessionId, "personale3", "application/pdf", "private", Nothing)
                Dim globale1 As File = Await PostFile(account, uploadSessionId, "globale1", "application/pdf", "global", AttachmentOptions.CreateAttachmentOptions(True, "bw", "A4", 80, True, False))
                Dim attachments As Attachments = Attachments.CreateAttachments(uploadSessionId, New List(Of File) From {
                    personale1,
                    personale2,
                    personale3,
                    globale1
                })
                Dim recipient1 As Recipient = Recipient.CreateRecipient("Via Emilia Ovest 129/2", "43126", "Parma", "PR", "it", "pt", "RACCOMANDATA1", "person", "Winton", "Marsalis", "Multidialogo Srl", "esempio1@catchall.netbuilder.it", Nothing, New List(Of String) From {
                    personale1.Id
                }, "sendposta", Nothing, Nothing)
                Dim recipient2 As Recipient = Recipient.CreateRecipient("Via Zarotto 63", "43123", "Collecchio", "PR", "it", "pt", "RACCOMANDATA1AR", "person", "Clara", "Schumann", "ASA Srl", Constants.MULTICERTA_ENABLED_ADDRESS, Nothing, New List(Of String) From {
                    personale2.Id
                }, "multicerta", "sendposta", Nothing)
                Dim recipient3 As Recipient = Recipient.CreateRecipient("Via Zarotto 63", "43123", "Collecchio", "PR", "it", "pt", "PRIORITARIA1", "person", "Amilcare", "Ponchielli", "AM Spa", Constants.MULTICERTA_ENABLED_ADDRESS, "info@pec.testtest.it", New List(Of String) From {
                    personale3.Id
                }, "multicerta", "sendposta", Nothing)
                Dim postQueueDto As PostQueueDto = PostQueue.CreatePostQueue(Sender.CreateSender(), attachments, New List(Of Recipient) From {
                    recipient1,
                    recipient2,
                    recipient3
                }, "Convocazione assemblea", "Caro sei convocato per l'assemblea. Visualizza l'allegato. Grazie.", True, False, False, False, "Test scenario 1", Nothing)
                Await SendPostQueueRequest(account, postQueueDto)
            Catch e As ApiDialogException
                Console.WriteLine("Errore: " & e.Message)
            End Try
        End Function

        Private Shared Async Function Scenario_3() As Task
            Try
                Dim account As String = Utils.GetAccount()
                Dim uploadSessionId As String = Await GetUploadSessionId(account)
                Dim personale1 As File = Await PostFile(account, uploadSessionId, "personale1", "application/pdf", "private", Nothing)
                Dim personale2 As File = Await PostFile(account, uploadSessionId, "personale2", "application/pdf", "private", Nothing)
                Dim globale1 As File = Await PostFile(account, uploadSessionId, "globale1", "application/pdf", "global", AttachmentOptions.CreateAttachmentOptions(True, "bw", "A4", 80, True, False))
                Dim attachments As Attachments = Attachments.CreateAttachments(uploadSessionId, New List(Of File) From {
                    personale1,
                    personale2,
                    globale1
                })
                Dim recipient1 As Recipient = Recipient.CreateRecipient("Via Emilia Ovest 129/2", "43126", "Parma", "PR", "it", "pt", "RACCOMANDATA1", "person", "Winton", "Marsalis", "Multidialogo Srl", "esempio1@catchall.netbuilder.it", Nothing, New List(Of String) From {
                    personale1.Id
                }, "sendposta", Nothing, Nothing)
                Dim recipient2 As Recipient = Recipient.CreateRecipient("Via Zarotto 63", "43123", "Collecchio", "PR", "it", "pt", "RACCOMANDATA1AR", "person", "Clara", "Schumann", "ASA Srl", "esempio2@catchall.netbuilder.it", Nothing, New List(Of String) From {
                    globale1.Id,
                    personale2.Id
                }, "sendposta", Nothing, Nothing)
                Dim postQueueDto As PostQueueDto = PostQueue.CreatePostQueue(Sender.CreateSender(), attachments, New List(Of Recipient) From {
                    recipient1,
                    recipient2
                }, "Convocazione assemblea", "Caro sei convocato per l'assemblea. Visualizza l'allegato. Grazie.", False, False, False, False, "Test scenario 1", Nothing)
                Await SendPostQueueRequest(account, postQueueDto)
            Catch e As ApiDialogException
                Console.WriteLine("Errore: " & e.Message)
            End Try
        End Function

        Private Shared Async Function Scenario_4() As Task
            Dim account As String = Utils.GetAccount()
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/preferences"
            Dim response As HttpResponseMessage = Await SendRequest(url, Nothing, "Get")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Return
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim userPreferences As UserPreferencesData = JsonConvert.DeserializeObject(Of UserPreferencesData)(responseBody)
            Console.WriteLine("Loghi:")
            Console.WriteLine("------------------------")
            Console.WriteLine("Id | Label | Default")
            Console.WriteLine("------------------------")

            For Each l As SenderLetterWatermarkPreferences In userPreferences.GetLoghi()
                Console.WriteLine(l.ToString())
            Next
        End Function

        Private Shared Async Function Scenario_5() As Task
            Dim account As String = Utils.GetAccount()
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/preferences"
            Dim response As HttpResponseMessage = Await SendRequest(url, Nothing, "Get")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Return
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim userPreferences As UserPreferencesData = JsonConvert.DeserializeObject(Of UserPreferencesData)(responseBody)
            Console.WriteLine("Pec:")
            Console.WriteLine("---------")
            Console.WriteLine("Indirizzo")
            Console.WriteLine("---------")

            For Each p As String In userPreferences.GetPec()
                Console.WriteLine(p)
            Next
        End Function

        Private Shared Async Function Scenario_6() As Task
            Dim account As String = "me"
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/users?include=user-profiles"
            Dim response As HttpResponseMessage = Await SendRequest(url, Nothing, "Get")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Return
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim userResponse As UserResponse = JsonConvert.DeserializeObject(Of UserResponse)(responseBody)
            Console.WriteLine("Utenti:")
            Console.WriteLine("----------------------------------------------")
            Console.WriteLine("Id | IsActive | Group | Username | Displayname")
            Console.WriteLine("----------------------------------------------")

            For Each u As UserExtended In userResponse.GetUsers()
                Console.WriteLine(u)
            Next
        End Function

        Private Shared Async Function Scenario_7() As Task
            Dim account As String = "me"
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/tax-withholding-transmission-sessions"
            Dim cuPostRequestDto As CuPostRequestDto = CuPostRequestDto.CreatePostRequestDto("esempio_cu")
            Dim json As String = JsonConvert.SerializeObject(cuPostRequestDto)
            Dim response As HttpResponseMessage = Await SendRequest(url, json, "Post")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Return
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Console.WriteLine("CU inviata")
            Else
                Console.WriteLine("Si è verificato un errore! Dettagli:")
                HandleErrors(responseBody, cuPostRequestDto)
            End If
        End Function

        Private Shared Async Function Scenario_8() As Task
            Dim account As String = Utils.GetAccount()
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/preferences"
            Dim response As HttpResponseMessage = Await SendRequest(url, Nothing, "Get")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Return
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim userPreferences As UserPreferencesData = JsonConvert.DeserializeObject(Of UserPreferencesData)(responseBody)
            Console.WriteLine("Affrancatura impostata: " & userPreferences.GetSenderPostageType())
        End Function

        Private Shared Async Function SendPostQueueRequest(ByVal account As String, ByVal postQueueDto As PostQueueDto) As Task
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/queues"
            Dim json As String = JsonConvert.SerializeObject(postQueueDto)
            Dim response As HttpResponseMessage = Await SendRequest(url, json, "Post")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Throw New ApiDialogException("Impossibile creare la coda")
            End If

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim status As String = Utils.GetResponseStatus(responseBody)

            If status.Equals("CREATED") Then
                Console.WriteLine("Coda creata")
            Else
                Console.WriteLine("Si è verificato un errore! Dettagli:")
                Console.WriteLine(responseBody)
                HandleErrors(responseBody, postQueueDto)
            End If
        End Function

        Private Shared Sub HandleErrors(ByVal response As String, ByVal postQueueDto As Object)
            Dim errorResponse As Dto.ErrorResponse = JsonConvert.DeserializeObject(Of ErrorResponse)(response)

            For Each p As Parameter In errorResponse.GetParameters()
                Console.WriteLine(p.code & " - " + p.parameter)
                ErrorUtils.ParseDotNotation(p.parameter, postQueueDto)

                For Each m As String In p.messages
                    Console.WriteLine(m)
                Next
            Next
        End Sub

        Private Shared Async Function PostFile(ByVal account As String, ByVal uploadSessionId As String, ByVal fileName As String, ByVal mimeType As String, ByVal visibility As String, ByVal options As AttachmentOptions) As Task(Of File)
            Dim fileContent As String = Utils.CreateFileContent(fileName, mimeType)
            Dim json As String = Utils.CreatePostFilePayload(fileName, fileContent)
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/upload-sessions/" & uploadSessionId & "/uploaded-files"
            Dim response As HttpResponseMessage = Await SendRequest(url, json, "Post")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Throw New ApiDialogException("Impossibile ottenere fileId")
            End If

            Dim fileId As String = Utils.GetResponseId(Await response.Content.ReadAsStringAsync())
            Console.WriteLine("PostFile Id = " & fileId)
            Return New File(fileId, visibility, options)
        End Function

        Private Shared Async Function GetUploadSessionId(ByVal account As String) As Task(Of String)
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/" & account & "/upload-sessions"
            Dim response As HttpResponseMessage = Await SendRequest(url, Constants.EMPTY_JSON, "Post")

            If response Is Nothing OrElse response.Content Is Nothing Then
                Throw New ApiDialogException("Impossibile ottenere uploadSessionid")
            End If

            Dim uploadSessionId As String = Utils.GetResponseId(Await response.Content.ReadAsStringAsync())
            Console.WriteLine("UploadSessionId = " & uploadSessionId)
            Return uploadSessionId
        End Function

        Private Shared Async Function SendRequest(ByVal url As String, ByVal json As String, ByVal method As String) As Task(Of HttpResponseMessage)
            Dim response As HttpResponseMessage = Nothing
            Dim done As Boolean = False

            Do
                response = Await ExecuteCall(url, json, method)

                If response.IsSuccessStatusCode Then
                    done = True
                ElseIf response.StatusCode = HttpStatusCode.InternalServerError Then
                    Console.WriteLine("Server error (500)!")
                    Return Nothing
                ElseIf response.StatusCode = HttpStatusCode.NotFound Then
                    Console.WriteLine("NotFound error (404)!")
                    Return Nothing
                ElseIf response.StatusCode = HttpStatusCode.Forbidden Then
                    Console.WriteLine("Forbidden error (403)!")
                    Return Nothing
                ElseIf response.StatusCode = HttpStatusCode.BadRequest Then
                    Console.WriteLine("Bad request error (400)!")
                    done = True
                ElseIf response.StatusCode = HttpStatusCode.Unauthorized Then
                    Console.WriteLine("Token scaduto -> Refresh")
                    Dim res As HttpResponseMessage = Await LoginRefresh(Constants.REST_MULTIDIALOGO_STAGE_USERNAME)

                    If res.StatusCode = HttpStatusCode.Unauthorized Then
                        Console.WriteLine("Refresh token scaduto -> Login")
                        Login(Constants.REST_MULTIDIALOGO_STAGE_USERNAME, Constants.REST_MULTIDIALOGO_STAGE_PASSWORD).Wait()
                    End If
                End If
            Loop While Not done

            Return response
        End Function

        Private Shared Async Function ExecuteCall(ByVal url As String, ByVal json As String, ByVal method As String) As Task(Of HttpResponseMessage)
            Dim request As HttpRequestMessage = Utils.CreateCurrTokenRequest(url, json, method)
            Return Await http.SendAsync(request)
        End Function

        Private Shared Async Function LoginRefresh(ByVal username As String) As Task(Of HttpResponseMessage)
            Dim token As String = TokenWallet.GetCurrentTokens().RefreshToken
            Dim json As String = RefreshTokenRequest.CreateRefreshTokenRequestAsJson(username, token)
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/login/refresh"
            Dim res As HttpResponseMessage = Await http.SendAsync(Utils.CreateRequest(url, token, Utils.CreateStringContent(json), "Post"))

            If res.IsSuccessStatusCode Then
                Dim responseContent As String = Await res.Content.ReadAsStringAsync()
                Dim authResponse As AuthResponse = JsonConvert.DeserializeObject(Of AuthResponse)(responseContent)
                Console.WriteLine("Refresh token ricevuto: " & authResponse.GetTokenResponse().RefreshToken)
                StoreTokens(authResponse)
            End If

            Return res
        End Function

        Private Shared Sub StoreTokens(ByVal authResponse As AuthResponse)
            TokenWallet.WriteTokens(authResponse.GetTokenResponse())
        End Sub

        Private Shared Async Function Login(ByVal username As String, ByVal password As String) As Task
            Dim json As String = LoginRequestData.CreateLoginRequestDataAsJson(username, password)
            Dim url As String = Constants.REST_MULTIDIALOGO_STAGE_HOST & "/users/login"
            Dim response As HttpResponseMessage = Await http.SendAsync(Utils.CreateRequest(url, Nothing, Utils.CreateStringContent(json), "Post"))
            Dim responseContent As String = Await response.Content.ReadAsStringAsync()
            Dim authResponse As AuthResponse = JsonConvert.DeserializeObject(Of AuthResponse)(responseContent)
            Console.WriteLine("Nuovo token ricevuto: " & authResponse.GetTokenResponse().Token)
            StoreTokens(authResponse)
        End Function
    End Class
End Namespace
