// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {
	// goal: send a request which looks like this:
	// POST : http://localhost:53038/api/TeacherData/AddTeacher
	// with POST data of teacher firstname,teacher lastname, employeement number, salary.

	var URL = "http://localhost:53038/api/TeacherData/AddTeacher/";
	 
	var req = new XMLHttpRequest(); // XML Request

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var Salary = document.getElementById('Salary').value;

	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"Salary": Salary
	};

	req.open("POST", URL, true); // Method POST
	req.setRequestHeader("Content-Type", "application/json"); // Request Type JSON
	req.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (req.readyState == 4 && req.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing
		}
	}
	// POST information sent through the .send() method
	req.send(JSON.stringify(TeacherData));
}

// Client Side Validation
function validation() { 
	var formHandler = document.forms.GetTeacherForm; // Get Form With all Fields
	console.log(formHandler);
	alert("hello");

	formHandler.onsubmit = onProcess;

	function onProcess() {
		console.log(formHandler);
	}
}
