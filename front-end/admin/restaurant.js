let base64imageValue = '';
var contentType = '';

async function createRestaurant() {
    let restaurantModel = {
        "restaurantName": "",
        "restaurantAddress": "",
        "restaurantEmail": "",
        "restaurantPhone": "",
        "longitude": 0,
        "latitude": 0,
        "base64Value": base64imageValue,
        "imageName": "",
        "contentType": contentType
    }

    let fileInput = document.getElementById('restaurantImage');
    if (fileInput){
        if (fileInput.files.length !=0) {
            restaurantModel.imageName = fileInput.files[0].name;
        }
    }
    const formData = new FormData(document.getElementById('branch-form'));

    for (var pair of formData.entries()) {
        if (pair[0] == 'restaurantName') restaurantModel.restaurantName = pair[1];
        if (pair[0] == 'restaurantAddress') restaurantModel.restaurantAddress = pair[1];
        if (pair[0] == 'restaurantEmail') restaurantModel.restaurantEmail = pair[1];
        if (pair[0] == 'restaurantPhone') restaurantModel.restaurantPhone = pair[1];
    }

    data = { ...restaurantModel };
    fetch('http://localhost:4000/api/restaurant/create', {
        method: 'POST', // or 'PUT'
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then(response => response.json())
        .then(res => {
            console.log('Success:', res);
            let response = { ...res };
            if (response.statusCode === 200) {
            } else {
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

        console.log('b64:', b64)

        // let b64ContentType = reader.result.match(/^data:.+;base64,/);
        // console.log('b64ContentType', b64ContentType)

        // contentType = b64ContentType[0]['0'];
        // console.log('contentType: ', contentType)
        let listString = reader.result.split(',');

        contentType = listString[0];
    };

    reader.readAsDataURL(file);
};

$("#restaurantImage").change(() => {
   let path = $("#restaurantImage").val()
   let fileName = path.split('\\').pop();
   $(".custom-file label").text(fileName)
})
