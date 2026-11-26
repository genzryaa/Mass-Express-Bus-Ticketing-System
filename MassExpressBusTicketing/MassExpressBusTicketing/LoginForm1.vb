Public Class LoginForm1
    Private txtUser As TextBox
    Private txtPass As TextBox

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "MASS EXPRESS - Login"
        Me.Size = New Size(720, 440)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        ' Left White Brand Panel
        Dim leftPanel As New Panel With {.Dock = DockStyle.Left, .Width = 260, .BackColor = Color.White}
        Dim brandTitle As New Label With {
            .Text = "MASS" & vbCrLf & "EXPRESS",
            .Font = New Font("Segoe UI", 26, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Dock = DockStyle.Fill
        }
        leftPanel.Controls.Add(brandTitle)

        ' Right Warm Cream Form Panel
        Dim rightPanel As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(238, 229, 145)}

        Dim lblWelcome As New Label With {
            .Text = "Welcome Back,",
            .Font = New Font("Segoe UI", 16, FontStyle.Italic Or FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(45, 30),
            .AutoSize = True
        }

        Dim lblSub As New Label With {
            .Text = "LOGIN FORM",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(45, 68),
            .AutoSize = True
        }

        Dim lblUser As New Label With {.Text = "User Name", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 125), .AutoSize = True}
        txtUser = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(45, 150), .Width = 330}

        Dim lblPass As New Label With {.Text = "Password", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 195), .AutoSize = True}
        txtPass = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(45, 220), .Width = 330, .PasswordChar = "●"c}

        Dim btnLogin As New Button With {
            .Text = "Log in",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(45, 275),
            .Size = New Size(150, 40)
        }
        AddHandler btnLogin.Click, AddressOf BtnLogin_Click

        Dim btnExit As New Button With {
            .Text = "Exit",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(225, 275),
            .Size = New Size(150, 40)
        }
        AddHandler btnExit.Click, Sub() Application.Exit()

        Dim lnkSignUp As New LinkLabel With {
            .Text = "New Passengers? Sign Up",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .LinkColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(125, 335),
            .AutoSize = True
        }
        AddHandler lnkSignUp.LinkClicked, Sub()
                                              Me.Hide()
                                              Dim f As New SignUpForm()
                                              f.Show()
                                          End Sub

        rightPanel.Controls.AddRange({lblWelcome, lblSub, lblUser, txtUser, lblPass, txtPass, btnLogin, btnExit, lnkSignUp})
        Me.Controls.Add(rightPanel)
        Me.Controls.Add(leftPanel)
    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs)
        Dim u As String = txtUser.Text.Trim()
        Dim p As String = txtPass.Text

        If AppData.Users.ContainsKey(u) AndAlso AppData.Users(u).Item1 = p Then
            AppData.CurrentUser = u
            Me.Hide()
            Dim mainF As New MainForm()
            mainF.Show()
        Else
            MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class