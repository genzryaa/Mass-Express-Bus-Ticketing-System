Public Class ProfileForm
    Private txtResetUser, txtOldPass, txtNewPass As TextBox
    Private txtDelUser, txtDelPass As TextBox

    Private Sub ProfileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "ProfileForm"
        Me.Size = New Size(780, 420)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(238, 229, 145)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        ' Left: Reset Password
        Dim lblResetTitle As New Label With {.Text = "Reset Password Form", .Font = New Font("Segoe UI", 16, FontStyle.Bold), .ForeColor = Color.FromArgb(24, 119, 66), .Location = New Point(75, 25), .AutoSize = True}
        Dim lblU1 As New Label With {.Text = "Username", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(45, 95), .AutoSize = True}
        txtResetUser = New TextBox With {.Location = New Point(165, 92), .Width = 180}

        Dim lblOld As New Label With {.Text = "Old Password", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(45, 145), .AutoSize = True}
        txtOldPass = New TextBox With {.Location = New Point(165, 142), .Width = 180, .PasswordChar = "●"c}

        Dim lblNew As New Label With {.Text = "New Password", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(45, 195), .AutoSize = True}
        txtNewPass = New TextBox With {.Location = New Point(165, 192), .Width = 180, .PasswordChar = "●"c}

        Dim btnReset As New Button With {.Text = "Reset Password", .BackColor = Color.FromArgb(46, 160, 95), .ForeColor = Color.White, .Font = New Font("Segoe UI", 10, FontStyle.Bold), .FlatStyle = FlatStyle.Flat, .Location = New Point(145, 255), .Size = New Size(140, 38)}
        AddHandler btnReset.Click, AddressOf BtnReset_Click

        ' Right: Delete Account
        Dim lblDelTitle As New Label With {.Text = "Delete Account Form", .Font = New Font("Segoe UI", 16, FontStyle.Bold), .ForeColor = Color.FromArgb(24, 119, 66), .Location = New Point(460, 25), .AutoSize = True}
        Dim lblU2 As New Label With {.Text = "Username", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(425, 105), .AutoSize = True}
        txtDelUser = New TextBox With {.Location = New Point(535, 102), .Width = 180}

        Dim lblP2 As New Label With {.Text = "Password", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(425, 165), .AutoSize = True}
        txtDelPass = New TextBox With {.Location = New Point(535, 162), .Width = 180, .PasswordChar = "●"c}

        Dim btnDel As New Button With {.Text = "Delete Account", .BackColor = Color.FromArgb(46, 160, 95), .ForeColor = Color.White, .Font = New Font("Segoe UI", 10, FontStyle.Bold), .FlatStyle = FlatStyle.Flat, .Location = New Point(515, 255), .Size = New Size(140, 38)}
        AddHandler btnDel.Click, AddressOf BtnDel_Click

        Dim btnBack As New Button With {.Text = "Back to Main", .BackColor = Color.DimGray, .ForeColor = Color.White, .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .FlatStyle = FlatStyle.Flat, .Location = New Point(320, 325), .Size = New Size(130, 34)}
        AddHandler btnBack.Click, Sub()
                                      Me.Close()
                                      MainForm.Show()
                                  End Sub

        Me.Controls.AddRange({lblResetTitle, lblU1, txtResetUser, lblOld, txtOldPass, lblNew, txtNewPass, btnReset, lblDelTitle, lblU2, txtDelUser, lblP2, txtDelPass, btnDel, btnBack})
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs)
        Dim u As String = txtResetUser.Text.Trim()
        If AppData.Users.ContainsKey(u) AndAlso AppData.Users(u).Item1 = txtOldPass.Text AndAlso txtNewPass.Text <> "" Then
            Dim phone As String = AppData.Users(u).Item2
            AppData.Users(u) = Tuple.Create(txtNewPass.Text, phone)
            MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Invalid username or old password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnDel_Click(sender As Object, e As EventArgs)
        Dim u As String = txtDelUser.Text.Trim()
        If AppData.Users.ContainsKey(u) AndAlso AppData.Users(u).Item1 = txtDelPass.Text Then
            AppData.Users.Remove(u)
            MessageBox.Show("Account deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            LoginForm1.Show()
        Else
            MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class