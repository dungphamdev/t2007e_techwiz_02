function register() {
    let registerModel = {
		'CustomerName': '',
		'CustomerEmailId': '',
		'CustomerContactPhone': '',
        'Username': '',
		'Password': '',
		'CustomerAddress': '',
    }
    const formData = new FormData(document.getElementById('register-form'))
    for (var pair of formData.entries()) {
        if (pair[0] == 'name') registerModel.CustomerName = pair[1];
        if (pair[0] == 'email') registerModel.CustomerEmailId = pair[1];
		if (pair[0] == 'phone') registerModel.CustomerContactPhone = pair[1];
		if (pair[0] == 'username') registerModel.Username = pair[1];
		if (pair[0] == 'password') registerModel.Password = pair[1];
		if (pair[0] == 'address') registerModel.CustomerAddress = pair[1];
    }
    //data = {...registerModel};
	data = {customer: {...registerModel}};
    fetch("http://localhost:4000/api/customer/create", {
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
               //localStorage.setItem('customer', JSON.stringify(response.customer));
			   alert('Ok');
           } else {
               //remove authen in localstore
               //localStorage.removeItem('customer');
			   alert('Fail');
           }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}