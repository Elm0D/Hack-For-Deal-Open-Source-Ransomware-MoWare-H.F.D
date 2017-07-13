Public Class Form2


    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        LinkLabel1.Enabled = False
        If TextBox1.Text.Length = 15 Then
            ProgressBar1.Maximum = ListBox1.Items.Count
            ProgressBar1.Minimum = 0
            ProgressBar1.Value = 0
            For i = ListBox1.Items.Count - 1 To 0 Step -1
                doDecrypt(ListBox1.Items(i).ToString, TextBox1.Text)
                ListBox1.Items.RemoveAt(i)
                ProgressBar1.Value = ProgressBar1.Value + 1
            Next
            ListBox1.ClearSelected()
            LinkLabel1.Enabled = True
            Interaction.MsgBox("Successfully restored !", MsgBoxStyle.Information, "Success")
            End
        Else
            LinkLabel1.Enabled = True
            Interaction.MsgBox("Private key is incorrect !", MsgBoxStyle.Critical, "Error")
        End If
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ddddd
    End Sub
End Class