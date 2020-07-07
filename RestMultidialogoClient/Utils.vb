Imports System
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

Namespace RestMultidialogoClient
    Module Utils
        Function CreateFileContent(ByVal fileName As String, ByVal mimeType As String) As String
            Dim buffer As Byte() = ReadFileFromResources(fileName)
            Return "data:" & mimeType & ";base64," & Convert.ToBase64String(buffer)
        End Function

        Private Function ReadFileFromResources(ByVal fileName As String) As Byte()
            Return (CType((RestMultidialogoClient.Resources.ResourceManager.GetObject(fileName)), Byte()))
        End Function

        Function CreatePostFilePayload(ByVal fileName As String, ByVal fileContent As String) As String
            Dim uploadFileRequestData As UploadFileRequestData = UploadFileRequestData.CreateUploadFileRequestData(fileName, fileContent)
            Return JsonConvert.SerializeObject(uploadFileRequestData)
        End Function

        Function GetChoice() As Integer
            Return Convert.ToInt32(Console.ReadLine())
        End Function

        Function GetAccount() As String
            Console.WriteLine("Inserire l'account desiderato: ""me"" per l'amministratore, l'id per uno dei sottoutenti (condomini)")
            Console.WriteLine("NB: la API che ritorna gli id dei sottoutenti disponibili è mostrata nell'esempio 6 - utenti collegati")
            Return Console.ReadLine()
        End Function

        Function GetResponseId(ByVal respBody As String) As String
            Return GetGenericResponse(respBody).GetId()
        End Function

        Private Function GetGenericResponse(ByVal respBody As String) As GenericResponse
            Return JsonConvert.DeserializeObject(Of GenericResponse)(respBody)
        End Function

        Function GetResponseStatus(ByVal respBody As String) As String
            Return GetGenericResponse(respBody).Status
        End Function

        Function CreateStringContent(ByVal json As String) As StringContent
            Return If(String.IsNullOrEmpty(json), Nothing, New StringContent(json, Encoding.UTF8, "application/json"))
        End Function

        Function CreateHttpClient() As HttpClient
            Dim http = New HttpClient()
            http.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            Return http
        End Function

        Function CreateRequest(ByVal url As String, ByVal token As String, ByVal json As StringContent, ByVal method As String) As HttpRequestMessage
            Return If(method.Equals("Post"), BuildRequest(url, token, json, "Post"), BuildRequest(url, token, json, "Get"))
        End Function

        Private Function BuildRequest(ByVal url As String, ByVal token As String, ByVal json As StringContent, ByVal method As String) As HttpRequestMessage
            Dim request = New HttpRequestMessage(New HttpMethod(method), New Uri(url))
           
            request.Headers.Add("X-api-client-name", Constants.X_API_CLIENT_NAME)
            '
            ' ATTENZIONE: 
            ' l'api key dovrebbe essere custodita in maniera sicura, non salvata in chiaro su disco, ad esempio. 
            '
            request.Headers.Add("X-api-key", Constants.X_API_KEY)
            request.Headers.Add("X-api-client-version", Constants.X_API_CLIENT_VERSION)
            request.Headers.Add("Accept-Language", "it")

            If Not String.IsNullOrEmpty(token) Then
                request.Headers.Add("Authorization", "Bearer " & token)
            End If

            request.Content = json
            Return request
        End Function

        Function CreateCurrTokenRequest(ByVal url As String, ByVal json As String, ByVal method As String) As HttpRequestMessage
            Return CreateRequest(url, TokenWallet.GetCurrentTokens().Token, CreateStringContent(json), method)
        End Function
    End Module
End Namespace
