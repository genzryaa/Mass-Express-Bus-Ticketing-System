Imports System.Drawing.Printing

Public Class InfoForm
    Private WithEvents printDoc As New PrintDocument()

    Private Sub InfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "InfoForm - Ticket Details"
        Me.Size = New Size(460, 440)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(238, 229, 145)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Controls.Clear()

        Dim lblHeader As New Label With {
            .Text = "Your ticket details",
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = Color.FromArgb(24, 119, 66),
            .Location = New Point(95, 25),
            .AutoSize = True
        }

        AddDetailRow("Name :", AppData.PassengerName, 95)
        AddDetailRow("Phone No :", AppData.PassengerPhone, 135)
        AddDetailRow("From :", AppData.SelectedOrigin, 175)
        AddDetailRow("To :", AppData.SelectedDestination, 215)
        AddDetailRow("Seat No :", "Seat " & AppData.SelectedSeat.ToString(), 255)
        AddDetailRow("Price :", "RM" & AppData.TicketPrice.ToString("0"), 295)

        Dim btnDone As New Button With {
            .Text = "Done",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(95, 345),
            .Size = New Size(115, 38)
        }
        AddHandler btnDone.Click, Sub()
                                      Me.Close()
                                      MainForm.Show()
                                  End Sub

        Dim btnPrint As New Button With {
            .Text = "Print",
            .BackColor = Color.FromArgb(46, 160, 95),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(235, 345),
            .Size = New Size(115, 38)
        }
        AddHandler btnPrint.Click, AddressOf BtnPrint_Click

        Me.Controls.AddRange({lblHeader, btnDone, btnPrint})
    End Sub

    Private Sub AddDetailRow(title As String, val As String, y As Integer)
        Dim l1 As New Label With {.Text = title, .Font = New Font("Segoe UI", 11, FontStyle.Bold), .Location = New Point(110, y), .AutoSize = True}
        Dim l2 As New Label With {.Text = val, .Font = New Font("Segoe UI", 11), .Location = New Point(235, y), .AutoSize = True}
        Me.Controls.Add(l1)
        Me.Controls.Add(l2)
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Dim preview As New PrintPreviewDialog With {.Document = printDoc, .Width = 600, .Height = 500}
        preview.ShowDialog()
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim fTitle As New Font("Consolas", 16, FontStyle.Bold)
        Dim fBody As New Font("Consolas", 12)
        Dim receipt As String =
            "======================================" & vbCrLf &
            "      MASS EXPRESS - BUS TICKET       " & vbCrLf &
            "======================================" & vbCrLf &
            $"Name        : {AppData.PassengerName}" & vbCrLf &
            $"Phone No    : {AppData.PassengerPhone}" & vbCrLf &
            $"Source      : {AppData.SelectedOrigin}" & vbCrLf &
            $"Destination : {AppData.SelectedDestination}" & vbCrLf &
            $"Travel Date : {AppData.SelectedDate}" & vbCrLf &
            $"Seat No     : {AppData.SelectedSeat}" & vbCrLf &
            $"Price       : RM{AppData.TicketPrice:0.00}" & vbCrLf &
            "======================================" & vbCrLf &
            "   Thank you for riding with us!      "

        e.Graphics.DrawString("CUSTOMER RECEIPT", fTitle, Brushes.Black, 60, 50)
        e.Graphics.DrawString(receipt, fBody, Brushes.Black, 60, 95)
    End Sub
End Class