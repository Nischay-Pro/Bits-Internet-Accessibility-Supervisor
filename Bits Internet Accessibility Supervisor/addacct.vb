Imports Newtonsoft.Json.Linq

Public Class addacct
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If MetroTextBox1.Text <> "" And MetroTextBox2.Text <> "" Then
            If settings.ListView1.FindItemWithText(MetroTextBox1.Text) IsNot Nothing Then
                MsgBox("Account already exists. If you are want to edit the account please remove and add again.", MsgBoxStyle.Exclamation, "Duplicate Account")
            Else
                MetroTextBox1.Enabled = False
                MetroTextBox2.Enabled = False
                Dim JOb As New JObject
                JOb.Add("username", MetroTextBox1.Text)
                JOb.Add("password", MetroTextBox2.Text)
                JOb.Add("status", "Not Yet Verified")
                settings.AddData(JOb)
                Dim loadsets As New Threading.Thread(AddressOf settings.LoadAccountsMan)
                loadsets.IsBackground = False
                loadsets.SetApartmentState(Threading.ApartmentState.STA)
                loadsets.Start()
                Me.Close()
            End If
        Else
                MsgBox("Please fill up all credentials before adding.", MsgBoxStyle.Exclamation, "Credentials Missing")
        End If
    End Sub
End Class