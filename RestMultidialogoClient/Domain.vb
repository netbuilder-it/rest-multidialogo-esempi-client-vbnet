Imports System.Collections.Generic

Namespace RestMultidialogoClient
    Module Domain
        Public Class Email
            Public ReadOnly DisplayAddress As String
            Public ReadOnly NotificationAddress As String
            Public ReadOnly CertifiedAddress As String

            Public Sub New(ByVal displayAddress As String, ByVal notificationAddress As String, ByVal certifiedAddress As String)
                Me.DisplayAddress = displayAddress
                Me.NotificationAddress = notificationAddress
                Me.CertifiedAddress = certifiedAddress
            End Sub
        End Class

        Public Class Sender
            Public ReadOnly companyName As String
            Public ReadOnly streetAddress As String
            Public ReadOnly admLvl3 As String
            Public ReadOnly admLvl2 As String
            Public ReadOnly country As String
            Public ReadOnly zipCode As String
            Public ReadOnly vatCode As String
            Public ReadOnly email As Email

            Public Sub New(ByVal companyName As String, ByVal streetAddress As String, ByVal admLvl3 As String, ByVal admLvl2 As String, ByVal country As String, ByVal zipCode As String, ByVal vatCode As String, ByVal email As Email)
                Me.companyName = companyName
                Me.streetAddress = streetAddress
                Me.admLvl3 = admLvl3
                Me.admLvl2 = admLvl2
                Me.country = country
                Me.zipCode = zipCode
                Me.vatCode = vatCode
                Me.email = email
            End Sub

            Public Shared Function CreateSender() As Sender
                Dim email As Email = New Email(Constants.SENDER_DISPLAY_ADDRESS, Constants.SENDER_NOTIFICATION_ADDRESS, Constants.SENDER_CERTIFIED_ADDRESS)
                Return New Sender(Constants.SENDER_COMPANY_NAME, Constants.SENDER_STREET_ADDRESS, Constants.SENDER_ADM_LVL3, Constants.SENDER_ADM_LVL2, Constants.SENDER_COUNTRY, Constants.SENDER_ZIP_CODE, Constants.SENDER_VAT_CODE, email)
            End Function
        End Class

        Public Class PrintOptions
            Public ReadOnly frontBack As Boolean
            Public ReadOnly colorMode As String
            Public ReadOnly sheetFormat As String
            Public ReadOnly weight As Integer?
            Public ReadOnly staple As Boolean
            Public ReadOnly globalStaple As Boolean

            Public Sub New(ByVal frontBack As Boolean, ByVal colorMode As String, ByVal sheetFormat As String, ByVal weight As Integer?, ByVal staple As Boolean, ByVal globalStaple As Boolean)
                Me.frontBack = frontBack
                Me.colorMode = colorMode
                Me.sheetFormat = sheetFormat
                Me.weight = weight
                Me.staple = staple
                Me.globalStaple = globalStaple
            End Sub
        End Class

        Public Class AttachmentOptions
            Public ReadOnly print As PrintOptions

            Public Sub New(ByVal print As PrintOptions)
                Me.print = print
            End Sub

            Public Shared Function CreateAttachmentOptions(ByVal frontBack As Boolean, ByVal colorMode As String, ByVal sheetFormat As String, ByVal weight As Integer, ByVal staple As Boolean, ByVal globalStaple As Boolean) As AttachmentOptions
                Return New AttachmentOptions(New PrintOptions(frontBack, colorMode, sheetFormat, weight, staple, globalStaple))
            End Function
        End Class

        Public Class File
            Public ReadOnly id As String
            Public ReadOnly visibility As String
            Public ReadOnly options As AttachmentOptions

            Public Sub New(ByVal id As String, ByVal visibility As String, ByVal options As AttachmentOptions)
                Me.id = id
                Me.visibility = visibility
                Me.options = options
            End Sub
        End Class

        Public Class Attachments
            Public ReadOnly uploadSessionId As String
            Public ReadOnly files As List(Of File)

            Public Sub New(ByVal uploadSessionId As String, ByVal files As List(Of File))
                Me.uploadSessionId = uploadSessionId
                Me.files = files
            End Sub

            Public Shared Function CreateAttachments(ByVal uploadSessionId As String, ByVal fileList As List(Of File)) As Attachments
                Return New Attachments(uploadSessionId, fileList)
            End Function
        End Class

        Public Class PostalInfoName
            Public ReadOnly type As String
            Public ReadOnly firstname As String
            Public ReadOnly lastname As String
            Public ReadOnly companyName As String

            Public Sub New(ByVal type As String, ByVal firstname As String, ByVal lastname As String, ByVal companyName As String)
                Me.type = type
                Me.firstname = firstname
                Me.lastname = lastname
                Me.companyName = companyName
            End Sub
        End Class

        Public Class Postage
            Public ReadOnly vector As String
            Public ReadOnly type As String

            Public Sub New(ByVal vector As String, ByVal type As String)
                Me.vector = vector
                Me.type = type
            End Sub
        End Class

        Public Class PostalInfo
            Public ReadOnly streetAddress As String
            Public ReadOnly zipCode As String
            Public ReadOnly admLvl3 As String
            Public ReadOnly admLvl2 As String
            Public ReadOnly countryCode As String
            Public ReadOnly postage As Postage
            Public ReadOnly name As PostalInfoName

            Public Sub New(ByVal streetAddress As String, ByVal zipCode As String, ByVal admLvl3 As String, ByVal admLvl2 As String, ByVal countryCode As String, ByVal postage As Postage, ByVal name As PostalInfoName)
                Me.streetAddress = streetAddress
                Me.zipCode = zipCode
                Me.admLvl3 = admLvl3
                Me.admLvl2 = admLvl2
                Me.countryCode = countryCode
                Me.postage = postage
                Me.name = name
            End Sub

            Public Shared Function CreatePostalInfo(ByVal streetAddress As String, ByVal zipCode As String, ByVal admLvl3 As String, ByVal admLvl2 As String, ByVal countryCode As String, ByVal postageVector As String, ByVal postageType As String, ByVal type As String, ByVal firstname As String, ByVal lastname As String, ByVal companyName As String) As PostalInfo
                Dim postage As Postage = If(postageVector IsNot Nothing, New Postage(postageVector, postageType), Nothing)
                Return New PostalInfo(streetAddress, zipCode, admLvl3, admLvl2, countryCode, postage, New PostalInfoName(type, firstname, lastname, companyName))
            End Function
        End Class

        Public Class RecipientAttachment
            Public ReadOnly files As List(Of String)

            Public Sub New(ByVal files As List(Of String))
                Me.files = files
            End Sub
        End Class

        Public Class Carrier
            Public ReadOnly channel As String
            Public ReadOnly alternativeChannel As String

            Public Sub New(ByVal channel As String, ByVal alternativeChannel As String)
                Me.channel = channel
                Me.alternativeChannel = alternativeChannel
            End Sub
        End Class

        Public Class Keyword
            Public ReadOnly placeholder As String
            Public ReadOnly value As String

            Public Sub New(ByVal placeholder As String, ByVal value As String)
                Me.placeholder = placeholder
                Me.value = value
            End Sub
        End Class

        Public Class MessageOptions
            Public ReadOnly keywords As List(Of Keyword)

            Public Sub New(ByVal keywords As List(Of Keyword))
                Me.keywords = keywords
            End Sub
        End Class

        Public Class Recipient
            Public ReadOnly email As String
            Public ReadOnly pec As String
            Public ReadOnly postalInfo As PostalInfo
            Public ReadOnly attachments As RecipientAttachment
            Public ReadOnly carrier As Carrier
            Public ReadOnly messageOptions As MessageOptions
            Public ReadOnly customData As List(Of CustomDataElement)

            Public Sub New(ByVal email As String, ByVal pec As String, ByVal postalInfo As PostalInfo, ByVal attachments As RecipientAttachment, ByVal carrier As Carrier, ByVal messageOptions As MessageOptions, ByVal customData As List(Of CustomDataElement))
                Me.email = email
                Me.pec = pec
                Me.postalInfo = postalInfo
                Me.attachments = attachments
                Me.carrier = carrier
                Me.messageOptions = messageOptions
                Me.customData = customData
            End Sub

            Private Shared Function CreateRecipient(ByVal email As String, ByVal pec As String, ByVal postalInfo As PostalInfo, ByVal attachmentFiles As List(Of String), ByVal channel As String, ByVal alternativeChannel As String, ByVal keywords As List(Of Keyword), ByVal customData As List(Of CustomDataElement)) As Recipient
                Dim messageOptions As MessageOptions = If(keywords IsNot Nothing, New MessageOptions(keywords), Nothing)
                Return New Recipient(email, pec, postalInfo, New RecipientAttachment(attachmentFiles), New Carrier(channel, alternativeChannel), messageOptions, customData)
            End Function

            Public Shared Function CreateRecipient(ByVal streetAddress As String, ByVal zipCode As String, ByVal admLvl3 As String, ByVal admLvl2 As String, ByVal countryCode As String, ByVal postageVector As String, ByVal postageType As String, ByVal type As String, ByVal firstname As String, ByVal lastname As String, ByVal companyName As String, ByVal email As String, ByVal pec As String, ByVal attachmentFiles As List(Of String), ByVal channel As String, ByVal alternativeChannel As String, ByVal keywords As List(Of Keyword)) As Recipient
                Dim postalInfo As PostalInfo = PostalInfo.CreatePostalInfo(streetAddress, zipCode, admLvl3, admLvl2, countryCode, postageVector, postageType, type, firstname, lastname, companyName)
                Return CreateRecipient(email, pec, postalInfo, attachmentFiles, channel, alternativeChannel, keywords, Nothing)
            End Function

            Public Shared Function CreateRecipient(ByVal streetAddress As String, ByVal zipCode As String, ByVal admLvl3 As String, ByVal admLvl2 As String, ByVal countryCode As String, ByVal postageVector As String, ByVal postageType As String, ByVal type As String, ByVal firstname As String, ByVal lastname As String, ByVal companyName As String, ByVal email As String, ByVal pec As String, ByVal attachmentFiles As List(Of String), ByVal channel As String, ByVal alternativeChannel As String, ByVal keywords As List(Of Keyword), ByVal customData As List(Of CustomDataElement)) As Recipient
                Dim postalInfo As PostalInfo = PostalInfo.CreatePostalInfo(streetAddress, zipCode, admLvl3, admLvl2, countryCode, postageVector, postageType, type, firstname, lastname, companyName)
                Return CreateRecipient(email, pec, postalInfo, attachmentFiles, channel, alternativeChannel, keywords, customData)
            End Function
        End Class

        Public Class Message
            Public ReadOnly subject As String
            Public ReadOnly body As String

            Public Sub New(ByVal subject As String, ByVal body As String)
                Me.subject = subject
                Me.body = body
            End Sub
        End Class

        Public Class Billing
            Public ReadOnly invoiceTag As String

            Public Sub New(ByVal invoiceTag As String)
                Me.invoiceTag = invoiceTag
            End Sub
        End Class

        Public Class Multicerta
            Public ReadOnly legalValue As Boolean

            Public Sub New(ByVal legalValue As Boolean)
                Me.legalValue = legalValue
            End Sub
        End Class

        Public Class Deadline
            Public ReadOnly acknowledgement As String
            Public ReadOnly duration As Integer

            Public Sub New(ByVal acknowledgement As String, ByVal duration As Integer)
                Me.acknowledgement = acknowledgement
                Me.duration = duration
            End Sub
        End Class

        Public Class Postal
            Public ReadOnly expedite As Boolean
            Public ReadOnly print As PrintOptions

            Public Sub New(ByVal expedite As Boolean, ByVal print As PrintOptions)
                Me.expedite = expedite
                Me.print = print
            End Sub
        End Class

        Public Class Options
            Public ReadOnly billing As Billing
            Public ReadOnly multicerta As Multicerta
            Public ReadOnly deadline As Deadline
            Public ReadOnly postal As Postal

            Public Sub New(ByVal billing As Billing, ByVal multicerta As Multicerta, ByVal deadline As Deadline, ByVal postal As Postal)
                Me.billing = billing
                Me.multicerta = multicerta
                Me.deadline = deadline
                Me.postal = postal
            End Sub
        End Class

        Public Class CustomDataElement
            Public ReadOnly key As String
            Public ReadOnly value As String
            Public ReadOnly visibility As String

            Public Sub New(ByVal key As String, ByVal value As String, ByVal visibility As String)
                Me.key = key
                Me.value = value
                Me.visibility = visibility
            End Sub

            Public Shared Function CreateCustomData(ByVal key As String, ByVal value As String, ByVal visibility As String) As List(Of CustomDataElement)
                Dim ret As List(Of CustomDataElement) = New List(Of CustomDataElement)()
                ret.Add(New CustomDataElement(key, value, visibility))
                Return ret
            End Function
        End Class

        Public Class PostQueue
            Public ReadOnly type As String
            Public ReadOnly sender As Sender
            Public ReadOnly attachments As Attachments
            Public ReadOnly recipients As List(Of Recipient)
            Public ReadOnly message As Message
            Public ReadOnly options As Options
            Public ReadOnly topic As String
            Public ReadOnly customData As List(Of CustomDataElement)

            Public Sub New(ByVal type As String, ByVal sender As Sender, ByVal attachments As Attachments, ByVal recipients As List(Of Recipient), ByVal message As Message, ByVal options As Options, ByVal topic As String, ByVal customData As List(Of CustomDataElement))
                Me.type = type
                Me.sender = sender
                Me.attachments = attachments
                Me.recipients = recipients
                Me.message = message
                Me.options = options
                Me.topic = topic
                Me.customData = customData
            End Sub

            Public Shared Function CreatePostQueue(ByVal sender As Sender, ByVal attachments As Attachments, ByVal recipients As List(Of Recipient), ByVal subject As String, ByVal body As String, ByVal useMulticerta As Boolean, ByVal useMulticertaLegal As Boolean, ByVal staple As Boolean, ByVal globalStaple As Boolean, ByVal topic As String, ByVal customData As List(Of CustomDataElement)) As PostQueueDto
                Dim message As Message = New Message(subject, body)
                Dim billing As Billing = New Billing("INVOICE_" & topic.Replace(" ", "_").ToUpper())
                Dim multicerta As Multicerta = Nothing
                Dim deadline As Deadline = Nothing

                If useMulticerta Then
                    multicerta = New Multicerta(useMulticertaLegal)
                    deadline = New Deadline("read", 3600)
                End If

                Dim postal As Postal = New Postal(True, New PrintOptions(True, "color", Nothing, Nothing, staple, globalStaple))
                Dim options As Options = New Options(billing, multicerta, deadline, postal)
                Return New PostQueueDto(New Dto.DataPayload(Of PostQueue)(New PostQueue("concrete", sender, attachments, recipients, message, options, topic, customData)))
            End Function
        End Class

        Public Class PostQueueDto
            Public ReadOnly data As Dto.DataPayload(Of PostQueue)

            Public Sub New(ByVal data As Dto.DataPayload(Of PostQueue))
                Me.data = data
            End Sub
        End Class
    End Module    
End Namespace
