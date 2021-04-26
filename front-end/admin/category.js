let base64imageValue = "";
let contentType = "";
let listCategories = [];

$(function () {
    //get list restaurants;
    data = {};
    fetch("http://localhost:4000/api/category/list", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })
        .then((response) => response.json())
        .then((res) => {
            listCategories = res.list ?? [];
            // //generate DOM
            // let html = '';
            // listRestaurants.forEach(e => {
            //     html += `<img src=${e.imageSrc} alt=${e.imageName}" width="500" height="600">`
            // });
            // $("#test-list-restaurant").append(html);
            console.log(listCategories);
            // let tbody = $("#tableBody")
            // let tr = $("<tr>")
            // listRestaurants.forEach(e => {
            //     let td = $("<td>")
            //     td.append(e.restaurantId)
            //     td.append(e.restaurantName)
            //     td.append(e.restaurantAddress)
            //     tr.append()
            // });
            // tbody.append(tr)

            let html = "";

            listCategories.forEach((e, i) => {
                html += `
                <tr>
              <td>${i++}</td>
              <td>${e.categoryName}</td>
              <td>
                <div class="btn btn-sm btn-warning" data-toggle="modal" data-target="#modalAddRestaurant" onclick="editRecord('${
                    e.categoryId
                }')">Edit</div>
                <div class="btn btn-sm btn-danger" onclick="deleteRecord('${
                    e.categoryId
                }')">Delete</div>
              </td>
            </tr>
                `;
            });
            $("#tableBody").append(html);
        })
        .catch((error) => {
            console.error("Error:", error);
        });
});

$("#openModalBtn").click(() => {
    $("#branch-form").trigger("reset");
    $("#oldImg").hide()
})

function editRecord(categoryId) {
    let restaurant = listCategories.find((e) => e.categoryId == categoryId);

    if (!restaurant) return;

    $("form #id").val(categoryId);

    // const formData = new FormData(document.getElementById('branch-form'));
    $(" #categoryName").val(restaurant.categoryName);
    // console.log(restaurant);



    $("#createBtn").html("Save");
}

function deleteRecord(categoryId) {
    data = {
        'categoryId': Number(categoryId)
    };

    console.log(data)

    if (confirm("Ban chac chan muon xoa hay khong?")){
        fetch("http://localhost:4000/api/category/delete", {
            method: "POST", // or 'PUT'
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((res) => {
                console.log("Success:", res);
                let response = { ...res };
                if (response.statusCode === 200) {
                    // thoong bao thanh cong

                } else {
                    // thong bao loi
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }
}
async function createRestaurant() {
    let restaurantModel = {
        categoryName: "",

        longitude: 0,
        latitude: 0,
        base64Value: base64imageValue,
        imageName: "",
        contentType: contentType,
    };

    let fileInput = document.getElementById("restaurantImage");
    if (fileInput) {
        if (fileInput.files.length != 0) {
            restaurantModel.imageName = fileInput.files[0].name;
        }
    }
    const formData = new FormData(document.getElementById("branch-form"));
    let restaurant_ID = 0;
    for (var pair of formData.entries()) {
        if (pair[0] == "categoryName") restaurantModel.categoryName = pair[1];

        if (pair[0] == "id") restaurant_ID = pair[1];
    }


    console.log('restaurant_ID:',restaurant_ID)


    if (restaurant_ID == 0) {
        data = { ...restaurantModel };
        $("#createBtn").html("Save");

        fetch("http://localhost:4000/api/category/create", {
            method: "POST", // or 'PUT'
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((res) => {
                console.log("Success:", res);
                let response = { ...res };
                if (response.statusCode === 200) {

                    // thoong bao thanh cong
                } else {
                    // thong bao loi
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    } else {
        restaurantModel.categoryId = Number(restaurant_ID);
        data = { ...restaurantModel };
        console.log(data)
        fetch("http://localhost:4000/api/category/update", {
            method: "POST", // or 'PUT'
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((res) => {
                console.log("Success:", res);
                let response = { ...res };
                if (response.statusCode === 200) {
                    $("#id").val(0);
                    // thong bao thanh cong
                    location.reload();
                } else {
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }
}
var input = document.querySelector("input[type=file]");
input.onchange = function () {
    var file = input.files[0],
        reader = new FileReader();
    reader.onloadend = function () {
        var b64 = reader.result.replace(/^data:.+;base64,/, "");
        base64imageValue = b64;

        console.log("b64:", b64);

        // let b64ContentType = reader.result.match(/^data:.+;base64,/);
        // console.log('b64ContentType', b64ContentType)

        // contentType = b64ContentType[0]['0'];
        // console.log('contentType: ', contentType)
        let listString = reader.result.split(",");

        contentType = listString[0];
    };

    reader.readAsDataURL(file);
};

$("#restaurantImage").change(() => {
    let path = $("#restaurantImage").val();
    let fileName = path.split("\\").pop();
    $(".custom-file label").text(fileName);
});
