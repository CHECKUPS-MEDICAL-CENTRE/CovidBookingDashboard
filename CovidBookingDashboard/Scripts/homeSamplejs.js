var table = [];
var newC = "";
var StartDate;
var EndDate;

$(document).ready(function () {
    var endDate= $('#EndDate').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    var startDate= $('#StartDate').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#EndDate1').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#StartDate1').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#EndDate2').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#StartDate2').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#EndDate3').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    $('#StartDate3').datepicker({
        setDate: new Date(),
        format: 'mm-dd-yyyy',
        autoclose: true
    }).datepicker("setDate", 'now');
    fetchNewBookings();
    FetchBookingCountToday();
    FetchBookingCountHomesampletoday();
    FetchBookingCountwalkinstoday();
    FetchBookingCountThisMonth();
    FetchScheduled();
    setInterval(function () {
        /*   $('#patientsTables').load(fetchBookings());*/
        $('newClients').load(FetchBookingCountToday());
        $('HomeSample').load(FetchBookingCountHomesampletoday());
        $('WalkIns').load(FetchBookingCountwalkinstoday());
        $('MonthlyClients').load(FetchBookingCountThisMonth());
    }, 3600)
    $('scheduled').load(FetchScheduled());
    var status = "1";
    fetchNewBookings(startDate,endDate,status);
});
$('#filterByDate').click(function () {
    FetchVisits();
});
function FetchVisits() {
    StartDate = $('#StartDate').val();
    EndDate = $('#EndDate').val();
    Status = $('#status').val();
    fetchNewBookings(StartDate, EndDate, Status);
}
$('#filterByDate1').click(function () {
    FetchScheduled();
});
function FetchScheduled() {
    StartDate = $('#StartDate1').val();
    EndDate = $('#EndDate1').val();
    Status = $('#status1').val();
    fetchScheduledBookings(StartDate, EndDate, Status);
}
$('#filterByDate2').click(function () {
    FetchPending();
});
function FetchPending() {
    StartDate = $('#StartDate2').val();
    EndDate = $('#EndDate2').val();
    Status = $('#status2').val();
    fetchPendingBookings(StartDate, EndDate, Status);
}
$('#filterByDate3').click(function () {
    FetchCompleted();
});
function FetchCompleted() {
    StartDate = $('#StartDate3').val();
    EndDate = $('#EndDate3').val();
    Status = $('#status3').val();
    fetchCompletedBookings(StartDate, EndDate,Status);
}
function fetchNewBookings(FromDate, ToDate, Status) {
    var bookings;
    var url = '/Home/getVisits?startDate=' + FromDate + '&endDate=' + ToDate + '&status=' + Status;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            dataBookings = res;
            bookings = res;
            populateBookings();
        }
    });
    return bookings;
}
function fetchScheduledBookings(FromDate, ToDate, Status) {
    var schedules;
    var url = '/Home/getVisits?startDate=' + FromDate + '&endDate=' + ToDate + '&status=' + Status;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            schedules = res;
            scheduled = res;
            populateScheuled();
        }
    });
    return schedules;
}
function fetchPendingBookings(FromDate, ToDate, Status) {
    var pendings;
    var url = '/Home/getVisits?startDate=' + FromDate + '&endDate=' + ToDate + '&status=' + Status;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            pendings = res;
            pendingResult = res;
            populatePendigResults();
        }
    });
    return pendings;
}
function fetchCompletedBookings(FromDate, ToDate, Status) {
    var completes;
    var url = '/Home/getVisits?startDate=' + FromDate + '&endDate=' + ToDate + '&status=' + Status;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            completes = res;
            Completed = res;
            populateCompleted();
        }
    });
    return completes;
}
function FetchBookingCountToday() {
    var counts;
    $.ajax({
        type: 'GET',
        url: '/Home/FetchBookingCountToday',
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            console.log(res);
            document.getElementById("newClients").innerHTML = res;
        }
    });
    return counts;
}
function FetchBookingCountHomesampletoday() {
    var counts;
    $.ajax({
        type: 'GET',
        url: '/Home/FetchBookingCountHomesampletoday',
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            document.getElementById("HomeSample").innerHTML = res;
        }
    });
    return counts;
}
function FetchBookingCountwalkinstoday() {
    var counts;
    $.ajax({
        type: 'GET',
        url: '/Home/FetchBookingCountwalkinstoday',
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            document.getElementById("WalkIns").innerHTML = res;
        }
    });
    return counts;
}
function FetchBookingCountThisMonth() {
    var counts;
    $.ajax({
        type: 'GET',
        url: '/Home/FetchBookingCountThisMonth',
        dataType: 'JSON',
        async: false,
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            document.getElementById("MonthlyClients").innerHTML = res;
        }
    });
    return counts;
}

function populateBookings() {
    var dt;
    $('#patientsTables').DataTable({
        data: dataBookings,
        destroy: true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ],
        columns: [
            {
                title: "Actions", data: null, render: function (data, type, row) {
                    if (data.status === 0) {
                        if (type === 'display') {
                            dt = '<div class="dropdown ">'
                                + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                                + '<span class="caret"></span></button>'
                                + '<ul class="dropdown-menu">'
                                + '<li><a class="btn btn-primary " onclick="updateBooking(\'' + data.CycleId + '\',\'' + data.status + '\')">CONFIRM</a></li>'
                                + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                                + '</ul>'
                                + '</div>';

                        }
                    

                    }
                    return dt
                },
                className: "dt-body-center"
            },
            { title: "CycleId", data: "CycleId" },
            { title: "Client Name", data: "FirstName" },
            { title: "PhoneNumber", data: "Telephone" },
            { title: "Visit Date", data: "VisitDate" },
            { title: "Booking Date", data: "DateCreated" },
            { title: "Booking Time", data: "VisitTime" },
            {
                title: "Status", data: null, render: function (data, type, row) {
                        return '<span class="label label-danger">New</span>';
                    }                   
                
            },

        ],
        "aaSorting": [[4, "desc"], [5, "desc"]]
    });
}
function populateScheuled() {
    var dt;
    $('#scheduledTables').DataTable({
        data: scheduled,
        destroy: true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ],
        columns: [
            {
                title: "Actions", data: null, render: function (data, type, row) {
                     if (data.status === 1) {
                        if (type === 'display') {
                            dt == '<div class="dropdown ">'
                                + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                                + '<span class="caret"></span></button>'
                                + '<ul class="dropdown-menu">'
                                + '<li><a class="btn btn-primary " onclick="updateBooking(\'' + data.CycleId + '\',\'' + data.status + '\')">Scheduled</a></li>'
                                + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                                + '</ul>'
                                + '</div>';
                        }
                    }
                    else if (data.status === 1) {
                        if (type === 'display') {
                            dt = '<div class="dropdown ">'
                                + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                                + '<span class="caret"></span></button>'
                                + '<ul class="dropdown-menu">'
                                + '<li><button type="button" class="btn btn-lg btn-primary" disabled>Scheduled</button></li>'
                                + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                                + '</ul>'
                                + '</div>';

                        }
                    }                  
                    return dt
                },
                className: "dt-body-center"
            },
            { title: "CycleId", data: "CycleId" },
            { title: "Client Name", data: "FirstName" },
            { title: "PhoneNumber", data: "Telephone" },
            { title: "Visit Date", data: "VisitDate" },
            { title: "Booking Date", data: "DateCreated" },
            { title: "Booking Time", data: "VisitTime" },
            {
                title: "Status", data: null, render: function (data, type, row) {
 
                        return '<span class="label label-default">Pending Sample Collection</span>';
                    }

            },

        ],
        "aaSorting": [[4, "desc"], [5, "desc"]]
    });
}
function populatePendigResults() {
    var dt;
    $('#pendingResultsTables').DataTable({
        data: scheduled,
        destroy: true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ],
        columns: [
            {
                title: "Actions", data: null, render: function (data, type, row) {
                    if (data.status === 2) {
                        if (type === 'display') {
                            dt = '<div class="dropdown ">'
                                + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                                + '<span class="caret"></span></button>'
                                + '<ul class="dropdown-menu">'
                                + '<li><button type="button" class="btn btn-lg btn-primary" disabled>Collected</button></li>'
                                + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                                + '</ul>'
                                + '</div>';
                        }
                    } 
                    return dt
                },
                className: "dt-body-center"
            },
            { title: "CycleId", data: "CycleId" },
            { title: "Client Name", data: "FirstName" },
            { title: "PhoneNumber", data: "Telephone" },
            { title: "Visit Date", data: "VisitDate" },
            { title: "Booking Date", data: "DateCreated" },
            { title: "Booking Time", data: "VisitTime" },
            {
                title: "Status", data: null, render: function (data, type, row) {
                        return '<span class="label label-success">Pending Results</span>';
                    } 

            },

        ],
        "aaSorting": [[4, "desc"], [5, "desc"]]
    });
}
function populateCompleted() {
    var dt;
    $('#completedTables').DataTable({
        data: scheduled,
        destroy: true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ],
        columns: [
            {
                title: "Actions", data: null, render: function (data, type, row) {
                    //  if (data.isWhatsAppSent === 0) {
                    //    if (type === 'display') {
                    //        dt = '<div class="dropdown ">'
                    //            + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                    //            + '<span class="caret"></span></button>'
                    //            + '<ul class="dropdown-menu">'
                    //            + '<li><a onclick="ViewCertificate(\'' + data.CycleId + '\')">View Certificate</a></li>'
                    //            + '<li><a onclick="SendWhatsAPP(\'' + data.CycleId + '\')">SendWhatsApp</a></li>'
                    //            + '<li><a onclick="SendEmail(\'' + data.CycleId + '\')">Send Email</a></li>'
                    //            + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                    //            + '</ul>'
                    //            + '</div>';
                    //    }

                    //}
                    //else 
                    if (data.isWhatsAppSent === 1) {
                        if (type === 'display') {
                            dt = '<div class="dropdown ">'
                                + '<button class="fa fa-list btn btn-primary dropdown-toggle " type="button" data-toggle="dropdown" ">'
                                + '<span class="caret"></span></button>'
                                + '<ul class="dropdown-menu">'
                                + '<li><a onclick="ViewCertificate(\'' + data.CycleId + '\')">View Certificate</a></li>'
                                + '<li><a onclick="SendWhatsAPP(\'' + data.CycleId + '\')">Resend WhatsApp</a></li>'
                                + '<li><a onclick="SendEmail(\'' + data.CycleId + '\')">Resend Email</a></li>'
                                + '<li><a onclick="ViewConsntForm(\'' + data.CycleId + '\')">Consent Form</a></li>'
                                + '</ul>'
                                + '</div>';
                        }
                    }
                    return dt
                },
                className: "dt-body-center"
            },
            { title: "CycleId", data: "CycleId" },
            { title: "Client Name", data: "FirstName" },
            { title: "PhoneNumber", data: "Telephone" },
            { title: "Visit Date", data: "VisitDate" },
            { title: "Booking Date", data: "DateCreated" },
            { title: "Booking Time", data: "VisitTime" },
            {
                title: "Status", data: null, render: function (data, type, row) {
                        return '<span class="label label-info">Completed</span>';
                    }

            },

        ],
        "aaSorting": [[4, "desc"], [5, "desc"]]
    });
}
//function updateBooking2(id) {
//    jQuery.grep(dataBookings, function (n, i) {
//        if (n.CycleId === id) {
//            console.log('Status6', id)
//            $('#status').val(3);
//            $('#clientName').val(n.FirstName);
//        }
//    });
//}

function updateBooking(id, status) {
    jQuery.grep(dataBookings, function (n, i) {
        if (n.CycleId === id) {
            if (parseInt(status) === 0) {
                $('#BookingStatus').val(1);
                $('#clientName').val(n.FirstName);
                $('#mid').val(id);
            }
            else if (parseInt(status) === 1) {
                $('#BookingStatus').val(2);
                $('#clientName').val(n.FirstName);
                $('#mid').val(id);
            }
            else if (parseInt(status) === 2) {
                $('#BookingStatus').val(3);
                $('#clientName').val(n.FirstName);
                $('#mid').val(id);
            }
            else if (parseInt(status) === 3) {
                $('#BookingStatus').val(4);
                $('#clientName').val(n.FirstName);
                $('#mid').val(id);
            }

        }
    })
    $('#PatientBookingModal').modal('show');
}
function ViewCertificate(CycleID) {
    console.log(CycleID);
    window.open('/Booking/CertPDF?CycleID=' + CycleID);

}
function ViewConsntForm(CycleID) {
    console.log(CycleID);
    window.open('/ConsetForm/index?CycleID=' + CycleID);

}

function SendWhatsAPP(id) {
    jQuery.grep(dataBookings, function (n, i) {
        if (n.CycleId === id) {
            $('#clientNameW').val(n.FirstName);
            $('#midW').val(id);
        }
    })
    $('#PatientWhatsAppModal').modal('show');
}
function SendEmail(id) {
    jQuery.grep(dataBookings, function (n, i) {
        if (n.CycleId === id) {
            $('#clientNameE').val(n.FirstName);
            $('#midE').val(id);
        }
    })
    $('#PatientEmailModal').modal('show');
}

function WhatsAppSender() {
    var url = '/Booking/SendWhatsApp';
    var postObj = {
        "cycleId": $('#midW').val(),
    };
    customPost("Proceed to send Certificate Via WhatsApp?", url, postObj, 'PatientWhatsAppModal', 'PatientWhatsAppModal',
        'PATIENT INFORMATION', populateCompleted);
}

function EmailSender() {
    var url = '/Booking/SendEmail';
    var postObj = {
        "cycleId": $('#midE').val(),
    };
    customPost("Proceed to send Certificate Via Email?", url, postObj, 'PatientEmailModal', 'PatientEmailModal',
        'PATIENT INFORMATION', populateCompleted);
}
function ClientUpdate() {
    var url = '/Home/UpdateStatus';
    var postObj = {
        "cycleId": $('#mid').val(),
        "newstatus": $('#BookingStatus').val()
    };
    customPost("Proceed to update booking?", url, postObj, 'PatientBookingModal', 'PatientBookingModal',
        'PATIENT INFORMATION', populateCompleted);
}
function CustomAlert(title, text, type) {
    swal({
        title: title,
        text: text,
        type: type,
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonClass: 'btn btn-quickSWap'
    });
}
function customPost(text, url, postObj, modalName, formName, actionName, populatingClass) {
    swal({
        title: "ARE YOU SURE?",
        text: text,
        type: 'warning',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        confirmButtonText: 'Yes!',
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: url,
                data: JSON.stringify(postObj),
                contentType: "application/json;charst=utf-8",
                dataType: 'JSON',
                type: 'POST',
                success: function (data) {
                    if (data.status === "success") {
                        if (modalName !== null) {
                            $('#' + modalName).modal('hide');
                        }
                        swal("Updated!", "Booking has been updated.!", "success");
                        $('#' + modalName).modal('hide');
                        fetchBookings();
                        populateBookings();
                    } else {
                        swal("Updated!", "Booking has been updated.!", "success");
                        $('#' + modalName).modal('hide');
                        fetchBookings();
                        populateBookings();
                    }
                }
            });

        } else {
            swal("Cancelled", "Your have cancelled :)", "error");
        }
    });
}
