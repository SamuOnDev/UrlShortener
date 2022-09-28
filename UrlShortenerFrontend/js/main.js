$(document).ready(function () {

    $('#test').click(function () {
        $.ajax({
            type: 'GET',
            url: '/api/profile',
            beforeSend: function (xhr) {
                if (localStorage.token) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                }
            },
            success: function (data) {
                alert('Hello ' + data.name + '! You have successfully accessed to /api/profile.');
            },
            error: function () {
                alert("Sorry, you are not logged in.");
            }
        });
    });

    document.querySelector('#login-form').addEventListener('submit', function () {
        event.preventDefault()
        alert(this.elements.username.value)
    });

    $('#goodLogin').click(function () {
        $.ajax({
            type: "POST",
            url: "/login",
            data: {
                username: document.getElementById('textbox_id').value,
                password: "foobar"
            },
            success: function (data) {
                localStorage.token = data.token;
                alert('Got a token from the server! Token: ' + data.token);
            },
            error: function () {
                alert("Login Failed");
            }
        });
    });

});