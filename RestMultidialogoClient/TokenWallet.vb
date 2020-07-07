Imports System
Imports System.Configuration

Namespace RestMultidialogoClient
    Module TokenWallet
        '
        ' ATTENZIONE: 
        ' i token dovrebbero essere custoditi in maniera sicura. Ad esempio _non_ salvati in chiaro su disco. 
        '
        Private tokens As TokenResponse

        Function GetCurrentTokens() As TokenResponse
            Return tokens
        End Function

        Sub ReadTokens()
            Dim token As String = ConfigurationManager.AppSettings("Token")
            Dim refreshToken As String = ConfigurationManager.AppSettings("RefreshToken")
            SetCurrentTokens(New TokenResponse(token, refreshToken))
        End Sub

        Private Sub SetCurrentTokens(ByVal tokenResponse As TokenResponse)
            tokens = tokenResponse
        End Sub

        Sub WriteTokens(ByVal tokenResponse As TokenResponse)
            Try
                Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim settings = configFile.AppSettings.Settings

                If settings("Token") Is Nothing Then
                    settings.Add("Token", tokenResponse.Token)
                Else
                    settings("Token").Value = tokenResponse.Token
                End If

                If settings("RefreshToken") Is Nothing Then
                    settings.Add("RefreshToken", tokenResponse.RefreshToken)
                Else
                    settings("RefreshToken").Value = tokenResponse.RefreshToken
                End If

                configFile.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
                SetCurrentTokens(tokenResponse)
            Catch __unusedConfigurationErrorsException1__ As ConfigurationErrorsException
                Console.WriteLine("Error writing app settings")
            End Try
        End Sub
    End Module
End Namespace
