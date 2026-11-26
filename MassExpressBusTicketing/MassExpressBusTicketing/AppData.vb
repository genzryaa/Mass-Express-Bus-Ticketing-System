Public Module AppData
    ' Registered users: Username -> (Password, Phone)
    Public Users As New Dictionary(Of String, Tuple(Of String, String))(StringComparer.OrdinalIgnoreCase) From {
        {"shadam", Tuple.Create("1234", "012345678")},
        {"admin", Tuple.Create("admin", "0111222333")}
    }

    Public CurrentUser As String = ""

    ' Current active trip details
    Public SelectedOrigin As String = "TBS"
    Public SelectedDestination As String = "Kota Bharu"
    Public SelectedDate As String = "20/12/2023"
    Public SelectedSeat As Integer = 0
    Public TicketPrice As Decimal = 45D

    Public PassengerName As String = ""
    Public PassengerPhone As String = ""

    ' Pre-booked seats matching your report screenshot (Seats 1, 4, 5 booked)
    Public BookedSeats As New HashSet(Of String) From {
        "TBS|Kota Bharu|20/12/2023|1",
        "TBS|Kota Bharu|20/12/2023|4",
        "TBS|Kota Bharu|20/12/2023|5"
    }

    Public Function GetRoutePrice(origin As String, dest As String) As Decimal
        If (origin = "TBS" AndAlso dest = "Kota Bharu") OrElse (origin = "Kota Bharu" AndAlso dest = "TBS") Then
            Return 45D
        ElseIf origin = "TBS" AndAlso dest = "Kuantan" Then
            Return 30D
        ElseIf origin = "TBS" AndAlso dest = "Penang" Then
            Return 40D
        ElseIf origin = "TBS" AndAlso dest = "Johor Bahru" Then
            Return 38D
        Else
            Return 35D
        End If
    End Function

    Public Function IsSeatBooked(origin As String, dest As String, travelDate As String, seatNo As Integer) As Boolean
        Return BookedSeats.Contains($"{origin}|{dest}|{travelDate}|{seatNo}")
    End Function

    Public Sub MarkSeatBooked(origin As String, dest As String, travelDate As String, seatNo As Integer)
        BookedSeats.Add($"{origin}|{dest}|{travelDate}|{seatNo}")
    End Sub
End Module