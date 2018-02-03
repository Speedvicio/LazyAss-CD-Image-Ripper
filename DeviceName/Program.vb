
Imports System.Management

Class Program
    Friend Shared Sub Main(args As String())

        Try
            Dim searcher As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_CDROMDrive")
            If System.IO.File.Exists("..\test.txt") Then System.IO.File.Delete("..\DeviceName")

            For Each queryObj As ManagementObject In searcher.[Get]()
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter("..\DeviceName", True)
                file.WriteLine(queryObj("Id") & " " & queryObj("Name"))
                file.Close()
            Next
        Catch e As ManagementException
            MsgBox("An error occurred while querying for WMI data: " & e.Message)
        End Try

    End Sub

End Class
