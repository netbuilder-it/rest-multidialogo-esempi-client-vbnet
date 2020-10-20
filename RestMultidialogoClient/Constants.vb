Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace RestMultidialogoClient
    Module Constants
        Public Const REST_MULTIDIALOGO_STAGE_HOST As String = "https://rest-stage.multidialogo.it/api/v0.0.1"

        Public Const REST_MULTIDIALOGO_STAGE_USERNAME As String = "your_username"
        Public Const REST_MULTIDIALOGO_STAGE_PASSWORD As String = "your_password"

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

        Public Const SENDER_SMS_ALIAS_UUID As String = ""
        Public Const SENDER_PHONE_NUMBER_UUID As String = ""

        Public Const MULTICERTA_ENABLED_ADDRESS As String = ""

        Public Const X_API_CLIENT_NAME As String = "test-client"
        Public Const X_API_KEY As String = "development_authorized_key"
        Public Const X_API_CLIENT_VERSION As String = "0.0.1"
        
        Public Const EMPTY_JSON As String = "{ ""data"": {} }"
    End Module
End Namespace
