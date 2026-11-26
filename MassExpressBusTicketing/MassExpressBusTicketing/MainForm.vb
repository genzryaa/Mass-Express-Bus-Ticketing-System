Public Class MainForm
    Private cmbFrom As ComboBox
    Private cmbTo As ComboBox
    Private dtpDate As DateTimePicker

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Main Form - Mass Express"
        Me.Size = New Size(780, 440)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        ' Top Navigation Strip
        Dim menuStrip As New MenuStrip With {.BackColor = Color.White}
        Dim mHome As New ToolStripMenuItem("Home")
        Dim mAbout As New ToolStripMenuItem("About")
        Dim mSetting As New ToolStripMenuItem("Setting") With {.Alignment = ToolStripItemAlignment.Right}
        Dim mLogout As New ToolStripMenuItem("Logout") With {.Alignment = ToolStripItemAlignment.Right}

        AddHandler mAbout.Click, Sub() MessageBox.Show("MASS EXPRESS Online Bus Ticketing System" & vbCrLf & "DDWD 2653 Visual Basic.NET Programming", "About")
        AddHandler mSetting.Click, Sub()
                                       Me.Hide()
                                       Dim pf As New ProfileForm()
                                       pf.Show()
                                   End Sub
        AddHandler mLogout.Click, Sub()
                                      Me.Close()
                                      LoginForm1.Show()
                                  End Sub

        menuStrip.Items.AddRange({mHome, mAbout, mLogout, mSetting})

        ' Top Banner
        Dim banner As New Panel With {.Dock = DockStyle.Top, .Height = 130, .BackColor = Color.White}
        Dim lblTitle As New Label With {
            .Text = "Bus Tickets",
            .Font = New Font("Segoe UI", 32, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        banner.Controls.Add(lblTitle)

        ' Bottom Search Body
        Dim body As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(238, 229, 145)}

        Dim stations() As String = {"TBS", "Kota Bharu", "Kuantan", "Penang", "Johor Bahru"}

        Dim lblFrom As New Label With {.Text = "Origin (From)", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(55, 40), .AutoSize = True}
        cmbFrom = New ComboBox With {.Font = New Font("Segoe UI", 12), .DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Point(55, 65), .Width = 200}
        cmbFrom.Items.AddRange(stations)
        cmbFrom.SelectedItem = "TBS"

        Dim lblTo As New Label With {.Text = "Destination (To)", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(285, 40), .AutoSize = True}
        cmbTo = New ComboBox With {.Font = New Font("Segoe UI", 12), .DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Point(285, 65), .Width = 200}
        cmbTo.Items.AddRange(stations)
        cmbTo.SelectedItem = "Kota Bharu"

        Dim lblDate As New Label With {.Text = "Travel Date", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(515, 40), .AutoSize = True}
        dtpDate = New DateTimePicker With {.Font = New Font("Segoe UI", 12), .Format = DateTimePickerFormat.Short, .Location = New Point(515, 65), .Width = 190}

        Dim btnSearch As New Button With {
            .Text = "Search Bus",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(215, 145),
            .Size = New Size(330, 45)
        }
        AddHandler btnSearch.Click, AddressOf BtnSearch_Click

        body.Controls.AddRange({lblFrom, cmbFrom, lblTo, cmbTo, lblDate, dtpDate, btnSearch})
        Me.Controls.Add(body)
        Me.Controls.Add(banner)
        Me.Controls.Add(menuStrip)
        Me.MainMenuStrip = menuStrip
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs)
        If cmbFrom.SelectedItem.ToString() = cmbTo.SelectedItem.ToString() Then
            MessageBox.Show("Origin and Destination cannot be the same!", "Invalid Route", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        AppData.SelectedOrigin = cmbFrom.SelectedItem.ToString()
        AppData.SelectedDestination = cmbTo.SelectedItem.ToString()
        AppData.SelectedDate = dtpDate.Value.ToString("dd/MM/yyyy")
        AppData.TicketPrice = AppData.GetRoutePrice(AppData.SelectedOrigin, AppData.SelectedDestination)
        AppData.SelectedSeat = 0

        Me.Hide()
        Dim sf As New SeatForm()
        sf.Show()
    End Sub
End Class