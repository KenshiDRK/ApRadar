Imports System.Security.Cryptography

Public Class Encrypt
#Region " MEMBER VARAIBLES "
    Private _encryptionKey As String
    Private _type As EncryptionType
#End Region

#Region " ENUM "
    Public Enum EncryptionType
        Rijndael
        TripleDES
    End Enum
#End Region

#Region " PROPERTIES "
    Private Shared ReadOnly Property Provider() As RijndaelManaged
        Get
            Return New RijndaelManaged
        End Get
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New(ByVal Type As EncryptionType)
        _encryptionKey = "{8ED9D3B8-CEC5-11DE-8ACD-7D7B55D89593}"
        _type = Type
    End Sub

    Public Sub New(ByVal Type As EncryptionType, ByVal EnctyptionKey As String)
        _encryptionKey = EnctyptionKey
        _type = Type
    End Sub
#End Region

#Region " PRIVATE MEMBERS "
    ''' <summary>
    ''' Creates the encryption key to be used for encrypting files
    ''' </summary>
    ''' <returns>32 Byte encryption key</returns>
    ''' <remarks></remarks>
    Private Function CreateKey() As Byte()
        Try
            'Convert strPassword to an array and store in chrData.
            Dim chrData() As Char = _encryptionKey.ToCharArray
            'Use intLength to get strPassword size.
            Dim intLength As Integer = chrData.GetUpperBound(0)
            'Declare bytDataToHash and make it the same size as chrData.
            Dim bytDataToHash(intLength) As Byte
            'Use For Next to convert and store chrData into bytDataToHash.
            For i As Integer = 0 To chrData.GetUpperBound(0)
                bytDataToHash(i) = CByte(Asc(chrData(i)))
            Next
            'Declare what hash to use.
            Using SHA512 As New SHA512Managed()
                'Declare bytResult, Hash bytDataToHash and store it in bytResult.
                Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
                'Declare bytKey(31).  It will hold 256 bits.
                Dim bytKey(15) As Byte
                For i As Integer = 24 To 39
                    bytKey(i - 24) = bytResult(i)
                Next
                Return bytKey
            End Using 'Return the key.
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates the IV key to be used for encrypting files
    ''' </summary>
    ''' <returns>8 byte IV key</returns>
    ''' <remarks></remarks>
    Private Function CreateIV() As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = _encryptionKey.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte
        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next
        'Declare what hash to use.
        Using SHA512 As New SHA512Managed()
            'Declare bytResult, Hash bytDataToHash and store it in bytResult.
            Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
            'Declare bytIV(15).  It will hold 128 bits.
            Dim bytIV(15) As Byte
            'Use For Next to put a specific size (128 bits) of bytResult into bytIV.
            'The 0 To 30 for bytKey used the first 256 bits of the hashed password.
            'The 32 To 47 will put the next 128 bits into bytIV.
            For i As Integer = 40 To 55
                bytIV(i - 40) = bytResult(i)
            Next
            Return bytIV
        End Using 'Return the IV.
    End Function
#End Region

#Region " PUBLIC FUNCTIONS "
    ''' <summary>
    ''' Encrypts the provided string
    ''' </summary>
    ''' <param name="Data">The data to encrypt</param>
    ''' <returns>An encrypted string representation of the specified data</returns>
    ''' <remarks></remarks>
    Public Function EncryptData(ByVal Data As String) As String
        Dim IV() As Byte = CreateIV()
        Try
            Dim bykey() As Byte = CreateKey()
            Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(Data)

            Using ms As New IO.MemoryStream()
                Dim cs As New CryptoStream(ms, Provider.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
                cs.Write(InputByteArray, 0, InputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            End Using
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Encrypts an array of bytes
    ''' </summary>
    ''' <param name="Data">the byte array to encrypt</param>
    ''' <returns>The encrypted byte array</returns>
    ''' <remarks></remarks>
    Public Function EncryptData(ByVal Data As Byte()) As Byte()
        Dim IV() As Byte = CreateIV()
        Try
            Dim bykey() As Byte = CreateKey()

            Using ms As New IO.MemoryStream()
                Dim cs As New CryptoStream(ms, Provider.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
                cs.Write(Data, 0, Data.Length)
                cs.FlushFinalBlock()
                Return ms.ToArray()
            End Using

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Decrypts an encrypted string
    ''' </summary>
    ''' <param name="Data">the encrypted string for decryption</param>
    ''' <returns>The unencrypted string</returns>
    ''' <remarks></remarks>
    Public Function DecryptData(ByVal Data As String) As String
        Dim IV() As Byte = CreateIV()
        Try
            Dim byKey() As Byte = CreateKey()

            Dim inputByteArray As Byte() = Convert.FromBase64String(Data)
            Using ms As New IO.MemoryStream()
                Dim cs As New CryptoStream(ms, Provider.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            End Using

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Decrypts an array of encrypted bytes
    ''' </summary>
    ''' <param name="Data">The encrypted byte array</param>
    ''' <returns>The decrypted </returns>
    ''' <remarks></remarks>
    Public Function DecryptData(ByVal Data As Byte()) As Byte()
        Dim IV() As Byte = CreateIV()
        Try
            Dim byKey() As Byte = CreateKey()

            Using ms As New IO.MemoryStream()
                Dim cs As New CryptoStream(ms, Provider.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
                cs.Write(Data, 0, Data.Length)
                cs.FlushFinalBlock()
                Return ms.ToArray()
            End Using

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
End Class
