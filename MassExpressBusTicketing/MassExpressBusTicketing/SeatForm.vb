Public Class SeatForm
    Private seatButtons(6) As Button
    Private txtName As TextBox
    Private txtPhone As TextBox
    Private chosenSeat As Integer = 0

    Private Sub SeatForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Seat Form - Choose Your Seat"
        Me.Size = New Size(820, 480)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(238, 229, 145)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        ' Trip Info Section
        Dim lblTripHeader As New Label With {.Text = "Trip Info", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(45, 25), .AutoSize = True}

        Dim lblFrom As New Label With {.Text = AppData.SelectedOrigin, .BackColor = Color.Silver, .Font = New Font("Segoe UI", 13, FontStyle.Bold), .Location = New Point(45, 60), .Size = New Size(130, 32), .TextAlign = ContentAlignment.MiddleCenter}
        Dim lblTo As New Label With {.Text = AppData.SelectedDestination, .BackColor = Color.Silver, .Font = New Font("Segoe UI", 13, FontStyle.Bold), .Location = New Point(200, 60), .Size = New Size(150, 32), .TextAlign = ContentAlignment.MiddleCenter}
        Dim lblDate As New Label With {.Text = AppData.SelectedDate, .BackColor = Color.Silver, .Font = New Font("Segoe UI", 13, FontStyle.Bold), .Location = New Point(375, 60), .Size = New Size(140, 32), .TextAlign = ContentAlignment.MiddleCenter}

        ' Passenger Information Section
        Dim lblPassHeader As New Label With {.Text = "Information Passenger :", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(45, 165), .AutoSize = True}

        Dim lblName As New Label With {.Text = "Name", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 215), .AutoSize = True}
        txtName = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(150, 212), .Width = 280}

        Dim lblPhone As New Label With {.Text = "Phone No", .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(45, 265), .AutoSize = True}
        Dim defaultPhone As String = If(AppData.Users.ContainsKey(AppData.CurrentUser), AppData.Users(AppData.CurrentUser).Item2, "")
        txtPhone = New TextBox With {.Font = New Font("Segoe UI", 12), .Location = New Point(150, 262), .Width = 180}

        ' Right Side: Seat Map (1 to 6 matching report)
        Dim lblChoose As New Label With {.Text = "Choose your seat", .Font = New Font("Segoe UI", 13, FontStyle.Bold), .Location = New Point(575, 55), .AutoSize = True}
        Dim lblLegend As New Label With {.Text = "Red = Booked  |  White = Available  |  Green = Selected", .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold), .Location = New Point(510, 85), .AutoSize = True}

        For i As Integer = 1 To 6
            Dim col As Integer = (i - 1) Mod 2
            Dim row As Integer = (i - 1) \ 2
            Dim xPos As Integer = 560 + (col * 130)
            Dim yPos As Integer = 120 + (row * 75)

            Dim numLbl As New Label With {.Text = i.ToString(), .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(xPos - 25, yPos + 15), .AutoSize = True}
            Dim btnSeat As New Button With {.Location = New Point(xPos, yPos), .Size = New Size(55, 50), .Tag = i, .FlatStyle = FlatStyle.Flat}

            If AppData.IsSeatBooked(AppData.SelectedOrigin, AppData.SelectedDestination, AppData.SelectedDate, i) Then
                btnSeat.BackColor = Color.Red
                btnSeat.Enabled = False
            Else
                btnSeat.BackColor = Color.White
                AddHandler btnSeat.Click, AddressOf Seat_Click
            End If

            seatButtons(i) = btnSeat
            Me.Controls.Add(numLbl)
            Me.Controls.Add(btnSeat)
        Next

        ' Bottom Action Buttons
        Dim btnBack As New Button With {
            .Text = "Back",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(525, 370),
            .Size = New Size(115, 42)
        }
        AddHandler btnBack.Click, Sub()
                                      Me.Close()
                                      MainForm.Show()
                                  End Sub

        Dim btnBook As New Button With {
            .Text = "Book Ticket",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(655, 370),
            .Size = New Size(125, 42)
        }
        AddHandler btnBook.Click, AddressOf BtnBook_Click

        Me.Controls.AddRange({lblTripHeader, lblFrom, lblTo, lblDate, lblPassHeader, lblName, txtName, lblPhone, txtPhone, lblChoose, lblLegend, btnBack, btnBook})
    End Sub

    Private Sub Seat_Click(sender As Object, e As EventArgs)
        Dim clicked As Button = DirectCast(sender, Button)
        Dim seatNum As Integer = CInt(clicked.Tag)

        For i As Integer = 1 To 6
            If seatButtons(i).Enabled Then
                seatButtons(i).BackColor = Color.White
            End If
        Next

        clicked.BackColor = Color.LimeGreen
        chosenSeat = seatNum
    End Sub

    Private Sub BtnBook_Click(sender As Object, e As EventArgs)
        If chosenSeat = 0 Then
            MessageBox.Show("Please choose an available seat first!", "No Seat Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If txtName.Text.Trim() = "" OrElse txtPhone.Text.Trim() = "" Then
            MessageBox.Show("Please enter Passenger Name and Phone No!", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        AppData.SelectedSeat = chosenSeat
        AppData.PassengerName = txtName.Text.Trim()
        AppData.PassengerPhone = txtPhone.Text.Trim()
        AppData.MarkSeatBooked(AppData.SelectedOrigin, AppData.SelectedDestination, AppData.SelectedDate, chosenSeat)

        Me.Hide()
        Dim infoF As New InfoForm()
        infoF.Show()
    End Sub
End Class