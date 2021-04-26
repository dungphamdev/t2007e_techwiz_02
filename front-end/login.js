function login() {
    let loginModel = {
        'username': '',
        'password': ''
    }
    const formData = new FormData(document.getElementById('login-form'))
    for (var pair of formData.entries()) {
        if (pair[0] == 'username') loginModel.username = pair[1];
        if (pair[0] == 'password') loginModel.password = pair[1];
    }
    data = {...loginModel};
    fetch('http://localhost:4000/api/login', {
        method: 'POST', // or 'PUT'
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then(response => response.json())
        .then(res => {
            console.log('Success:', res);
           let response = {...res};
           if (response.statusCode === 200) {
               //add authen to store
               localStorage.setItem('customer', JSON.stringify(response.customer));
           } else {
               //remove authen in localstore
               localStorage.removeItem('customer');
           }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}