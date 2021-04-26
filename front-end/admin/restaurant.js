let base64imageValue = "";
let contentType = "";
let listRestaurants = [];

$(function () {
  //get list restaurants;
  data = {};
  fetch("http://localhost:4000/api/restaurant/list", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => response.json())
    .then((res) => {
      listRestaurants = res.list ?? [];
      // //generate DOM
      // let html = '';
      // listRestaurants.forEach(e => {
      //     html += `<img src=${e.imageSrc} alt=${e.imageName}" width="500" height="600">`
      // });
      // $("#test-list-restaurant").append(html);
      console.log(listRestaurants);
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

      listRestaurants.forEach((e, i) => {
        html += `
                <tr>
              <td>${i++}</td>
              <td>${e.restaurantName}</td>
              <td>${e.restaurantAddress}</td>
              <td>${e.restaurantEmail}</td>
              <td>${e.restaurantPhone}</td>
              <td>
                <div class="btn btn-sm btn-warning" data-toggle="modal" data-target="#modalAddRestaurant" onclick="editRecord('${
                  e.restaurantId
                }')">Edit</div>
                <div class="btn btn-sm btn-danger" onclick="deleteRecord()">Delete</div>
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

function editRecord(restaurantId) {
  let restaurant = listRestaurants.find((e) => e.restaurantId == restaurantId);

  if (!restaurant) return;

  $("form #id").val(restaurantId);

  // const formData = new FormData(document.getElementById('branch-form'));
  $("form #restaurantName").val(restaurant.restaurantName);
  $("#restaurantAddress").val(restaurant.restaurantAddress);
  $("#restaurantEmail").val(restaurant.restaurantEmail);
  $("#restaurantPhone").val(restaurant.restaurantPhone);
  // console.log(restaurant);

  let html = "";
  html += `<img src=${restaurant.imageSrc} alt=${restaurant.imageName}">`;

  $("#oldImg").append(html);

  $("#createBtn").html("Save");
}

function deleteRecord() {
  console.log();
}

async function createRestaurant() {
  let restaurantModel = {
    restaurantName: "",
    restaurantAddress: "",
    restaurantEmail: "",
    restaurantPhone: "",
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
    if (pair[0] == "restaurantName") restaurantModel.restaurantName = pair[1];
    if (pair[0] == "restaurantAddress")
      restaurantModel.restaurantAddress = pair[1];
    if (pair[0] == "restaurantEmail") restaurantModel.restaurantEmail = pair[1];
    if (pair[0] == "restaurantPhone") restaurantModel.restaurantPhone = pair[1];

    if (pair[0] == "id") restaurant_ID = pair[1];
  }

  if (restaurant_ID === 0) {
    data = { ...restaurantModel };
    $("#createBtn").html("Save");

    fetch("http://localhost:4000/api/restaurant/create", {
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
        } else {
        }
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  } else {
    
    restaurantModel.restaurantId = Number(restaurant_ID);
    data = { ...restaurantModel };
    console.log(data)
    fetch("http://localhost:4000/api/restaurant/update", {
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
