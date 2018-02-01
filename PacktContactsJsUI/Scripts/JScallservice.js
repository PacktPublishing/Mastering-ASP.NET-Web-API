var endpoint = "http://localhost:50461";
var recordsArray = [];
var isEdit = false;
var selectedItem = null;

function doLogin() {    
    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');

    var loginUrl = endpoint + "/api/auth/token";
    var xhr = new XMLHttpRequest();
    var userElement = document.getElementById('uname');
    var passwordElement = document.getElementById('psw');    
    var username = userElement.value;
    var password = passwordElement.value;

    xhr.open('POST', loginUrl, true);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    xhr.addEventListener('load', function () {
        if (this.status == 200) {
            var responseObject = JSON.parse(this.response);
            if (responseObject.token) {
                //console.log(responseObject.token);
                localStorage.setItem("AuthToken", responseObject.token);
                getContacts();                
                divlogin.style.display = 'none';
            }
        }
        else {
            bootbox.alert("Authentication Failed", function () {                
            });
        }
    });

    var sendObject = JSON.stringify({ username: username, password: password });
    xhr.send(sendObject);
}

function getContacts() {
    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');


    var Url = endpoint + "/api/contacts";
    var xhr = new XMLHttpRequest();    
    var authtoken = localStorage.getItem("AuthToken");
    xhr.open('GET', Url, true);
    xhr.setRequestHeader("Authorization", "Bearer " + authtoken);
    xhr.addEventListener('load', function () {
        var responseObject = JSON.parse(this.response);        
        if (this.status == 200) {
            var responseObject = JSON.parse(this.response);
            console.log(responseObject + ' ' + responseObject.length); 
            if (responseObject.length > 0) {                
                contactsgrid.style.display = 'block';
                DisplayContactsGrid(responseObject);
            } else {
                bootbox.confirm("No Contacts exists, Click OK to create", function (result) {                    
                    if (result) {
                        contactform.style.display = 'block';
                        contactsgrid.style.display = 'none';
                        clearFormValues();
                    }
                    else {
                        contactform.style.display = 'none';
                        contactsgrid.style.display = 'block';                        
                    }
                    
                });                
            }            
        }
        else {
            bootbox.alert("Operation Failed", function () {
            });
        }
    });

    xhr.send(null);
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
        html += "<td><button onclick='EditContact("+i+")' class='btn btn-primary'>Edit</button>&nbsp;<button onclick='DeleteContact("+i+")' class='btn btn-danger'>Delete</button></td>";
        html += "</tr>";
    }
    html += "</tbody></table>";
    
    document.getElementById("gridContent").innerHTML = html;

}

function EditContact(recIndex) {    
    selectedItem = recordsArray[recIndex];
    isEdit = true;
    showContactEditForm(selectedItem);
}

function showContactEditForm(recordItem) {

    var contactform = document.getElementById('contactform');    
    var contactsgrid = document.getElementById('contactsgrid');
    contactform.style.display = 'block';
    contactsgrid.style.display = 'none';


    var firstNameElement = document.getElementById('firstName');
    var lastNameElement = document.getElementById('lastName');
    var emailElement = document.getElementById('emailid');

    firstNameElement.value = recordItem.firstName;
    lastNameElement.value = recordItem.lastName;
    emailElement.value = recordItem.email;
}

function DeleteContact(recIndex) {
    //alert(recIndex + 'Delete');
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

    if (isEdit == true && selectedItem !=null) {
        updateContact(selectedItem);
    }

    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');

    var postUrl = endpoint + "/api/contacts";
    var xhr = new XMLHttpRequest();
    var firstNameElement = document.getElementById('firstName');
    var lastNameElement = document.getElementById('lastName');
    var emailElement = document.getElementById('emailid');

    var fname = firstNameElement.value;
    var lname = lastNameElement.value;
    var email = emailElement.value;

    var authtoken = localStorage.getItem("AuthToken");    
    xhr.open('POST', postUrl, true);
    xhr.setRequestHeader("Authorization", "Bearer " + authtoken);    
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    
    xhr.addEventListener('load', function () {
        if (this.status == 201) {
            var responseObject = JSON.parse(this.response);  
            console.log(responseObject);
            getContacts();
            contactform.style.display = 'none';
        }
        else {
            bootbox.alert("Save Failed", function () {
                contactform.style.display = 'none';
            });
        }
    });

    var sendObject = JSON.stringify({ firstName: fname, lastName: lname, email:email });
    xhr.send(sendObject);
}

function updateContact(recordItem) {
    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');

    var putUrl = endpoint + "/api/contacts/" + recordItem.id;
    var xhr = new XMLHttpRequest();
    var firstNameElement = document.getElementById('firstName');
    var lastNameElement = document.getElementById('lastName');
    var emailElement = document.getElementById('emailid');

    var fname = firstNameElement.value;
    var lname = lastNameElement.value;
    var email = emailElement.value;

    var authtoken = localStorage.getItem("AuthToken");
    xhr.open('PUT', putUrl, true);
    xhr.setRequestHeader("Authorization", "Bearer " + authtoken);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

    xhr.addEventListener('load', function () {
        if (this.status == 204) {            
            getContacts();
            clearFormValues();
            contactform.style.display = 'none';
        }
        else {
            bootbox.alert("Update Failed", function () {
                contactform.style.display = 'none';
            });
        }
    });

    var sendObject = JSON.stringify({ firstName: fname, lastName: lname, email: email, id: selectedItem.id });
    xhr.send(sendObject);
}

function clearFormValues() {
    var firstNameElement = document.getElementById('firstName');
    var lastNameElement = document.getElementById('lastName');
    var emailElement = document.getElementById('emailid');

    firstNameElement.value = "";
    lastNameElement.value = "";
    emailElement.value = "";
}

function DeleteRecords(contactId) {

    var Url = endpoint + "/api/contacts/" + contactId;
    var xhr = new XMLHttpRequest();
    var authtoken = localStorage.getItem("AuthToken");
    xhr.open('DELETE', Url, true);
    xhr.setRequestHeader("Authorization", "Bearer " + authtoken);
    xhr.addEventListener('load', function () {        
        if (this.status == 204) {
            contactsgrid.style.display = 'block';
            selectedItem = null;
            getContacts();
        }
        else {
            bootbox.alert("Operation Failed", function () {
            });
        }
    });

    xhr.send(null);
}

function doCancel() {
    var contactform = document.getElementById('contactform');
    var divlogin = document.getElementById('divlogin');
    var contactsgrid = document.getElementById('contactsgrid');

    contactsgrid.style.display = 'block';
    contactform.style.display = 'none';
    //divlogin.style.display = 'none';
    isEdit = false;
    clearFormValues();
    getContacts();
}