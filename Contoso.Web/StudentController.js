angular.module("contosoApp").controller("StudentController", function ($scope, $http, $resource, $stateParams, toastr) {
    $scope.whatisjavascript = "Javascript is like wife!";
    $scope.whatisangularjs = "AngularJS is like girlfriend!";

    var formData = new FormData();

    $scope.getTheStudentPhoto = function ($files) {
        angular.forEach($files, function (value, key) {
            formData.append(key, value);
        });
    }

    var uploadStudentPhoto = function (studentId) {
        var studentPhotoObj = {
            method: "post",
            url: "http://localhost:3065/api/upload-student-photo/" + studentId,
            data: formData,
            headers: {
                'Content-type': undefined
            }
        }

        $http(studentPhotoObj).then(function (response) {
            console.log(response);
        });
    };

    $scope.saveStudent = function (data) {
        $http.post("http://localhost:3065/contoso/student", data).then(function (response) {
            if (response.status === 201 && response.statusText === "Created") {
                uploadStudentPhoto(response.data.Id);
                toastr.success("Student information has been added successfully!");
            }
        }, function (error) {
            console.log(error);
            toastr.error(error.data.Message);
        });
    }

    $scope.getStudents = function () {
        $http.get("http://localhost:3065/contoso/student").then(function (response) {
            $scope.students = response.data;
        });
    }

    $scope.getStudentById = function () {
        $http.get("http://localhost:3065/contoso/student/" + $stateParams.studentId).then(function (response) {
            $scope.student = response.data;
        });
    }

    $scope.updateStudent = function (student) {
        $http.put("http://localhost:3065/contoso/student/" + $stateParams.studentId, student).then(function (response) {
            if (response.status === 204 && response.statusText === "No Content") {
                toastr.success('Student information is updated successfully!');
            }
        });
    }

    $scope.deleteStudent = function (studentId) {
        $http.delete("http://localhost:3065/contoso/student/" + studentId).then(function (response) {
            debugger;
            toastr.error('Student information is deleted!');
            $scope.getStudents();
        });
    }
});