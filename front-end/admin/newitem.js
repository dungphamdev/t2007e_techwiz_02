let base64imageValue = '';
let contentType = '';

$(document).ready(function(){
    /* restaurant dropdown */
    fetch("http://localhost:4000/api/restaurant/list", {
        method: 'POST', // or 'PUT'
        headers: {
            'Content-Type': 'application/json',
        },
        data: null,       
    })
    .then(response => response.json())
    .then(res => {
        let response = {...res};
        if (response.statusCode === 200) {
            var html = '';
            for(let i = 0;i<response.list.length; i++)
            {
                html += '<option value="' + response.list[i].restaurantId + '">' + response.list[i].restaurantName + '</option>';
            }
            $('#sltRestaurant').html(html);
        } else {
            alert('Fail');
        }
    })
    .catch((error) => {
        console.error('Error:', error);
    });

    /* category dropdown */
	fetch("http://localhost:4000/api/category/list", {
        method: 'POST', // or 'PUT'
        headers: {
            'Content-Type': 'application/json',
        },
        data: null,       
    })
    .then(response => response.json())
    .then(res => {
        let response = {...res};
        console.log(response)
        if (response.statusCode === 200) {
            var htmlCategory = '';
            for(let i = 0;i<response.list.length; i++)
            {
                htmlCategory += '<option value="' + response.list[i].categoryId + '">' + response.list[i].categoryName + '</option>';
            }
            $('#categoryid').html(htmlCategory);

        } else {
            console.error('Error:', error);
        }
    })
    .catch((error) => {
        console.error('Error:', error);
    });
})
function create(){
    let ItemModel = {
		'ItemName': '',
		'RestaurantId': '',
		'ItemDescription': '',
        'ItemPrice': 0,
		'ItemCategoryId': null,
        "imageName": "",
        "contentType": contentType,
        "base64Value": base64imageValue,
    }

    let fileInput = document.getElementById('restaurantImage');
    if (fileInput){
        if (fileInput.files.length !=0) {
            ItemModel.imageName = fileInput.files[0].name;
        }
    }

    const formData = new FormData(document.getElementById('newitem-form'))
    for (var pair of formData.entries()) {
        console.log(pair)
        if (pair[0] == 'itemname') ItemModel.ItemName = pair[1];
        if (pair[0] == 'sltRestaurant')
        {
            ItemModel.RestaurantId =  pair[1] ? Number(pair[1]) : null ;
        }
		if (pair[0] == 'desc') ItemModel.ItemDescription = pair[1];
		if (pair[0] == 'price')
        {
            ItemModel.ItemPrice =  pair[1] ? Number(pair[1]) : null ;
        }
        if (pair[0] == 'categoryid')
        {
            ItemModel.ItemCategoryId = pair[1] ? Number(pair[1]) : null ;
            // if (pair[1]) {
            //     ItemModel.ItemCategoryId = [...ItemModel.ItemCategoryId, Number(pair[1])]
            // }
        }
		if (pair[0] == 'imagepath') ItemModel.MainImagePath = pair[1];
    }


     data = {...ItemModel}

    console.log(data)

    fetch("http://localhost:4000/api/item/create", {
        method: 'POST', // or 'PUT'
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
    .then(response => response.json())
    .then(res => {
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

var input = document.querySelector('input[type=file]');
input.onchange = function () {
    var file = input.files[0],
        reader = new FileReader();
    reader.onloadend = function () {
        var b64 = reader.result.replace(/^data:.+;base64,/, '');
        base64imageValue = b64;
        let listString = reader.result.split(',');
        contentType = listString[0];
    };

    reader.readAsDataURL(file);
};