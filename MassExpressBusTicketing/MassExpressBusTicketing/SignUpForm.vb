Public Class SignUpForm
    Private txtUser As TextBox
    Private txtPhone As TextBox
    Private txtPass As TextBox

    Private Sub SignUpForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "SignUpForm"
        Me.Size = New Size(720, 450)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        Dim leftPanel As New Panel With {.Dock = DockStyle.Left, .Width = 260, .BackColor = Color.White}
        Dim brandTitle As New Label With {
            .Text = "MASS" & vbCrLf & "EXPRESS",
            .Font = New Font("Segoe UI", 26, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Dock = DockStyle.Fill
        }
        leftPanel.Controls.Add(brandTitle)

        Dim rightPanel As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(238, 229, 145)}

        Dim lblTop As New Label With {
            .Text = "Create an account",
            .Font = New Font("Segoe UI", 16, FontStyle.Italic),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(110, 20),
            .AutoSize = True
        }

        Dim lblSub As New Label With {
            .Text = "Let's fill in your details",
            .Font = New Font("Segoe UI", 17, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(45, 65),
            .AutoSize = True
        }

        Dim lblU As New Label With {.Text = "User Name", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 115), .AutoSize = True}
        txtUser = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(45, 140), .Width = 330}

        Dim lblPh As New Label With {.Text = "No Phone", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 185), .AutoSize = True}
        txtPhone = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(45, 210), .Width = 330}

        Dim lblP As New Label With {.Text = "Password", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 255), .AutoSize = True}
        txtPass = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(45, 280), .Width = 330, .PasswordChar = "●"c}

        Dim btnSignUp As New Button With {
            .Text = "Sign Up",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(75, 340),
            .Size = New Size(130, 38)
        }
        AddHandler btnSignUp.Click, AddressOf BtnSignUp_Click

        Dim btnExit As New Button With {
            .Text = "Exit",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(235, 340),
            .Size = New Size(130, 38)
        }
        AddHandler btnExit.Click, Sub()
                                      Me.Close()
                                      LoginForm1.Show()
                                  End Sub

        rightPanel.Controls.AddRange({lblTop, lblSub, lblU, txtUser, lblPh, txtPhone, lblP, txtPass, btnSignUp, btnExit})
        Me.Controls.Add(rightPanel)
        Me.Controls.Add(leftPanel)
    End Sub

    Private Sub BtnSignUp_Click(sender As Object, e As EventArgs)
        Dim u As String = txtUser.Text.Trim()
        Dim ph As String = txtPhone.Text.Trim()
        Dim p As String = txtPass.Text

        If u = "" OrElse ph = "" OrElse p = "" Then
            MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If AppData.Users.ContainsKey(u) Then
            MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        AppData.Users.Add(u, Tuple.Create(p, ph))
        MessageBox.Show("Account created successfully! Please log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
        LoginForm1.Show()
    End Sub
End Class