Public Class Main_Screen

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            Dim Display_Message1 As New Display_Message()
            Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.Message.ToString
            Display_Message1.Timer1.Interval = 1000
            Display_Message1.ShowDialog()
            Display_Message1.Dispose()
            Display_Message1 = Nothing
            If My.Computer.FileSystem.DirectoryExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs") = False Then
                My.Computer.FileSystem.CreateDirectory((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
            End If
            Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
            filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
            filewriter.Flush()
            filewriter.Close()
            filewriter = Nothing
            ex = Nothing
            identifier_msg = Nothing
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
            If My.Application.CommandLineArgs.Count > 0 Then
                TextBox1.Text = (My.Application.CommandLineArgs.Item(0))
                WebBrowser1.Navigate(My.Application.CommandLineArgs.Item(0))
            End If
        Catch ex As Exception
            Error_Handler(ex, "Application_Load")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            WebBrowser1.Navigate(TextBox1.Text)
        Catch ex As Exception
            Error_Handler(ex, "Navigate")
        End Try
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                WebBrowser1.Navigate(TextBox1.Text)
            End If
        Catch ex As Exception
            Error_Handler(ex, "Navigate")
        End Try
    End Sub
End Class
