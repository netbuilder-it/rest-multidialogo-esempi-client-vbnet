Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Text.RegularExpressions

Namespace RestMultidialogoClient
    Module ErrorUtils
        Function ParseDotNotation(ByVal errorMsg As String, ByVal sentObj As Object) As Object
            For Each attributeName As String In errorMsg.Split("."c)

                If sentObj Is Nothing Then
                    Return Nothing
                End If

                Dim propName As String = attributeName

                If IsArrayProperty(attributeName) Then
                    propName = GetPropertyNameFromArrayReference(attributeName)
                End If

                Dim type As Type = sentObj.[GetType]()
                Dim info As FieldInfo = type.GetField(propName)

                If info Is Nothing Then
                    Return Nothing
                End If

                If IsArrayProperty(attributeName) Then
                    Dim index As Integer = GetIndex(attributeName)
                    Dim values As IEnumerable = CType(info.GetValue(sentObj), IEnumerable)
                    sentObj = GetObjectFromList(index, values)
                    Console.WriteLine("Attributo: " & propName & "[" & index & "]")
                Else
                    Console.WriteLine("Attributo: " & propName)
                    sentObj = info.GetValue(sentObj)
                End If
            Next

            Return sentObj
        End Function

        Private Function GetObjectFromList(ByVal index As Integer, ByVal list As IEnumerable) As Object
            Dim i As Integer = 0

            For Each o As Object In list

                If i = index Then
                    Return o
                End If

                i += 1
            Next

            Return Nothing
        End Function

        Private Function GetIndex(ByVal expr As String) As Integer
            Dim pattern As String = "[\d+]"
            Dim ret As Integer = -1
            Dim m As Match = Regex.Match(expr, pattern)

            If m.Success Then
                Int32.TryParse(m.Value, ret)
            End If

            Return ret
        End Function

        Private Function IsArrayProperty(ByVal name As String) As Boolean
            Return name.Contains("[")
        End Function

        Private Function GetPropertyNameFromArrayReference(ByVal arrayReference As String) As String
            Return arrayReference.Substring(0, arrayReference.IndexOf("["))
        End Function
    End Module
End Namespace
