
function create(){
    let Model = {
		// 'ParentId': '',
		'CategoryName': '',
    }
    const formData = new FormData(document.getElementById('category-form'))
    for (var pair of formData.entries()) {
        // if (pair[0] == 'parentid')
        // {
        //     Model.ParentId =  pair[1] ? Number(pair[1]) : null ;
        // }
        if (pair[0] == 'name') Model.CategoryName = pair[1];
    }
    console.log('ItemModel:', Model)
    data = {...Model}
    fetch("http://localhost:4000/api/category/create", {
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