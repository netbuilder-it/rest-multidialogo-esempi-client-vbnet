Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace RestMultidialogoClient
    Module Dto
        Public Class DataPayload(Of T)
            Public id As String
            Public type As String
            Public attributes As T

            Public Function GetId() As String
                Return id
            End Function

            Public Function GetAttributes() As T
                Return attributes
            End Function

            Public Sub New(ByVal attributes As T)
                Me.attributes = attributes
            End Sub
        End Class

        Public Class LoginRequest
            Public ReadOnly username As String
            Public ReadOnly password As String
            Public ReadOnly grantType As String

            Public Sub New(ByVal username As String, ByVal password As String)
                Me.username = username
                Me.password = password
                Me.grantType = "password"
            End Sub
        End Class

        Public Class LoginRequestData
            Public data As DataPayload(Of LoginRequest)

            Public Sub New(ByVal data As DataPayload(Of LoginRequest))
                Me.data = data
            End Sub

            Private Shared Function CreateLoginRequestData(ByVal username As String, ByVal password As String) As LoginRequestData
                Return New LoginRequestData(New DataPayload(Of LoginRequest)(New LoginRequest(username, password)))
            End Function

            Public Shared Function CreateLoginRequestDataAsJson(ByVal username As String, ByVal password As String) As String
                Dim loginRequestData As LoginRequestData = CreateLoginRequestData(username, password)
                Return JsonConvert.SerializeObject(loginRequestData)
            End Function
        End Class

        Public Class RefreshToken
            Public username As String
            Public refreshToken As String
            Public grantType As String

            Public Sub New(ByVal username As String, ByVal refreshToken As String)
                Me.username = username
                Me.refreshToken = refreshToken
                Me.grantType = "refresh-token"
            End Sub
        End Class

        Public Class RefreshTokenRequest
            Public data As DataPayload(Of RefreshToken)

            Public Sub New(ByVal data As DataPayload(Of RefreshToken))
                Me.data = data
            End Sub

            Private Shared Function CreateRefreshTokenRequest(ByVal username As String, ByVal refreshToken As String) As RefreshTokenRequest
                Return New RefreshTokenRequest(New DataPayload(Of RefreshToken)(New RefreshToken(username, refreshToken)))
            End Function

            Public Shared Function CreateRefreshTokenRequestAsJson(ByVal username As String, ByVal token As String) As String
                Dim refreshTokenRequest As RefreshTokenRequest = CreateRefreshTokenRequest(username, token)
                Return JsonConvert.SerializeObject(refreshTokenRequest)
            End Function
        End Class

        Public Class TokenResponse
            Public Token As String
            Public RefreshToken As String
            Private Category As String

            Public Sub New(ByVal token As String, ByVal refreshToken As String)
                Me.Token = token
                Me.RefreshToken = refreshToken
            End Sub
        End Class

        Public Class AuthResponse
            Public status As String
            Public data As DataPayload(Of TokenResponse)

            Public Function GetTokenResponse() As TokenResponse
                Return If(data?.GetAttributes(), Nothing)
            End Function
        End Class

        Public Class GenericResponse
            Public Status As String
            Public data As DataPayload(Of Object)

            Public Function GetId() As String
                Return data?.GetId()
            End Function
        End Class

        Public Class UploadFileRequest
            Public filename As String
            Public fileData As String
            Public customId As String

            Public Sub New(ByVal filename As String, ByVal fileData As String)
                Me.filename = filename
                Me.fileData = fileData
            End Sub
        End Class

        Public Class UploadFileRequestData
            Public data As DataPayload(Of UploadFileRequest)

            Public Sub New(ByVal data As DataPayload(Of UploadFileRequest))
                Me.data = data
            End Sub

            Public Shared Function CreateUploadFileRequestData(ByVal fileName As String, ByVal fileContent As String) As UploadFileRequestData
                Return New UploadFileRequestData(New DataPayload(Of UploadFileRequest)(New UploadFileRequest(fileName, fileContent)))
            End Function
        End Class

        Public Class Parameter
            Public parameter As String
            Public code As String
            Public messages As List(Of String)
        End Class

        Public Class Source
            Public parameters As List(Of Parameter)
        End Class

        Public Class ErrorResponse
            Public id As String
            Public status As String
            Public code As String
            Public title As String
            Public detail As String
            Public source As Source

            Public Function GetParameters() As List(Of Parameter)
                Return source.parameters
            End Function
        End Class

        Public Class AppPush
            Public blocked As Boolean
            Public delivered As Boolean
            Public refused As Boolean
        End Class

        Public Class Credit
            Public serviceThreshold As Single
            Public postageThreshold As Single
            Public notificationEmail As String
        End Class

        Public Class DailyDigest
            Public queue As List(Of String)
        End Class

        Public Class Space
            Public threshold As Integer
            Public notificationEmail As String
        End Class

        Public Class NotificationPreferences
            Public appPush As AppPush
            Public credit As Credit
            Public dailyDigest As DailyDigest
            Public space As Space
        End Class

        Public Class SenderMulticertaPostageUserPreferences
            Public legalPostageTypeLabel As String
            Public postageTypeLabel As String
        End Class

        Public Class SenderMulticertaDeadlineUserPreferences
            Public acknowledgement As String
            Public duration As Integer
            Public legalAcknowledgement As String
            Public legalDuration As Integer
        End Class

        Public Class SenderMulticertaPreferences
            Public postage As SenderMulticertaPostageUserPreferences
            Public deadline As SenderMulticertaDeadlineUserPreferences
        End Class

        Public Class SenderEmailPreferences
            Public certifiedAddresses As List(Of String)
            Public notificationAddress As String
            Public displayAddress As String
        End Class

        Public Class SenderFaxPreferences
            Public name As String
            Public prefix As String
            Public number As String
            Public notificationEmail As String
            Public cover As String
        End Class

        Public Class SenderSmsPreference
            Public type As String
            Public display As String
            Public prefix As String
            Public number As String
            Public [alias] As String
            Public notificationEmail As String
        End Class

        Public Class SenderLetterWatermarkPreferences
            Public id As Integer
            Public label As String
            Public [default] As Boolean

            Public Overrides Function ToString() As String
                Return $"{id} {label} {[default]}"
            End Function
        End Class

        Public Class SenderLetterPrintPreferences
            Public frontBack As Boolean
            Public colorMode As String
            Public sheetFormat As String
            Public weight As Integer
            Public staple As Boolean
        End Class

        Public Class SenderLetterPostageVectorsPreferences
            Public type As String
            Public vector As String
        End Class

        Public Class SenderLetterPostageVectorEnabling
            Public vector As String
            Public enabledAt As String
            Public disabledAt As String
        End Class

        Public Class SenderLetterPostagePreferences
            Public type As String
            Public vectors As List(Of SenderLetterPostageVectorsPreferences)
            Public vectorEnablings As List(Of SenderLetterPostageVectorEnabling)
            Public topicOnReturnReceipt As Boolean
        End Class

        Public Class SenderLetterPreferences
            Public watermarks As List(Of SenderLetterWatermarkPreferences)
            Public postage As SenderLetterPostagePreferences
            Public print As SenderLetterPrintPreferences
            Public zipCode As String
            Public streetAddress As String
            Public admLvl2 As String
            Public admLvl3 As String
            Public countryCodeOrStateName As String
            Public notificationEmail As String

            Public Function GetPostageType() As String
                Return postage?.type
            End Function
        End Class

        Public Class SenderPreferences
            Public multicerta As SenderMulticertaPreferences
            Public email As SenderEmailPreferences
            Public fax As SenderFaxPreferences
            Public sms As SenderSmsPreference
            Public letter As SenderLetterPreferences

            Public Function GetLoghi() As List(Of SenderLetterWatermarkPreferences)
                Return If(letter?.watermarks, New List(Of SenderLetterWatermarkPreferences)())
            End Function

            Public Function GetPec() As List(Of String)
                Return If(email?.certifiedAddresses, New List(Of String)())
            End Function

            Public Function GetPostageType() As String
                Return letter?.GetPostageType()
            End Function
        End Class

        Public Class MultiboxRecipientEnvelopeRecipientListPreferences
            Public subject As String
        End Class

        Public Class MultiboxRecipientEnvelopePreferences
            Public package As String
            Public recipientList As MultiboxRecipientEnvelopeRecipientListPreferences
        End Class

        Public Class MultiboxRecipientPreferences
            Public firstname As String
            Public lastname As String
            Public companyName As String
            Public addressee As String
            Public streetAddress As String
            Public zipCode As String
            Public admLvl2 As String
            Public admLvl3 As String
            Public envelop As MultiboxRecipientEnvelopePreferences
        End Class

        Public Class RecipientPreferences
            Public multibox As MultiboxRecipientPreferences
        End Class

        Public Class UserPreference
            Public notification As NotificationPreferences
            Public sender As SenderPreferences
            Public recipient As RecipientPreferences

            Public Function GetLoghi() As List(Of SenderLetterWatermarkPreferences)
                Return If(sender?.GetLoghi(), New List(Of SenderLetterWatermarkPreferences)())
            End Function

            Public Function GetPec() As List(Of String)
                Return If(sender?.GetPec(), New List(Of String)())
            End Function

            Public Function GetSenderPostageType() As String
                Return sender?.GetPostageType()
            End Function
        End Class

        Public Class UserPreferenceSet
            Public userId As Integer
            Public preset As UserPreference
            Public current As UserPreference
            Public requested As UserPreference
        End Class

        Public Class UserPreferencesData
            Public data As DataPayload(Of UserPreferenceSet)

            Public Function GetLoghi() As List(Of SenderLetterWatermarkPreferences)
                Dim ret As List(Of SenderLetterWatermarkPreferences) = New List(Of SenderLetterWatermarkPreferences)()

                If data.GetAttributes().requested IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().requested.GetLoghi())
                End If

                If data.GetAttributes().current IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().current.GetLoghi())
                End If

                If data.GetAttributes().preset IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().preset.GetLoghi())
                End If

                Return ret
            End Function

            Public Function GetPec() As List(Of String)
                Dim ret As List(Of String) = New List(Of String)()

                If data.GetAttributes().requested IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().requested.GetPec())
                End If

                If data.GetAttributes().current IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().current.GetPec())
                End If

                If data.GetAttributes().preset IsNot Nothing Then
                    ret.AddRange(data.GetAttributes().preset.GetPec())
                End If

                Return ret
            End Function

            Public Function GetSenderPostageType() As String
                Dim attributes As UserPreferenceSet = data.GetAttributes()
                Return If(attributes.requested?.GetSenderPostageType(), If(attributes.current?.GetSenderPostageType(), attributes.preset?.GetSenderPostageType()))
            End Function
        End Class

        Public Class User
            Public isActive As Boolean
            Public group As String

            Public Overrides Function ToString() As String
                Return $"{isActive} {group}"
            End Function
        End Class

        Public Class Profile
            Public id As String
            Public type As String
        End Class

        Public Class ProfileData
            Public data As Profile
        End Class

        Public Class ProfileEnv
            Public profile As ProfileData
        End Class

        Public Class UserDto
            Public id As String
            Public type As String
            Public attributes As User
            Public relationships As ProfileEnv

            Public Overrides Function ToString() As String
                Return $"{id} {type} {attributes}"
            End Function
        End Class

        Public Class Meta
            Public total As Integer
        End Class

        Public Class UserProfile
            Public username As String
            Public displayName As String

            Public Overrides Function ToString() As String
                Return $"{username} {displayName}"
            End Function
        End Class

        Public Class UserExtended
            Public id As String
            Public user As User
            Public profile As UserProfile

            Public Overrides Function ToString() As String
                Return $"{id} {user} {profile}"
            End Function
        End Class

        Public Class UserResponse
            Public status As String
            Public meta As Meta
            Public data As List(Of UserDto)
            Public included As List(Of DataPayload(Of UserProfile))

            Public Function GetUsers() As List(Of UserExtended)
                Dim ret As List(Of UserExtended) = New List(Of UserExtended)()
                Dim users As List(Of UserDto) = If(data, New List(Of UserDto)())

                For Each u As UserDto In users
                    Dim ue As UserExtended = New UserExtended()
                    ue.id = u.id
                    ue.user = u.attributes
                    ue.profile = If(included?.Find(Function(x) x.id.Equals(ue.id)).GetAttributes(), Nothing)
                    ret.Add(ue)
                Next

                Return ret
            End Function
        End Class

        Public Class CuTrack
            Public fileData As String

            Public Sub New(ByVal fileContent As String)
                Me.fileData = fileContent
            End Sub
        End Class

        Public Class CuPostRequest
            Public label As String
            Public countryCode As String
            Public billingMode As String
            Public track As CuTrack
            Public type as String

            Public Sub New(ByVal fileContent As String)
                Me.label = "Esempio"
                Me.countryCode = "it"
                Me.billingMode = "CLAIM"
                Me.type = "CU"
                Me.track = New CuTrack(fileContent)
            End Sub
        End Class

        Public Class CuPostRequestDto
            Public data As DataPayload(Of CuPostRequest)

            Public Sub New(ByVal postRequest As CuPostRequest)
                Me.data = New DataPayload(Of CuPostRequest)(postRequest)
            End Sub

            Public Shared Function CreatePostRequestDto(ByVal filename As String) As CuPostRequestDto
                Dim fileContent As String = Utils.CreateFileContent(filename, "text/plain")
                Return New CuPostRequestDto(New CuPostRequest(fileContent))
            End Function

            Public Shared Function CreatePostRequestDtoAsJson(ByVal filename As String) As String
                Return JsonConvert.SerializeObject(CreatePostRequestDto(filename))
            End Function
        End Class
    End Module
End Namespace
