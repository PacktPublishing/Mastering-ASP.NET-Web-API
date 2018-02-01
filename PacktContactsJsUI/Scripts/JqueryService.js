var endpoint = "http://localhost:50461";
var recordsArray = [];
var isEdit = false;
var selectedItem = null;

function doLogin() {
    var loginUrl = endpoint + "/api/auth/token";
    var sendObject = JSON.stringify({ username: $("#uname").val(), password: $("#psw").val() });

    $.ajax({
        url: loginUrl,
        contentType: 'application/json; charset=UTF-8',
        data: sendObject,
        method: "POST"
    }).done(function (data, status) {
        if (status == "success") {
            if (data.token) {
                localStorage.setItem("AuthToken", data.token);
                getContacts();
                $('#divlogin').hide();
            }
        }
        else {
            bootbox.alert("Authentication Failed", function () {
            });
        }

        }).fail(function () {
            bootbox.alert("Authentication Error", function () {
            });
    });
}

function getContacts() {
    var getUrl = endpoint + "/api/contacts";
    var authtoken = localStorage.getItem("AuthToken");

    $.ajax({
        url: getUrl,
        contentType: 'application/json; charset=UTF-8',        
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'BEARER ' + authtoken);
        },
        method: "GET"
    }).done(function (data, status) {
        if (status == "success") {            
            if (data.length > 0) {
                $("#contactsgrid").show();
                DisplayContactsGrid(data);
            }
            else {
                bootbox.confirm("No Contacts exists, Click OK to create", function (result) {
                    if (result) {
                        $("#contactsgrid").hide();
                        $("#contactform").show();
                        clearFormValues();
                    }
                    else {
                        $("#contactsgrid").show();
                        $("#gridContent").html("");
                        $("#contactform").hide();
                    }
                })
    }}
    }).fail(function () {
        bootbox.alert("Authentication Error", function () {
        });
    });   
}

function DisplayContactsGrid(recrdArry) {
    recordsArray = [];
    recordsArray = recrdArry;
    var html = "<table class='table table-hover'><thead><tr><th>First Name</th><th>Last Name</th><th>Email</th><th></th></tr></thead><tbody>";
    for (var i = 0; i < recrdArry.length; i++) {
        html += "<tr>";
        html += "<td>" + recrdArry[i].firstName + "</td>";
        html += "<td>" + recrdArry[i].lastName + "</td>";
        html += "<td>" + recrdArry[i].email + "</td>";
        html += "<td><button onclick='EditContact(" + i + ")' class='btn btn-primary'>Edit</button>&nbsp;<button onclick='DeleteContact(" + i + ")' class='btn btn-danger'>Delete</button></td>";
        html += "</tr>";
    }
    html += "</tbody></table>";

    $("#gridContent").html(html);

}

function EditContact(recIndex) {
    selectedItem = recordsArray[recIndex];
    isEdit = true;
    showContactEditForm(selectedItem);
}

function showContactEditForm(recordItem) {
    $("#contactsgrid").hide();
    $("#contactform").show();    

    $("#firstName").val(recordItem.firstName);
    $("#lastName").val(recordItem.lastName);
    $("#emailid").val(recordItem.email);    
}

function DeleteContact(recIndex) {
    selectedItem = recordsArray[recIndex];
    bootbox.confirm({
        message: "Do you really want to delete?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                DeleteRecords(selectedItem.id);
            }
        }
    });
}

function AddContacts() {
    var contactform = document.getElementById('contactform');
    var contactsgrid = document.getElementById('contactsgrid');

    contactform.style.display = 'block';
    contactsgrid.style.display = 'none';

    clearFormValues();
}

function saveContact() {

    if (isEdit == true && selectedItem != null) {
        updateContact(selectedItem);
    }
    var authtoken = localStorage.getItem("AuthToken");
    var postUrl = endpoint + "/api/contacts";
    var sendObject = JSON.stringify({ firstName: $("#firstName").val(), lastName: $("#lastName").val(), "email": $("#emailid").val() });

    $.ajax({
        url: postUrl,
        contentType: 'application/json; charset=UTF-8',
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'BEARER ' + authtoken);
        },
        data:sendObject,
        method: "POST"
    }).done(function (data, status) {
        if (status == "success") {
            getContacts();
            $("#contactform").hide();
        }
    }).fail(function () {
        bootbox.alert("Save Error", function () {
        });
        $("#contactform").hide();
    });
}

function updateContact(recordItem) {
    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');

    var putUrl = endpoint + "/api/contacts/" + recordItem.id;
    var authtoken = localStorage.getItem("AuthToken");
    
    var sendObject = JSON.stringify({ firstName: $("#firstName").val(), lastName: $("#lastName").val(), "email": $("#emailid").val() });

    $.ajax({
        url: putUrl,
        contentType: 'application/json; charset=UTF-8',
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'BEARER ' + authtoken);
        },
        data: sendObject,
        method: "PUT"
    }).done(function (data, status) {
        if (status == "success") {
            getContacts();
            $("#contactform").hide();
        }
    }).fail(function () {
        bootbox.alert("Save Error", function () {
        });
        $("#contactform").hide();
    });
    
}

function clearFormValues() {    
    $('#firstName').val("");
    $('#lastName').val("");
    $('#emailid').val("");

}

function DeleteRecords(contactId) {

    var deleteUrl = endpoint + "/api/contacts/" + contactId;
    var authtoken = localStorage.getItem("AuthToken");

    $.ajax({
        url: deleteUrl,
        contentType: 'application/json; charset=UTF-8',        
        method: "DELETE",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'BEARER ' + authtoken);
        },
    }).done(function (data, status) {
        if (status == "nocontent") {            
            $('#contactsgrid').show();
            selectedItem = null;
            getContacts();
        }
    }).fail(function () {
        bootbox.alert("Operation Error", function () {
        });
    });
}

function doCancel() {
    $('#contactsgrid').show();
    $('#contactform').hide();    
    isEdit = false;
    clearFormValues();
    getContacts();
}